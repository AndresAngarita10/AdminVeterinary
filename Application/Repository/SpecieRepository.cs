
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class SpecieRepository : GenericRepository<Specie>, ISpecieRepository
{
    protected readonly ApiContext _context;
    
    public SpecieRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Specie>> GetAllAsync()
    {
        return await _context.Species
        .ToListAsync();
    }

    public override async Task<Specie> GetByIdAsync(int id)
    {
        return await _context.Species
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    
    public override async Task<(int totalRegistros, IEnumerable<Specie> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Species as IQueryable<Specie>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
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
