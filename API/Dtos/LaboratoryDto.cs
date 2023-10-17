
using Domain;

namespace API.Dtos;

public class LaboratoryDto : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    //public ICollection<Medicine> Medicines { get; set; }
}
