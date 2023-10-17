
namespace Domain.Entities;

public class PartnerType : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Partner> Partners { get; set; }
}
