
namespace Domain.Entities;

public class Breed : BaseEntity //Raza
{
    public string Name { get; set; }
    public ICollection<Pet> Pets { get; set; }
}
