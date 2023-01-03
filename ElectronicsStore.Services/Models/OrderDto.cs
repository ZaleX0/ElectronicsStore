using ElectronicsStore.Data.Entities;

namespace ElectronicsStore.Services.Models;

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserEmail { get; set; }
    public string Status { get; set; }
    public DateTime TimeOrdered { get; set; }
    public DateTime? TimeAccepted { get; set; }
    public List<ProductDto> Products { get; set; }
}
