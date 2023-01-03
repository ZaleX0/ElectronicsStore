using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<Order>> GetAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.User)
            .ToListAsync();
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
}
