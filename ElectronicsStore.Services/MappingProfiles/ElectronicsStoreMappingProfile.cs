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

		CreateMap<Category, CategoryDto>();

		CreateMap<User, UserDto>()
			.ForMember(d => d.RoleName, c => c.MapFrom(s => s.Role.Name));

		CreateMap<Order, OrderDto>()
			.ForMember(d => d.UserEmail, c => c.MapFrom(s => s.User.Email))
			.ForMember(d => d.Products, c => c.MapFrom(s => s.OrderProducts.Select(op => op.Product)))
			.ForMember(d => d.IsAccepted, c => c.MapFrom(s => s.TimeAccepted != null));
	}
}
