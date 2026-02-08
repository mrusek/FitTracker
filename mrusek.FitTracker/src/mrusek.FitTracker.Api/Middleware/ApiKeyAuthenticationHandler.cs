using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using mrusek.FitTracker.Domain.Interfaces;

namespace mrusek.FitTracker.Api.Middleware;

internal static class ApiKeyAuthenticationDefaults
{
    public const string AuthenticationScheme = "ApiKey";
    public const string ApiKeyHeaderName = "X-API-Key";
    public const string KeyName = "ApiKey";
}
public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISecretManager secretManager)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    private readonly ISecretManager _secretManager =
        secretManager ?? throw new ArgumentNullException(nameof(secretManager));

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyAuthenticationDefaults.ApiKeyHeaderName, out var apiKeyValue))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var foundApiKey = _secretManager.TryGetValue(ApiKeyAuthenticationDefaults.KeyName, out var keyValue);
        if (!foundApiKey || !keyValue.Equals(apiKeyValue, StringComparison.Ordinal))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API Key"));
        }

        var claims = new[] { new Claim(ClaimTypes.Name, "ApiKeyUser") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        var authResult = await HandleAuthenticateOnceAsync();
        if (authResult.Succeeded)
        {
            return;
        }

        Response.StatusCode = StatusCodes.Status401Unauthorized;
        Response.Headers.WWWAuthenticate = ApiKeyAuthenticationDefaults.AuthenticationScheme;

        var detail = authResult.Failure?.Message;
        const string type = "https://tools.ietf.org/html/rfc9110#section-15.5.2";
        var problemDetails = new ProblemDetails()
        {
            Type = type,
            Title = ReasonPhrases.GetReasonPhrase(StatusCodes.Status401Unauthorized),
            Status = StatusCodes.Status401Unauthorized,
            Detail = detail
        };

        const string contentType = "application/problem+json";
        await Response.WriteAsJsonAsync(problemDetails, (JsonSerializerOptions?)null, contentType);
    }
}