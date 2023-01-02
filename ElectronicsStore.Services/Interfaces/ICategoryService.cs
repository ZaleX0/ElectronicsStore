using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAll();
}
