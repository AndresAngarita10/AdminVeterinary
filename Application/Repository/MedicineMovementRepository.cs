
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicineMovementRepository: GenericRepository<MedicineMovement>, IMedicineMovementRepository
{
    protected readonly ApiContext _context;
    
    public MedicineMovementRepository(ApiContext context) : base (context)
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
        .FirstOrDefaultAsync(p =>  p.Id == id);
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
}
