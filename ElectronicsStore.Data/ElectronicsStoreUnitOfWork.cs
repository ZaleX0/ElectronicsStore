using ElectronicsStore.Data.Interfaces;

namespace ElectronicsStore.Data;

public class ElectronicsStoreUnitOfWork : IElectronicsStoreUnitOfWork
{
    private readonly ElectronicsStoreDbContext _context;

    public ElectronicsStoreUnitOfWork(
        ElectronicsStoreDbContext context,
        IUserRepository users,
        IBrandRepository brands,
        IProductRepository products,
        ICategoryRepository categories,
        IOrderRepository orders,
        IOrderProductRepository orderProducts)
    {
        _context = context;
        Users = users;
        Brands = brands;
        Products = products;
        Categories = categories;
        Orders = orders;
        OrderProducts = orderProducts;
    }

    public IUserRepository Users { get; }
    public IBrandRepository Brands { get; }
    public IProductRepository Products { get; }
    public ICategoryRepository Categories { get; }
    public IOrderRepository Orders { get; }
    public IOrderProductRepository OrderProducts { get; }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
