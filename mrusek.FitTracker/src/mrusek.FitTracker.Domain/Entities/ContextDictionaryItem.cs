namespace mrusek.FitTracker.Domain.Entities;

public class ContextDictionaryItem : AuditableEntity
{
    public ContextDictionary Dictionary { get; set; }
    public string Key { get; set; }
    
    public string Value { get; set; }
    
    public bool IsActive { get; set; }
}