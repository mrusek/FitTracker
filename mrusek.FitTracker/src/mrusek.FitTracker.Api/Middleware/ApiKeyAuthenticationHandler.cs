using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using mrusek.FitTracker.Domain.Interfaces;

namespace mrusek.FitTracker.Api.Middleware;

public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISecretManager secretManager)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder) //TODO: Problem details
{
    private readonly ISecretManager _secretManager =
        secretManager ?? throw new ArgumentNullException(nameof(secretManager));

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("apiKey", out var apiKey))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var foundApiKey = _secretManager.TryGetValue(apiKey, out var keyValue);
        if (!foundApiKey)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API Key"));
        }

        var claims = new[] { new Claim(ClaimTypes.Name, "ApiKeyUser") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}