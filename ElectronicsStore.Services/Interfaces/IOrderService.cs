using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services.Interfaces;

public interface IOrderService
{
    Task MakeOrder(IEnumerable<CreateOrderProductDto> dtos);
    Task<IEnumerable<OrderDto>> GetAll();
    Task<IEnumerable<OrderDto>> GetForUser();
    Task Accept(int orderId);
    Task Cancel(int orderId);
}
