
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
}

