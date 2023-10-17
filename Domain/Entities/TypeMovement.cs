
namespace Domain.Entities;

public class TypeMovement : BaseEntity
{
    public string Name { get; set; }
    public ICollection<MedicineMovement> MedicineMovements { get; set; }
}
