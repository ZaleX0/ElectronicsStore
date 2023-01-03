using ElectronicsStore.Services.Interfaces;
using ElectronicsStore.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Get()
    {
        var orders = await _service.Get();
        return Ok(orders);
    }
}
