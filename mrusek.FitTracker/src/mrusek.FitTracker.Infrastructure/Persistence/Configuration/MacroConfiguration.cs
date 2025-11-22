using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mrusek.FitTracker.Domain.Entities;

namespace mrusek.FitTracker.Infrastructure.Persistence.Configuration;

public class MacroConfiguration : BaseConfiguration<Macro>
{
    public override void Configure(EntityTypeBuilder<Macro> builder)
    {
        base.Configure(builder);
        builder.ToTable("Macro");
        builder.Property(x => x.Id);
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Calories).IsRequired().HasColumnType(DecimalColumnType);
        builder.Property(x=>x.Carbs).IsRequired().HasColumnType(DecimalColumnType);
        builder.Property(x=>x.Fats).IsRequired().HasColumnType(DecimalColumnType);
        builder.Property(x=>x.SaturatedFats).IsRequired().HasColumnType(DecimalColumnType);
        builder.Property(x=>x.PortionSize).IsRequired().HasColumnType(DecimalColumnType);
        builder.Property(x=>x.Proteins).IsRequired().HasColumnType(DecimalColumnType);
        builder.Property(x=>x.Salt).IsRequired().HasColumnType(DecimalColumnType);
        builder.Property(x=>x.SaturatedCarbs).IsRequired().HasColumnType(DecimalColumnType);
        builder.Property(x => x.PortionTypes).HasConversion<int>().IsRequired();
    }
}