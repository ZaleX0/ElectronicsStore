using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ElectronicsStoreDbContext _context;

    public ProductRepository(ElectronicsStoreDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Brand)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
}
