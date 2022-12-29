namespace ElectronicsStore.Services.Models;

public class UserDto
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
