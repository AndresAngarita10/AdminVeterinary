
using Domain.Entities;

namespace Domain.Interfaces;

public interface IMedicineMovementRepository : IGenericRepository<MedicineMovement>
{
    abstract Task<IEnumerable<object>> MovMedicamentoYTotal();
    abstract public Task<(int totalRegistros, IEnumerable<Object> registros)> MovMedicamentoYTotal(int pageIndex, int pageSize, string search);

}
