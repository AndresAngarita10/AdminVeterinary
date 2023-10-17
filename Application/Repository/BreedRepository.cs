
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class BreedRepository : GenericRepository<Breed>, IBreedRepository
{
    protected readonly ApiContext _context;
    
    public BreedRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Breed>> GetAllAsync()
    {
        return await _context.Breeds
        .ToListAsync();
    }

    public override async Task<Breed> GetByIdAsync(int id)
    {
        return await _context.Breeds
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}
