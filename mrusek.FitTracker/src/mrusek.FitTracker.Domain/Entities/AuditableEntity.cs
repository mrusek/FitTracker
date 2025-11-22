namespace mrusek.FitTracker.Domain.Entities;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = null!;
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
}