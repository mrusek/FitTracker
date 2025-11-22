using Microsoft.AspNetCore.Authorization;

namespace mrusek.FitTracker.Api.Extensions;

public static class AuthorizationPolicyBuilder
{
    public static void AddAuthorizationPolicies(this AuthorizationBuilder builder)
    {
        builder.AddPolicy("admin", policy => policy.RequireRole("admin").RequireClaim("scope", "admin_panel"));
        builder.AddPolicy("user", policy => policy.RequireRole("user").RequireClaim("scope", "fit_tracker_api"));
    }
}