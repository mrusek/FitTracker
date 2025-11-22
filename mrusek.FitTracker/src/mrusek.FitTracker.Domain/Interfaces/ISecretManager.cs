namespace mrusek.FitTracker.Domain.Interfaces;

public interface ISecretManager
{
    Task<bool> TryGetValueAsync(string key, out string value);
    bool TryGetValue(string key, out string value);
}