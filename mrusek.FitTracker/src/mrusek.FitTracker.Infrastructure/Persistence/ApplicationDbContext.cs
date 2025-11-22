using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using mrusek.FitTracker.Domain.Entities;
using mrusek.FitTracker.Domain.Enums;
using mrusek.FitTracker.Domain.Interfaces;

namespace mrusek.FitTracker.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentSessionProvider currentSessionProvider) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<ContextDictionary> Dictionaries => Set<ContextDictionary>();
    public DbSet<ContextDictionaryItem> DictionaryItems => Set<ContextDictionaryItem>();
    public DbSet<Macro> Macros => Set<Macro>(); 
    public DbSet<AuditTrail> AuditTrails => Set<AuditTrail>();
    public DbSet<User> Users => Set<User>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new ())
    {
        var userId = currentSessionProvider.GetUserId();
        SetAuditableProperties(userId);
        
        var auditEntries = HandleAuditingBeforeSaveChanges(userId).ToList();
        if (auditEntries.Count > 0)
        {
            await AuditTrails.AddRangeAsync(auditEntries, cancellationToken);
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    private void SetAuditableProperties(Guid? userId)
    {
        const string systemSource = "system";
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = userId?.ToString() ?? systemSource;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId?.ToString() ?? systemSource;
                    break;
            }
        }
    }
    private List<AuditTrail> HandleAuditingBeforeSaveChanges(Guid? userId)
    {
        var auditableEntries = ChangeTracker.Entries<AuditableEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Deleted or EntityState.Modified)
            .Select(x => CreateTrailEntry(userId, x))
            .ToList();

        return auditableEntries;
    }
    private static AuditTrail CreateTrailEntry(Guid? userId, EntityEntry<AuditableEntity> entry)
    {
        var trailEntry = new AuditTrail
        {
            Id = Guid.NewGuid(),
            EntityName = entry.Entity.GetType().Name,
            UserId = userId,
            DateUtc = DateTime.UtcNow
        };

        SetAuditTrailPropertyValues(entry, trailEntry);
        SetAuditTrailNavigationValues(entry, trailEntry);
        SetAuditTrailReferenceValues(entry, trailEntry);

        return trailEntry;
    }
    private static void SetAuditTrailReferenceValues(EntityEntry entry, AuditTrail trailEntry)
    {
        foreach (var reference in entry.References.Where(x => x.IsModified))
        {
            var referenceName = reference.EntityEntry.Entity.GetType().Name;
            trailEntry.ChangedColumns.Add(referenceName);
        }
    }

    private static void SetAuditTrailNavigationValues(EntityEntry entry, AuditTrail trailEntry)
    {
        foreach (var navigation in entry.Navigations.Where(x => x.Metadata.IsCollection && x.IsModified))
        {
            if (navigation.CurrentValue is not IEnumerable<object> enumerable)
            {
                continue;
            }

            var collection = enumerable.ToList();
            if (collection.Count == 0)
            {
                continue;
            }

            var navigationName = collection.First().GetType().Name;
            trailEntry.ChangedColumns.Add(navigationName);
        }
    }

    
    private static void SetAuditTrailPropertyValues(EntityEntry entry, AuditTrail trailEntry)
    {
        // Skip temp fields (that will be assigned automatically by ef core engine, for example: when inserting an entity
        foreach (var property in entry.Properties.Where(x => !x.IsTemporary))
        {
            if (property.Metadata.IsPrimaryKey())
            {
                trailEntry.PrimaryKey = property.CurrentValue?.ToString();
                continue;
            }

            // Filter properties that should not appear in the audit list
            if (property.Metadata.Name.Equals("PasswordHash"))
            {
                continue;
            }

            SetAuditTrailPropertyValue(entry, trailEntry, property);
        }
    }

    private static void SetAuditTrailPropertyValue(EntityEntry entry, AuditTrail trailEntry, PropertyEntry property)
    {
        var propertyName = property.Metadata.Name;

        switch (entry.State)
        {
            case EntityState.Added:
                trailEntry.TrailType = TrailTypes.Create;
                trailEntry.NewValues[propertyName] = property.CurrentValue;

                break;

            case EntityState.Deleted:
                trailEntry.TrailType = TrailTypes.Delete;
                trailEntry.OldValues[propertyName] = property.OriginalValue;

                break;

            case EntityState.Modified:
                if (property.IsModified && (property.OriginalValue is null || !property.OriginalValue.Equals(property.CurrentValue)))
                {
                    trailEntry.ChangedColumns.Add(propertyName);
                    trailEntry.TrailType = TrailTypes.Update;
                    trailEntry.OldValues[propertyName] = property.OriginalValue;
                    trailEntry.NewValues[propertyName] = property.CurrentValue;
                }

                break;
        }

        if (trailEntry.ChangedColumns.Count > 0)
        {
            trailEntry.TrailType = TrailTypes.Update;
        }
    }
}