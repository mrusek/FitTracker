using mrusek.FitTracker.Application.Abstractions.Common;
using mrusek.FitTracker.Domain.Entities;

namespace mrusek.FitTracker.Infrastructure.Persistence;

internal sealed class ProductRepository(ApplicationDbContext dbContext) : Repository<Product>(dbContext), IProductRepository
{
    
    public Task<Product?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Product product)
    {
        throw new NotImplementedException();
    }
}