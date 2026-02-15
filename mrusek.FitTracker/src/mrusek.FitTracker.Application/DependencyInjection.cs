using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using mrusek.FitTracker.Application.Abstractions.Orchestration;
using mrusek.FitTracker.Application.Behaviors;

namespace mrusek.FitTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddValidatorsFromAssembly(assembly);
        services.AddTransient(
            typeof(ICommandPipelineBehavior<,>),
            typeof(ValidationPipelineBehavior<,>));
        return services;
    }
}