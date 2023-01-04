using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.Data.Repositories;

public class OrderProductRepository : IOrderProductRepository
{
	private readonly ElectronicsStoreDbContext _context;

	public OrderProductRepository(ElectronicsStoreDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(OrderProduct orderProduct)
	{
		await _context.OrderProduct.AddAsync(orderProduct);
	}

    public async Task<IEnumerable<OrderProduct>> GetByOrderIdAsync(int orderId)
    {
        return await _context.OrderProduct
            .Where(op => op.OrderId == orderId)
            .Include(op => op.Product)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderProduct>> GetByUserIdAsync(int userId)
    {
        return await _context.OrderProduct
            .Where(op => op.Order.UserId == userId)
            .Include(op => op.Order)
            .Include(op => op.Product)
            .ToListAsync();
    }
}
