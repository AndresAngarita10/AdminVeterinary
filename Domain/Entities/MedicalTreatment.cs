
namespace Domain.Entities;

public class MedicalTreatment : BaseEntity
{
    public DateOnly DateStartTreatment { get; set; }
    public int QuoteIdFk { get; set; }
    public Quote Quote { get; set; }
    public ICollection<DescriptionMedicalTreatment> DescriptionMedicalTreatments { get; set; }
}
