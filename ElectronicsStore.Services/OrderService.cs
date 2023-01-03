using AutoMapper;
using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;
using ElectronicsStore.Services.Exceptions;
using ElectronicsStore.Services.Interfaces;
using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services;

public class OrderService : IOrderService
{
	private readonly IElectronicsStoreUnitOfWork _unitOfWork;
	private readonly IUserContextService _userContextService;
	private readonly IMapper _mapper;

	public OrderService(IElectronicsStoreUnitOfWork unitOfWork, IUserContextService userContextService, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_userContextService = userContextService;
		_mapper = mapper;
	}

    public async Task MakeOrder(IEnumerable<CreateOrderProductDto> dtos)
    {
        // Add Order
        var order = new Order
        {
            UserId = _userContextService.GetUserId != null
                ? (int)_userContextService.GetUserId
                : throw new NotFoundException("User not found"),

            TimeOrdered = DateTime.Now,
        };
        await _unitOfWork.Orders.AddAsync(order);

        // Add OrderProducts
        foreach (var dto in dtos)
        {
            var orderProduct = new OrderProduct
            {
                OrderId = order.Id,
                ProductId = dto.Id,
                Quantity = dto.Quantity,
            };
            await _unitOfWork.OrderProducts.AddAsync(orderProduct);
        }

        //await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<OrderDto>> Get()
	{
		throw new NotImplementedException();
	}

    public async Task Accept(int orderId)
    {
        throw new NotImplementedException();
    }

    public async Task Cancel(int orderId)
    {
        throw new NotImplementedException();
    }
}
