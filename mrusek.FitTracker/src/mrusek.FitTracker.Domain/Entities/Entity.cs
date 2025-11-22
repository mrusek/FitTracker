namespace mrusek.FitTracker.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
}