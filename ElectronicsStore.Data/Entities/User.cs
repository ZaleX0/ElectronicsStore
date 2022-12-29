namespace ElectronicsStore.Data.Entities;

public class User
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    virtual public Role Role { get; set; }
}
