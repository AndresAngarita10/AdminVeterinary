
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class GenderRepository: GenericRepository<Gender>, IGenderRepository
{
    protected readonly ApiContext _context;
    
    public GenderRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Gender>> GetAllAsync()
    {
        return await _context.Genders
        .ToListAsync();
    }

    public override async Task<Gender> GetByIdAsync(int id)
    {
        return await _context.Genders
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    
    public override async Task<(int totalRegistros, IEnumerable<Gender> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Genders as IQueryable<Gender>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Partners)
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}

