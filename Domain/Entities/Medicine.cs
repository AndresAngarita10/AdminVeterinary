
namespace Domain.Entities;
//Csv ok y Controller ok

public class Medicine : BaseEntity
{
    public string Name { get; set; }
    public int QuantityAvalible { get; set; }//Cantidad disponible
    public double Price { get; set; }
    public int LaboratoryIdFk { get; set; }
    public Laboratory Laboratory { get; set; }
    public ICollection<DetailMovement> DetailMovements { get; set; }
    public ICollection<DescriptionMedicalTreatment> DescriptionMedicalTreatments { get; set; }
    public ICollection<MedicinePartner> MedicinePartners { get; set; }

}
