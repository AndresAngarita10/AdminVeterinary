
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class PartnerTypeRepository : GenericRepository<PartnerType>, IPartnerTypeRepository
{
    protected readonly ApiContext _context;
    
    public PartnerTypeRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<PartnerType>> GetAllAsync()
    {
        return await _context.PartnerTypes
        .ToListAsync();
    }

    public override async Task<PartnerType> GetByIdAsync(int id)
    {
        return await _context.PartnerTypes
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }

    
    public override async Task<(int totalRegistros, IEnumerable<PartnerType> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.PartnerTypes as IQueryable<PartnerType>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}

