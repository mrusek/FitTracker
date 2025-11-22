using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mrusek.FitTracker.Domain.Entities;

namespace mrusek.FitTracker.Infrastructure.Persistence.Configuration;

public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity
{
    protected const string DecimalColumnType = "decimal(18,2)";

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.CreatedOn).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
    }
}