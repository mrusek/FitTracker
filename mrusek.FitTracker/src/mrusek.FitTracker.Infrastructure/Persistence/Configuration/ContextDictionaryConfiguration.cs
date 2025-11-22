using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mrusek.FitTracker.Domain.Entities;

namespace mrusek.FitTracker.Infrastructure.Persistence.Configuration;

public class ContextDictionaryConfiguration:BaseConfiguration<ContextDictionary>
{
    public override void Configure(EntityTypeBuilder<ContextDictionary> builder)
    {
        base.Configure(builder);
        builder.ToTable("ContextDictionary");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(2000);
        builder.Property(x => x.IsActive);
        
        builder.HasMany(o => o.DictionaryItems)
            .WithOne(o => o.Dictionary)
            .HasForeignKey(x=>x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}