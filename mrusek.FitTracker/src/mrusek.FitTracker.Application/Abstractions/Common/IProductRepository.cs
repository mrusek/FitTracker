using mrusek.FitTracker.Domain.Entities;

namespace mrusek.FitTracker.Application.Abstractions.Common;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<List<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task DeleteAsync(Product product);
    Task SaveChangesAsync();
}