using System.Security.Claims;

namespace ElectronicsStore.Services.Interfaces;
public interface IUserContextService
{
    int? GetUserId { get; }
    ClaimsPrincipal? User { get; }
}