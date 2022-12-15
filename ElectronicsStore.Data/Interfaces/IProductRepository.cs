using ElectronicsStore.Data.Entities;

namespace ElectronicsStore.Data.Interfaces;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    void Remove(Product product);
}
