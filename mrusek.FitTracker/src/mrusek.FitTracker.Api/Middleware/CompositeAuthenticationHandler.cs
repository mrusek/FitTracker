using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace mrusek.FitTracker.Api.Middleware;

internal static class CompositeAuthenticationDefaults
{
    public const string AuthenticationScheme =
        $"Composite-{JwtBearerDefaults.AuthenticationScheme}-{ApiKeyAuthenticationDefaults.ApiKeyHeaderName}";
}

internal sealed class CompositeAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var scheme = GetAuthenticationScheme();
        return await Context.AuthenticateAsync(scheme);
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        var scheme = GetAuthenticationScheme();
        await Context.ChallengeAsync(scheme);
    }

    protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        var scheme = GetAuthenticationScheme();
        await Context.ForbidAsync(scheme);
    }

    private bool IsApiKeyAuthScheme()
        => Request.Headers.ContainsKey(ApiKeyAuthenticationDefaults.ApiKeyHeaderName);

    private string GetAuthenticationScheme()
        => IsApiKeyAuthScheme()
            ? ApiKeyAuthenticationDefaults.AuthenticationScheme
            : JwtBearerDefaults.AuthenticationScheme;
}