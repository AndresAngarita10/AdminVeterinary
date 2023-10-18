
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class SpecialityRepository : GenericRepository<Speciality>, ISpecialityRepository
{
    protected readonly ApiContext _context;
    
    public SpecialityRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Speciality>> GetAllAsync()
    {
        return await _context.Specialities
        .ToListAsync();
    }

    public override async Task<Speciality> GetByIdAsync(int id)
    {
        return await _context.Specialities
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    
    public override async Task<(int totalRegistros, IEnumerable<Speciality> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Specialities as IQueryable<Speciality>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
           /*  .Include(p => p) */
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}
