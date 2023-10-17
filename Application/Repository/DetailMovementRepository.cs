
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class DetailMovementRepository: GenericRepository<DetailMovement>, IDetailMovementRepository
{
    protected readonly ApiContext _context;
    
    public DetailMovementRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<DetailMovement>> GetAllAsync()
    {
        return await _context.DetailMovements
        .ToListAsync();
    }

    public override async Task<DetailMovement> GetByIdAsync(int id)
    {
        return await _context.DetailMovements
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}
