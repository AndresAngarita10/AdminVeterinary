
namespace Domain.Entities;
//Csv ok y Controller ok
public class Breed : BaseEntity //Raza ok
{
    public string Name { get; set; }
    public ICollection<Pet> Pets { get; set; }
}
