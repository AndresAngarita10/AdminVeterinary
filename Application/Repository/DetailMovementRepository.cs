
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class DetailMovementRepository: GenericRepository<DetailMovement>, IDetailMovementRepository
{
    protected readonly ApiContext _context;
    
    public DetailMovementRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<DetailMovement>> GetAllAsync()
    {
        return await _context.DetailMovements
        .ToListAsync();
    }

    public override async Task<DetailMovement> GetByIdAsync(int id)
    {
        return await _context.DetailMovements
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    
    public override async Task<(int totalRegistros, IEnumerable<DetailMovement> registros)> GetAllAsync(int pageIndez, int pageSize, int search)
    {
        var query = _context.DetailMovements as IQueryable<DetailMovement>;
        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.MedicineIdFk == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.MedicineMovementIdFk)
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}
