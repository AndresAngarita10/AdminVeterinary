
using Domain.Entities;

namespace Domain.Interfaces;

public interface IDetailMovementRepository : IGenericRepository<DetailMovement>
{
    public Task<IEnumerable<Object>> SumTotalCadaMov();

}
