
namespace Domain.Entities;
//Csv ok y Controller ok

public class Laboratory : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public ICollection<Medicine> Medicines { get; set; }
}
