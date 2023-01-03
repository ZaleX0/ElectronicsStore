using ElectronicsStore.Data.Entities;

namespace ElectronicsStore.Data.Interfaces;

public interface IOrderProductRepository
{
    Task AddAsync(OrderProduct orderProduct);
}