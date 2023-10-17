
using Domain;

namespace API.Dtos;

public class DetailMovementDto : BaseEntity
{
    public int Quantity { get; set; }
    public double Price { get; set; }
    public int MedicineIdFk { get; set; }
    //public Medicine Medicine { get; set; }
    public int MedicineMovementIdFk { get; set; }
    //public MedicineMovement MedicineMovement { get; set; }
}
