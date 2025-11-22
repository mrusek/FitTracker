using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mrusek.FitTracker.Domain.Entities;
using mrusek.FitTracker.Domain.Enums;

namespace mrusek.FitTracker.Infrastructure.Persistence.Configuration;

public class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PortionSize).IsRequired().HasColumnType(DecimalColumnType);
        builder.Property(x => x.PortionTypes).IsRequired().HasConversion<int>();
        builder.HasOne(x => x.Nutrients)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.Id);
        builder.Property(x => x.Benefits).IsRequired().HasConversion<string>(
            v => string.Join(";", v.Select(c => c.ToString())),
            x => x.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(w => Enum.Parse<Benefits>(w)).ToList());
    }
}