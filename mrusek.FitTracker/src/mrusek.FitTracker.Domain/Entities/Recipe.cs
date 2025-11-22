using mrusek.FitTracker.Domain.Enums;

namespace mrusek.FitTracker.Domain.Entities;

public class Recipe : AuditableEntity
{
    public ICollection<Benefits>? Benefits { get; set; }

    public ICollection<Product> Products { get; set; } = null!;

    public ICollection<ContextDictionaryItem> Tags { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public ICollection<string> Steps { get; set; } = null!;
}