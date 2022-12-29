using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services.Interfaces;
public interface IAuthorizationService
{
    Task<UserDto> GetUser(LoginDto dto);
    Task<string> GenerateJwt(LoginDto dto);
    Task RegisterUser(RegisterDto dto, int roleId = 1);
}