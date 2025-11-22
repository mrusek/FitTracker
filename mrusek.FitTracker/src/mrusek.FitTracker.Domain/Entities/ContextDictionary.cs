namespace mrusek.FitTracker.Domain.Entities;

public class ContextDictionary : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public bool IsActive { get; set; }
    
    public ICollection<ContextDictionaryItem>? DictionaryItems { get; set; }
}