using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAll();
    Task<ProductDto> GetById(int id);
}
