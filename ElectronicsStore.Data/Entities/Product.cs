namespace ElectronicsStore.Data.Entities;

public class Product
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    virtual public Brand Brand { get; set; }
}
