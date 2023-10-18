
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class LaboratoryRepository: GenericRepository<Laboratory>, ILaboratoryRepository
{
    protected readonly ApiContext _context;
    
    public LaboratoryRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Laboratory>> GetAllAsync()
    {
        return await _context.Laboratories
        .ToListAsync();
    }

    public override async Task<Laboratory> GetByIdAsync(int id)
    {
        return await _context.Laboratories
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }

    public override async Task<(int totalRegistros, IEnumerable<Laboratory> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Laboratories as IQueryable<Laboratory>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Medicines)
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}

