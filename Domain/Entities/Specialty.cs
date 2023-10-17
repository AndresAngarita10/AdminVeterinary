
namespace Domain.Entities;

public class Specialty : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Partner> Partners { get; set; }
}
