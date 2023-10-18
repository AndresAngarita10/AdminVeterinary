
using Domain.Entities;

namespace Domain.Interfaces;

public interface IPartnerRepository : IGenericRepository<Partner>
{
    abstract Task<IEnumerable<object>> VeterinarioCirujanoVascular();
    abstract Task<(int totalRegistros, List<Partner> registros)> VeterinarioCirujanoVascular(int pageIndex, int pageSize, string search);
    abstract Task<IEnumerable<object>> PropietarioYMascota();
    abstract Task<(int totalRegistros, Object registros)> PropietarioYMascota(int pageIndex, int pageSize, string search);


}
