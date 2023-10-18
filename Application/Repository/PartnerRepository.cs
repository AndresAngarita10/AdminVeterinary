
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class PartnerRepository : GenericRepository<Partner>, IPartnerRepository
{
    protected readonly ApiContext _context;
    
    public PartnerRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Partner>> GetAllAsync()
    {
        return await _context.Partners
        .ToListAsync();
    }

    public override async Task<Partner> GetByIdAsync(int id)
    {
        return await _context.Partners
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    
    public override async Task<(int totalRegistros, IEnumerable<Partner> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Partners as IQueryable<Partner>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            /* .Include(p => p.Pets) */
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}

