using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Queries;

namespace ElectronicsStore.Data.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllAsync(OrderQuery query);
    Task<IEnumerable<Order>> GetByUserIdAsync(OrderQuery query, int userId);
    void Remove(Order order);
    void Update(Order order);
    Task<int> CountAsync(OrderQuery query);
    Task<int> CountByUserIdAsync(int userId);
}