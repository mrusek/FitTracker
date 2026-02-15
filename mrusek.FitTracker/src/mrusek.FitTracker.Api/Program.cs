using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using mrusek.FitTracker.Api.Endpoints;
using mrusek.FitTracker.Api.Extensions;
using mrusek.FitTracker.Api.Middleware;
using mrusek.FitTracker.Application;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Domain.Interfaces;
using mrusek.FitTracker.Infrastructure;
using mrusek.FitTracker.Infrastructure.Identity;
using mrusek.FitTracker.Infrastructure.Persistence;
using mrusek.FitTracker.Infrastructure.Services;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Formatting.Json;
using DependencyInjection = mrusek.FitTracker.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.Scan(scan =>
    scan.FromAssembliesOf(typeof(Program), typeof(DependencyInjection), typeof(ICommand))
        .AddClasses()
        .AsMatchingInterface()
        .WithScopedLifetime());

var jwtConfig = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddSingleton(jwtConfig!);
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
if (builder.Environment.IsDevelopment())
    builder.Services.AddSingleton<ISecretManager, UserSecretManager>();
else
    builder.Services.AddSingleton<ISecretManager, EnvSecretManager>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CompositeAuthenticationDefaults.AuthenticationScheme;
    })
    .AddScheme<AuthenticationSchemeOptions, CompositeAuthenticationHandler>(
        CompositeAuthenticationDefaults.AuthenticationScheme, _ => { })
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(
        ApiKeyAuthenticationDefaults.AuthenticationScheme, _ => { })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true;
        // options.Authority = "authority"; Do keycloak
        // options.MetadataAddress =
        //     "metadata-address";  e.g. keycloak ? openid-configuration
        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
        options.Audience = "Fittracker.Api";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig!.Issuer,
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)),
            RequireExpirationTime = true
        };
    });
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddApplication();
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder().AddAuthorizationPolicies();
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddFixedWindowLimiter("fixed", config =>
    {
        config.PermitLimit = 10;
        config.Window = TimeSpan.FromMinutes(1);
    });
});
var connectionString = builder.Configuration.GetConnectionString("fitTrackerDev");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure().UseMicrosoftJson()));
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(path: "logs/log-.json",
        rollingInterval: RollingInterval.Day,
        formatter: new JsonFormatter(),
        retainedFileCountLimit: 10)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeScopes = true;
    logging.IncludeFormattedMessage = true;
});
builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("mrusek.FitTracker.Api"))
    .WithMetrics(metrics =>
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation())
    .WithTracing(tracing =>
        tracing
            .AddHttpClientInstrumentation()
            .AddAspNetCoreInstrumentation())
    .UseOtlpExporter();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentSessionProvider, CurrentSessionProvider>();
builder.Services.AddHealthChecks();
var app = builder.Build();
app.MapHealthChecks("/hc");
app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseSerilogRequestLogging(options => { options.MessageTemplate = "Handled {RequestPath} in {Elapsed:0.0000} ms"; });
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/api-docs");
}

app.MapGet("/", () => "Hello World!");
app.MapRecipeEndpoints();
app.MapProductEndpoints();
app.UseAuthorization();
app.UseAuthentication();
await app.RunAsync();