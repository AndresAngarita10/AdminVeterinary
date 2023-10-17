
namespace Domain.Entities;

public class UserRole
{
    public int UserIdFk { get; set; }
    public User User { get; set; }
    public int RolIdFk { get; set; }
    public Rol Rol { get; set; }
}
