using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mrusek.FitTracker.Domain.Entities;

namespace mrusek.FitTracker.Infrastructure.Persistence.Configuration;

public class RecipeConfiguration:BaseConfiguration<Recipe>
{
    public override void Configure(EntityTypeBuilder<Recipe> builder)
    {
        base.Configure(builder);
        builder.ToTable("Recipe");
        builder.Property(x => x.Id);
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x=>x.Description).HasMaxLength(2000);
    }
}