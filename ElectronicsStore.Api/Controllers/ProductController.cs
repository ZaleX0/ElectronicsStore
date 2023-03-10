using ElectronicsStore.Data.Queries;
using ElectronicsStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ProductQuery query)
    {
        var pagedProducts = await _service.GetAll(query);
        return Ok(pagedProducts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetById(id);
        return Ok(product);
    }
}
