﻿using ElectronicsStore.Data.Interfaces;

namespace ElectronicsStore.Data;

public class ElectronicsStoreUnitOfWork : IElectronicsStoreUnitOfWork
{
    private readonly ElectronicsStoreDbContext _context;

    public ElectronicsStoreUnitOfWork(
        ElectronicsStoreDbContext context,
        IUserRepository users,
        IBrandRepository brands,
        IProductRepository products)
    {
        _context = context;
        Users = users;
        Brands = brands;
        Products = products;
    }

    public IUserRepository Users { get; }
    public IBrandRepository Brands { get; }
    public IProductRepository Products { get; }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
