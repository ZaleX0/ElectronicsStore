using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;

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
}
