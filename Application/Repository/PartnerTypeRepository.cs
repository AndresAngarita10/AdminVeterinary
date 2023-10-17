
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
}

