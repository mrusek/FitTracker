using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mrusek.FitTracker.Domain.Entities;

namespace mrusek.FitTracker.Infrastructure.Persistence.Configuration;

public class ContextDictionaryItemConfiguration:BaseConfiguration<ContextDictionaryItem>
{
    public override void Configure(EntityTypeBuilder<ContextDictionaryItem> builder)
    {
        base.Configure(builder);
        builder.ToTable("ContextDictionaryItem");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);
        builder.Property(x=> x.Key).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Value).IsRequired().HasMaxLength(500);
        builder.Property(x => x.IsActive).IsRequired();
    }
}