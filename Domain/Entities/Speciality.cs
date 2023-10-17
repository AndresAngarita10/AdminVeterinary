
namespace Domain.Entities;

public class Speciality : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Partner> Partners { get; set; }
}
