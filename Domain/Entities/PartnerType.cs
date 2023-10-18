
namespace Domain.Entities;
//Csv ok y Controller ok
public class PartnerType : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Partner> Partners { get; set; }
}
