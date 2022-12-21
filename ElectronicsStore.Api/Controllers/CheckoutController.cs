using ElectronicsStore.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CheckoutController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Checkout(IEnumerable<CheckoutProductDto> dtos)
    {
        return Ok("Thank you for the purchase!");
    }
}
