using AutoMapper;
using ElectronicsStore.Data.Entities;
using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services.MappingProfiles;

public class ElectronicsStoreMappingProfile : Profile
{
    public ElectronicsStoreMappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.BrandName, c => c.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.CategoryName, c => c.MapFrom(s => s.Category.Name));

        CreateMap<Product, OrderProductDto>()
            .ForMember(d => d.BrandName, c => c.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.CategoryName, c => c.MapFrom(s => s.Category.Name));

        CreateMap<OrderProduct, OrderProductDto>()
            .ForMember(d => d.Id, c => c.MapFrom(s => s.Product.Id))
            .ForMember(d => d.BrandId, c => c.MapFrom(s => s.Product.BrandId))
            .ForMember(d => d.CategoryId, c => c.MapFrom(s => s.Product.CategoryId))
            .ForMember(d => d.BrandName, c => c.MapFrom(s => s.Product.Brand.Name))
            .ForMember(d => d.CategoryName, c => c.MapFrom(s => s.Product.Category.Name))
            .ForMember(d => d.Name, c => c.MapFrom(s => s.Product.Name))
            .ForMember(d => d.Price, c => c.MapFrom(s => s.Product.Price));

        CreateMap<Category, CategoryDto>();

        CreateMap<User, UserDto>()
            .ForMember(d => d.RoleName, c => c.MapFrom(s => s.Role.Name));

        CreateMap<Order, OrderDto>()
            .ForMember(d => d.UserEmail, c => c.MapFrom(s => s.User.Email))
            .ForMember(d => d.Products, c => c.MapFrom(s => s.OrderProducts))
            .ForMember(d => d.IsAccepted, c => c.MapFrom(s => s.TimeAccepted != null));
    }
}
