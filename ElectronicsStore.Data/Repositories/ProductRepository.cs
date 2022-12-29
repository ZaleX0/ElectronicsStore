using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;
using ElectronicsStore.Data.Queries;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    public async Task<IEnumerable<Product>> GetAllAsync(ProductQuery query)
    {
        var baseQuery = _context.Products
            .Include(p => p.Brand)
            .Where(p => query.SearchPhrase == null
                || p.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                || p.Description.ToLower().Contains(query.SearchPhrase.ToLower()));

        if (!string.IsNullOrEmpty(query.SortBy))
        {
            var columnSelectors = new Dictionary<string, Expression<Func<Product, object>>>
            {
                { nameof(Product.Name), r => r.Name },
                { nameof(Product.Price), r => r.Price },
                { nameof(Product.Brand), r => r.Brand.Name },
                { nameof(Product.Category), r => r.Category.Name },
            };
            var selectedColumn = columnSelectors[query.SortBy];
            baseQuery = query.SortDirection == SortDirection.ASC
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        return await baseQuery
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync(ProductQuery query)
    {
        return await _context.Products
            .Where(p => query.SearchPhrase == null
                || p.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                || p.Description.ToLower().Contains(query.SearchPhrase.ToLower()))
            .CountAsync();
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
