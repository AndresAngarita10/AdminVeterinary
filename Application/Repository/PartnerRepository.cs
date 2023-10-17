
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
}

