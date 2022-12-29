using ElectronicsStore.Data.Queries;
using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services.Interfaces;

public interface IProductService
{
    Task<PagedResult<ProductDto>> GetAll(ProductQuery query);
    Task<ProductDto> GetById(int id);
}
