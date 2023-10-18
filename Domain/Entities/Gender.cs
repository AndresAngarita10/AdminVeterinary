
namespace Domain.Entities;
//Csv ok y Controller ok
public class Gender : BaseEntity
{
    public string Name { get; set; }
    public ICollection<User> Users { get; set; }
    public ICollection<Partner> Partners { get; set; }
    /* 
    Male: Masculino.
    Female: Femenino 2:35
    */
}
