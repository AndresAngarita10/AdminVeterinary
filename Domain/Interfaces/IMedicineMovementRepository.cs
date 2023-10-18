
using Domain.Entities;

namespace Domain.Interfaces;

public interface IMedicineMovementRepository : IGenericRepository<MedicineMovement>
{
    public Task<IEnumerable<object>> MovMedicamentoYTotal();
}
