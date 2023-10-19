
namespace Domain.Entities;
//Csv ok y Controller ok
public class Partner : BaseEntity//ok
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int SpecialtyIdFk { get; set; }
    public Speciality Specialty { get; set; }
    public int GenderIdFk { get; set; }
    public Gender Gender { get; set; }
    public int PartnerTypeIdFk { get; set; }
    public PartnerType PartnerType { get; set; }
    public ICollection<MedicinePartner> MedicinePartners { get; set; }
    public ICollection<Pet> Pets { get; set; }
    public ICollection<Quote> Quotes { get; set; }
    public ICollection<MedicineMovement> MedicineMovements { get; set; }
}
