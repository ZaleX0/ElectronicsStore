namespace ElectronicsStore.Data.Interfaces;

public interface IElectronicsStoreUnitOfWork
{
    IUserRepository Users { get; }
    IBrandRepository Brands { get; }
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }

    Task<int> CommitAsync();
}