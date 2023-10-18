
namespace Domain.Entities;
//Csv ok y Controller ok
public class MedicineMovement : BaseEntity
{
    public int Quantity { get; set; }
    public DateOnly DateMovement { get; set; }
    public int TypeMovementFk { get; set; }
    public TypeMovement TypeMovement { get; set; }
    public int PartnerIdFk { get; set; }
    public Partner Partner { get; set; }
    public ICollection<DetailMovement> DetailMovements { get; set; }
}
