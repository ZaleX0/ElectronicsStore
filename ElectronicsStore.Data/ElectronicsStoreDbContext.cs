using ElectronicsStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.Data;

public class ElectronicsStoreDbContext : DbContext
{
	public ElectronicsStoreDbContext(DbContextOptions options)
		: base(options)
	{
	}

	public DbSet<Brand> Brands { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderProduct> OrderProduct { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
		modelBuilder.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });
	}
}
