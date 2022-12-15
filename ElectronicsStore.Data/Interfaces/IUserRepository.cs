using ElectronicsStore.Data.Entities;

namespace ElectronicsStore.Data.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}
