namespace ElectronicsStore.Data.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime TimeOrdered { get; set; }
    public DateTime? TimeAccepted { get; set; }
    public decimal TotalPrice { get; set; }

    virtual public User User { get; set; }
    virtual public List<OrderProduct> OrderProducts { get; set; }
}
