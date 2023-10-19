
namespace Domain.Entities;
//Csv ok y Controller ok
public class TypeMovement : BaseEntity//ok
{
    public string Name { get; set; }
    public ICollection<MedicineMovement> MedicineMovements { get; set; }
}
