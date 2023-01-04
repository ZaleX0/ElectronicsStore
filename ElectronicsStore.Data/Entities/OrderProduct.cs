namespace ElectronicsStore.Data.Entities;

public class OrderProduct
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    virtual public Order Order { get; set; }
    virtual public Product Product { get; set; }
}
