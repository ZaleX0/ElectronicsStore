using ElectronicsStore.Data.Entities;

namespace ElectronicsStore.Data.Interfaces;

public interface IOrderProductRepository
{
    Task AddAsync(OrderProduct orderProduct);
    Task<IEnumerable<OrderProduct>> GetByOrderIdAsync(int orderId);
    Task<IEnumerable<OrderProduct>> GetByUserIdAsync(int userId);
}