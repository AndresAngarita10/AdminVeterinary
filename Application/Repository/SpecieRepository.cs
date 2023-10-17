
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
}
