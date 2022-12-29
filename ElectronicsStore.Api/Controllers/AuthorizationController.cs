using ElectronicsStore.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = ElectronicsStore.Services.Interfaces.IAuthorizationService;

namespace ElectronicsStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationService _service;

    public AuthorizationController(IAuthorizationService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterDto dto)
    {
        await _service.RegisterUser(dto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginDto dto)
    {
        var user = await _service.GetUser(dto);
        return Ok(user);
    }
}
