using ElectronicsStore.Data;
using ElectronicsStore.Data.Entities;

namespace ElectronicsStore.Services;

public class BrandAndProductSeeder
{
	private readonly ElectronicsStoreDbContext _context;

	public BrandAndProductSeeder(ElectronicsStoreDbContext context)
	{
		_context = context;
	}

	public void Seed()
	{
		if (!_context.Database.CanConnect())
			return;

		if (!_context.Brands.Any())
		{
			var brands = GetBrands();
            var products = GetProducts();
			_context.Brands.AddRange(brands);
            _context.Products.AddRange(products);
			_context.SaveChanges();
        }
    }

	private IEnumerable<Brand> GetBrands()
	{
		return new List<Brand>
		{
			new Brand { Name = "Brand One" },
			new Brand { Name = "Brand Two" },
			new Brand { Name = "Brand Three" },
		};
	}

    private IEnumerable<Product> GetProducts()
    {
		return new List<Product>
		{
			new Product { BrandId = 1, Price = 100.00m, Name = "Product Name 1", Description = "Product 1 Description" },
			new Product { BrandId = 1, Price = 200.00m, Name = "Product Name 2", Description = "Product 2 Description" },
			new Product { BrandId = 1, Price = 300.00m, Name = "Product Name 3", Description = "Product 3 Description" },
			new Product { BrandId = 2, Price = 400.00m, Name = "Product Name 4", Description = "Product 4 Description" },
			new Product { BrandId = 2, Price = 500.00m, Name = "Product Name 5", Description = "Product 5 Description" },
			new Product { BrandId = 2, Price = 600.00m, Name = "Product Name 6", Description = "Product 6 Description" },
			new Product { BrandId = 3, Price = 700.00m, Name = "Product Name 7", Description = "Product 7 Description" },
			new Product { BrandId = 3, Price = 800.00m, Name = "Product Name 8", Description = "Product 8 Description" },
			new Product { BrandId = 3, Price = 900.00m, Name = "Product Name 9", Description = "Product 9 Description" },
		};
    }
}
