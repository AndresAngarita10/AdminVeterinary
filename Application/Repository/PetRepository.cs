
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class PetRepository : GenericRepository<Pet>, IPetRepository
{
    protected readonly ApiContext _context;
    
    public PetRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Pet>> GetAllAsync()
    {
        return await _context.Pets
        .ToListAsync();
    }

    public override async Task<Pet> GetByIdAsync(int id)
    {
        return await _context.Pets
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    public override async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Pets as IQueryable<Pet>;

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
