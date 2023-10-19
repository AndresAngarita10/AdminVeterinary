
namespace Domain.Entities;
// Controller ok

public class MedicalTreatment : BaseEntity//ok
{
    public DateOnly DateStartTreatment { get; set; }
    public int QuoteIdFk { get; set; }
    public Quote Quote { get; set; }
    public ICollection<DescriptionMedicalTreatment> DescriptionMedicalTreatments { get; set; }
}
