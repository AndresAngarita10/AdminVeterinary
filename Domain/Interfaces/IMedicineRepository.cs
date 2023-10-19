
using Domain.Entities;

namespace Domain.Interfaces;

public interface IMedicineRepository : IGenericRepository<Medicine>
{
    abstract public Task<IEnumerable<Medicine>> MedicamentoLaboratorioGenfar();
    abstract public Task<(int totalRegistros, List<Medicine> registros)> MedicamentoLaboratorioGenfar(int pageIndex, int pageSize, string search);
    abstract Task<IEnumerable<Object>> MedicamentoMayor10Usd();
    abstract public Task<(int totalRegistros, IEnumerable<Object> registros)> MedicamentoMayor10Usd(int pageIndex, int pageSize, string search);

}
