
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
}
