using ElectronicsStore.Data.Queries;
using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services.Interfaces;

public interface IOrderService
{
    Task MakeOrder(IEnumerable<CreateOrderProductDto> dtos);
    Task<PagedResult<OrderDto>> GetAll(OrderQuery query);
    Task<PagedResult<OrderDto>> GetForUser(OrderQuery query);
    Task Accept(int orderId);
    Task Cancel(int orderId);
}
