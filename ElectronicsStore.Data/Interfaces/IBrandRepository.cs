using ElectronicsStore.Data.Entities;

namespace ElectronicsStore.Data.Interfaces;

public interface IBrandRepository
{
    Task AddAsync(Brand brand);
    Task<Brand?> GetByIdAsync(int id);
    Task<IEnumerable<Brand>> GetAllAsync();
    void Remove(Brand brand);
}
