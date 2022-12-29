using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Queries;

namespace ElectronicsStore.Data.Interfaces;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync(ProductQuery query);
    void Remove(Product product);
    Task<int> CountAsync(ProductQuery query);
}
