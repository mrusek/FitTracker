using mrusek.FitTracker.Domain.Enums;

namespace mrusek.FitTracker.Domain.Entities;

public class AuditTrail : Entity
{
    public Guid? UserId { get; set; }

    public User? User { get; set; }

    public TrailTypes TrailType { get; set; }

    public DateTime DateUtc { get; set; }

    public required string EntityName { get; set; }

    public string? PrimaryKey { get; set; }

    public Dictionary<string, object?> OldValues { get; set; } = [];

    public Dictionary<string, object?> NewValues { get; set; } = [];

    public List<string> ChangedColumns { get; set; } = [];
}