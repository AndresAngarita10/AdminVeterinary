
namespace Domain.Entities;
//Csv ok y Controller ok

public class Rol : BaseEntity
{
    public string Name { get; set; }
    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<UserRole> UsersRoles { get; set; } 
}
