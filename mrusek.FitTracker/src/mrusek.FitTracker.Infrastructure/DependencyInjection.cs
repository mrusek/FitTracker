using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mrusek.FitTracker.Domain.Interfaces;
using mrusek.FitTracker.Infrastructure.Identity;
using mrusek.FitTracker.Infrastructure.Persistence;
using mrusek.FitTracker.Infrastructure.Services;

namespace mrusek.FitTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment env)
    {
        AddSecretHandling(services, configuration, env);
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(connectionString));
        return services;
    }

    private static void AddSecretHandling(IServiceCollection services, IConfiguration configuration,
        IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddScoped<ISecretManager, UserSecretManager>();
        }
        else
            services.AddScoped<ISecretManager, EnvSecretManager>();
    }
}