using Microsoft.EntityFrameworkCore;
using mrusek.FitTracker.Domain.Entities;

namespace mrusek.FitTracker.Infrastructure.Persistence;

internal abstract class Repository<TEntity>(ApplicationDbContext dbContext)
    where TEntity : Entity
{
    protected readonly ApplicationDbContext _dbContext = dbContext;

    public void Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    public ValueTask<TEntity?> FindByIdAsync(Guid id)
    {
        return _dbContext.Set<TEntity>().FindAsync(id);
    }
}