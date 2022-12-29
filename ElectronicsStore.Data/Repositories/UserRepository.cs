using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace ElectronicsStore.Data.Repositories;

public class UserRepository : IUserRepository
{
	private readonly ElectronicsStoreDbContext _context;

	public UserRepository(ElectronicsStoreDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(User user)
	{
		await _context.Users.AddAsync(user);
	}

	public async Task<User?> GetByEmailAsync(string email)
	{
		return await _context.Users
			.Include(u => u.Role)
			.FirstOrDefaultAsync(u => u.Email == email);
	}
}
