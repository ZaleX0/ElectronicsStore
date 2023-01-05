using ElectronicsStore.Data.Queries;
using ElectronicsStore.Services.Interfaces;
using ElectronicsStore.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ElectronicsStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> MakeOrder(IEnumerable<CreateOrderProductDto> dtos)
    {
        await _service.MakeOrder(dtos);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] OrderQuery query)
    {
        var pagedOrders = await _service.GetForUser(query);
        return Ok(pagedOrders);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll([FromQuery] OrderQuery query)
    {
        var pagedOrders = await _service.GetAll(query);
        return Ok(pagedOrders);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AcceptOrder(int id)
    {
        await _service.Accept(id);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        await _service.Cancel(id);
        return Ok();
    }
}
