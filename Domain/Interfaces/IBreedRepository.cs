
using Domain.Entities;

namespace Domain.Interfaces;

public interface IBreedRepository : IGenericRepository<Breed>
{
    
    abstract Task<IEnumerable<Breed>> GetAllAsync2();
    abstract Task<(int totalRegistros, List<Breed> registros)> GetAllAsync2(int pageIndex, int pageSize, string search);

}
