using mrusek.FitTracker.Domain.Enums;

namespace mrusek.FitTracker.Domain.Entities;

public class Product : AuditableEntity
{
    public Macro Nutrients { get; set; } = null!;
    
    public PortionTypes PortionTypes { get; set; }
    
    public decimal PortionSize { get; set; }
    
    public string Name { get; set; }
    
    public ICollection<Benefits> Benefits { get; set; }

}