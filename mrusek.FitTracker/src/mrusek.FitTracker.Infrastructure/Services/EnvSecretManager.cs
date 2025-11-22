using mrusek.FitTracker.Domain.Interfaces;

namespace mrusek.FitTracker.Infrastructure.Services;

public class EnvSecretManager : ISecretManager
{
    public Task<bool> TryGetValueAsync(string key, out string value)
    {
        var envVal = Environment.GetEnvironmentVariable(key);
        value = string.Empty;
        if (envVal is null)
            return Task.FromResult(false);
        value = envVal;
        return Task.FromResult(true);
    }

    public bool TryGetValue(string key, out string value)
    {
        var envVal = Environment.GetEnvironmentVariable(key);
        value = string.Empty;
        if (envVal is null)
            return false;
        value = envVal;
        return true;
    }
}