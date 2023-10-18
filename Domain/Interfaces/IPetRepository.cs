
using Domain.Entities;

namespace Domain.Interfaces;

public interface IPetRepository : IGenericRepository<Pet>
{
    abstract Task<IEnumerable<Object>> MascotasFelinos();
    abstract Task<(int totalRegistros, object registros)> MascotasFelinos(int pageIndex, int pageSize, string search);
    abstract Task<IEnumerable<Object>> MascotasVacunadasPrimerTrim2023();
    abstract Task<(int totalRegistros, IEnumerable<object> registros)> MascotasVacunadasPrimerTrim2023(int pageIndex, int pageSize, string search);
    abstract Task<IEnumerable<object>> EspecieYMascota();
    abstract Task<(int totalRegistros, IEnumerable<object> registros)> EspecieYMascota(int pageIndex, int pageSize, string search);



}
