using ElectronicsStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.Data.Repositories;

public class CategoryRepository
{
	private readonly ElectronicsStoreDbContext _context;

	public CategoryRepository(ElectronicsStoreDbContext context)
	{
		_context = context;
	}

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }
}
