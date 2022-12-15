using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.Data.Repositories;

public class BrandRepository : IBrandRepository
{
	private readonly ElectronicsStoreDbContext _context;

	public BrandRepository(ElectronicsStoreDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(Brand brand)
	{
		await _context.Brands.AddAsync(brand);
	}

	public async Task<IEnumerable<Brand>> GetAllAsync()
	{
        return await _context.Brands.ToListAsync();
    }

	public async Task<Brand?> GetByIdAsync(int id)
	{
		return await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
	}

    public void Remove(Brand brand)
	{
		_context.Brands.Remove(brand);
	}
}
