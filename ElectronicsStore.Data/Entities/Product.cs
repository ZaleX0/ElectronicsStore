namespace ElectronicsStore.Data.Entities;

public class Product
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }

    virtual public Brand Brand { get; set; }
    virtual public Category Category { get; set; }

    virtual public List<OrderProduct> OrderProducts { get; set; }
}
