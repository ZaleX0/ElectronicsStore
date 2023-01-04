using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ElectronicsStore.Data.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly ElectronicsStoreDbContext _context;

	public OrderRepository(ElectronicsStoreDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(Order order)
	{
		await _context.Orders.AddAsync(order);
	}

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var query = IncludeUserAndOrderProducts(_context.Orders);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
    {
        var query = _context.Orders.Where(o => o.UserId == userId);
        query = IncludeUserAndOrderProducts(query);
        return await query.ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
		return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }

    public void Remove(Order order)
    {
        _context.Orders.Remove(order);
    }

    private IQueryable<Order> IncludeUserAndOrderProducts(IQueryable<Order> query)
    {
        return query
            .Include(o => o.User)
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ThenInclude(op => op.Brand)
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ThenInclude(op => op.Category);
    }
}
