
namespace Domain.Entities;
//Csv ok y Controller ok
public class DetailMovement : BaseEntity//ok
{
    public int Quantity { get; set; }
    public double Price { get; set; }
    public int MedicineIdFk { get; set; }
    public Medicine Medicine { get; set; }
    public int MedicineMovementIdFk { get; set; }
    public MedicineMovement MedicineMovement { get; set; }
}
