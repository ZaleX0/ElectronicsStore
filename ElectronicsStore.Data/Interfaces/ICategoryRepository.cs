using ElectronicsStore.Data.Entities;

namespace ElectronicsStore.Data.Interfaces;
public interface ICategoryRepository
{
    Task AddAsync(Category category);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
}