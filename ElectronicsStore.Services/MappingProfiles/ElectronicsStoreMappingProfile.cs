using AutoMapper;
using ElectronicsStore.Data.Entities;
using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services.MappingProfiles;

public class ElectronicsStoreMappingProfile : Profile
{
	public ElectronicsStoreMappingProfile()
	{
		CreateMap<Product, ProductDto>()
			.ForMember(d => d.BrandName, c => c.MapFrom(s => s.Brand.Name));

		CreateMap<User, UserDto>()
			.ForMember(d => d.RoleName, c => c.MapFrom(s => s.Role.Name));
	}
}
