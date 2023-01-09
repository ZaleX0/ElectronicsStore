using AutoMapper;
using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;
using ElectronicsStore.Data.Queries;
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
            UserId = GetUserId(),
            TotalPrice = await CalculateOrderTotalPriceAsync(dtos),
            TimeOrdered = DateTime.Now
        };
        await _unitOfWork.Orders.AddAsync(order);

        // Add OrderProducts
        foreach (var dto in dtos)
        {
            var orderProduct = new OrderProduct
            {
                Order = order,
                ProductId = dto.Id,
                Quantity = dto.Quantity,
            };
            await _unitOfWork.OrderProducts.AddAsync(orderProduct);
        }
        await _unitOfWork.CommitAsync();
    }

    public async Task<PagedResult<OrderDto>> GetAll(OrderQuery query)
    {
        var orders = await _unitOfWork.Orders.GetAllAsync(query);
        var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

        var totalCount = await _unitOfWork.Orders.CountAsync(query);

        return new PagedResult<OrderDto>(orderDtos, totalCount, query.PageSize, query.PageNumber);
    }

    public async Task<PagedResult<OrderDto>> GetForUser(OrderQuery query)
    {
        var userId = GetUserId();
        var orders = await _unitOfWork.Orders.GetByUserIdAsync(query, userId);
        var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

        var totalCount = await _unitOfWork.Orders.CountByUserIdAsync(query, userId);

        return new PagedResult<OrderDto>(orderDtos, totalCount, query.PageSize, query.PageNumber);
    }

    public async Task Accept(int orderId)
    {
        var order = await GetOrderAsync(orderId);
        order.TimeAccepted = DateTime.Now;
        _unitOfWork.Orders.Update(order);
        await _unitOfWork.CommitAsync();
    }

    public async Task Cancel(int orderId)
    {
        var order = await GetOrderAsync(orderId);
        _unitOfWork.Orders.Remove(order);
        await _unitOfWork.CommitAsync();
    }

    private int GetUserId()
    {
        var userId = _userContextService.GetUserId;
        if (userId == null)
            throw new NotFoundException("User not found");
        return (int) userId;
    }

    private async Task<Order> GetOrderAsync(int id)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(id);
        if (order == null)
            throw new NotFoundException("Order not found");
        return order;
    }

    private async Task<decimal> CalculateOrderTotalPriceAsync(IEnumerable<CreateOrderProductDto> dtos)
    {
        decimal totalPrice = 0.00m;
        foreach (var dto in dtos)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(dto.Id);
            if (product == null)
                throw new NotFoundException($"Product (Id: {dto.Id}) not found");

            totalPrice += product.Price * dto.Quantity;
        }
        return totalPrice;
    }
}
