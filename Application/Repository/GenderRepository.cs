
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
}

