namespace mrusek.FitTracker.Domain.Entities;

public class User : AuditableEntity
{
    public string Name { get; set; }
    
    public string? Email { get; set; }
    
}