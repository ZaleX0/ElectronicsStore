using ElectronicsStore.Services.Models;

namespace ElectronicsStore.Services.Interfaces;
public interface IAuthorizationService
{
    Task<string> GenerateJwt(LoginDto dto);
    Task RegisterUser(RegisterDto dto);
}