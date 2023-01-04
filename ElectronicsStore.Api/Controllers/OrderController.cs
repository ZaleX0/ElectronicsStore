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

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        //var orders = await _service.GetAll();
        //return Ok(orders);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var orders = await _service.GetForUser();
        return Ok(orders);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> AcceptOrder(int id)
    {
        await _service.Accept(id);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        await _service.Cancel(id);
        return Ok();
    }
}
