
using Domain;

namespace API.Dtos;

public class QuoteDto : BaseEntity
{
    public DateOnly Date { get; set; }
    public TimeOnly Hour { get; set; }
    public string Reason { get; set; }
    public int PetIdFk { get; set; }
    //public Pet Pet { get; set; }
    public int VeterinarianIdFk { get; set; }
    //public Partner Veterinarian { get; set; }
    //public ICollection<MedicalTreatment> MedicalTreatments { get; set; }
}
