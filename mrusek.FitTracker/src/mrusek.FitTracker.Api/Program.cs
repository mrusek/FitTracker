using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.RateLimiting;
using mrusek.FitTracker.Api.Endpoints;
using mrusek.FitTracker.Api.Extensions;
using mrusek.FitTracker.Api.Middleware;
using mrusek.FitTracker.Application;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Domain.Interfaces;
using mrusek.FitTracker.Infrastructure.Services;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.Scan(scan =>
    scan.FromAssembliesOf(typeof(Program), typeof(DependencyInjection), typeof(ICommand))
        .AddClasses()
        .AsMatchingInterface()
        .WithScopedLifetime());
    
    
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
if (builder.Environment.IsDevelopment())
    builder.Services.AddSingleton<ISecretManager, UserSecretManager>();
else
    builder.Services.AddSingleton<ISecretManager, EnvSecretManager>();
builder.Services.AddAuthentication().AddJwtBearer()
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("", options => { });
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

await app.RunAsync();