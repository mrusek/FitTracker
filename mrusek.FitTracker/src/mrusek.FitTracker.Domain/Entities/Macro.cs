using mrusek.FitTracker.Domain.Enums;

namespace mrusek.FitTracker.Domain.Entities;

public class Macro : AuditableEntity
{
    public PortionTypes PortionTypes { get; set; }

    public decimal PortionSize { get; set; }

    public decimal Fats { get; set; }

    public decimal SaturatedFats { get; set; }

    public decimal Proteins { get; set; }
    
    public decimal Carbs { get; set; }
    
    public decimal SaturatedCarbs { get; set; }
    
    public decimal Calories { get; set; }
    
    public decimal Salt { get; set; }
    
    public ICollection<Product>? Products { get; set; }
}