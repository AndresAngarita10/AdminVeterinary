
using Domain;

namespace API.Dtos;

public class DescriptionMedicalTreatmentDto: BaseEntity
{
    public string Dose { get; set; } // dosis
    public DateOnly AdministrationDate { get; set; }
    public string Observation { get; set; }
    public int MedicineIdFk { get; set; }
    //public Medicine Medicine { get; set; }
    public int MedicalTreatmentIdFk { get; set; }
    //public MedicalTreatment MedicalTreatment { get; set; }
}

