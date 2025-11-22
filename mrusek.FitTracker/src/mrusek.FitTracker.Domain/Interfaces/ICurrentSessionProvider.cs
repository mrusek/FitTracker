namespace mrusek.FitTracker.Domain.Interfaces;

public interface ICurrentSessionProvider
{
    Guid? GetUserId();
}