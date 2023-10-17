
using Domain;

namespace API.Dtos;

public class MedicalTreatmentDto : BaseEntity
{
    public DateOnly DateStartTreatment { get; set; }
    public int QuoteIdFk { get; set; }
    //public Quote Quote { get; set; }
    //public ICollection<DescriptionMedicalTreatment> DescriptionMedicalTreatments { get; set; }
}

