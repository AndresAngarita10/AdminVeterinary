
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicineMovementRepository : GenericRepository<MedicineMovement>, IMedicineMovementRepository
{
    protected readonly ApiContext _context;

    public MedicineMovementRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<MedicineMovement>> GetAllAsync()
    {
        return await _context.MedicineMovements
        .ToListAsync();
    }

    public override async Task<MedicineMovement> GetByIdAsync(int id)
    {
        return await _context.MedicineMovements
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<(int totalRegistros, IEnumerable<MedicineMovement> registros)> GetAllAsync(int pageIndez, int pageSize, int search)
    {
        var query = _context.MedicineMovements as IQueryable<MedicineMovement>;

        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.Id == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            /* .Include(p => p) */
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public async Task<IEnumerable<object>> MovMedicamentoYTotal()
    {
        return await (
            from me in _context.MedicineMovements
            join type in _context.TypeMovements on me.TypeMovementFk equals type.Id
            select new
            {
                MovementNumber = me.Id,
                TypeMovement = type.Name,
                DetailMov = (
                    from det in _context.DetailMovements
                    join med in _context.Medicines on det.MedicineIdFk equals med.Id
                    where det.MedicineMovementIdFk == me.Id
                    select new
                    {
                        NumberDescription = det.Id,
                        MedicamentName = med.Name,
                        Quantity = det.Quantity,
                        PriceUnit = det.Price,
                        Check = det.Quantity * det.Price
                    }
                ).ToList()
            }
        ).OrderBy(m => m.TypeMovement).ToListAsync();
    }

    public async Task<(int totalRegistros, IEnumerable<Object> registros)> MovMedicamentoYTotal(int pageIndex, int pageSize, string search)
    {
        var query = from me in _context.MedicineMovements
                    join type in _context.TypeMovements on me.TypeMovementFk equals type.Id
                    select new
                    {
                        MovementNumber = me.Id,
                        TypeMovement = type.Name,
                        DetailMov = (
                            from det in _context.DetailMovements
                            join med in _context.Medicines on det.MedicineIdFk equals med.Id
                            where det.MedicineMovementIdFk == me.Id
                            select new
                            {
                                NumberDescription = det.Id,
                                MedicamentName = med.Name,
                                Quantity = det.Quantity,
                                PriceUnit = det.Price,
                                Check = det.Quantity * det.Price
                            }
                        ).ToList()
                    };

        if (!string.IsNullOrEmpty(search) && int.TryParse(search, out int num))
        {
            query = query.Where(p => p.MovementNumber == num);
        }

        query = query.OrderBy(p => p.MovementNumber);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}
