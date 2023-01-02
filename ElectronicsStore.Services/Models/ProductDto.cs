namespace ElectronicsStore.Services.Models;

public class ProductDto
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public string BrandName { get; set; }
    public string CategoryName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
}
