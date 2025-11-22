using Microsoft.Extensions.Options;
using mrusek.FitTracker.Domain.Interfaces;
using mrusek.FitTracker.Infrastructure.Identity;
using mrusek.FitTracker.Application.Common;

namespace mrusek.FitTracker.Infrastructure.Services;

public class UserSecretManager(IOptions<JwtSettings> jwtSettings) : ISecretManager
{
    private readonly JwtSettings _jwtSettings =
        jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));

    public Task<bool> TryGetValueAsync(string key, out string value)
    {
        var result = DynamicExtensions.GetPropertyValue<string>(_jwtSettings, key);

        if (result is not null)
        {
            value = result;
            return Task.FromResult(true);
        }

        value = string.Empty;
        return Task.FromResult(false);
    }

    public bool TryGetValue(string key, out string value)
    {
        var result = DynamicExtensions.GetPropertyValue<string>(_jwtSettings, key);

        if (result is not null)
        {
            value = result;
            return true;
        }

        value = string.Empty;
        return false;
    }
}