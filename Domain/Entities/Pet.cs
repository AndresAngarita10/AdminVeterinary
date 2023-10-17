
namespace Domain.Entities;

public class Pet : BaseEntity
{
    public string Name { get; set; }
    public DateOnly DateBirth { get; set; }
    public int UserOwnerId { get; set; }
    public Partner Owner { get; set; }
    public int SpeciesIdFk { get; set; }
    public Specie Specie { get; set; }
    public int BreedIdFk { get; set; } //Raza
    public Breed Breed { get; set; }
    public ICollection<Quote> Quotes { get; set; }
}
