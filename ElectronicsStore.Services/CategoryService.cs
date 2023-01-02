using AutoMapper;
using ElectronicsStore.Data.Interfaces;
using ElectronicsStore.Services.Interfaces;
using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services;

public class CategoryService : ICategoryService
{
    private readonly IElectronicsStoreUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IElectronicsStoreUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        var categoriesDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
        return categoriesDtos;
    }
}
