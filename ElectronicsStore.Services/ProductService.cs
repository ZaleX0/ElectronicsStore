using AutoMapper;
using ElectronicsStore.Data.Interfaces;
using ElectronicsStore.Data.Queries;
using ElectronicsStore.Services.Exceptions;
using ElectronicsStore.Services.Interfaces;
using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services;

public class ProductService : IProductService
{
    private readonly IElectronicsStoreUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IElectronicsStoreUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<ProductDto>> GetAll(ProductQuery query)
    {
        var products = await _unitOfWork.Products.GetAllAsync(query);
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        var totalCount = await _unitOfWork.Products.CountAsync(query);

        return new PagedResult<ProductDto>(productDtos, totalCount, query.PageSize, query.PageNumber);
    }

    public async Task<ProductDto> GetById(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
            throw new NotFoundException("Product not found");

        var productDto = _mapper.Map<ProductDto>(product);
        return productDto;
    }
}
