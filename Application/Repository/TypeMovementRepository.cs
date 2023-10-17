
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class TypeMovementRepository : GenericRepository<TypeMovement>, ITypeMovementRepository
{
    protected readonly ApiContext _context;
    
    public TypeMovementRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<TypeMovement>> GetAllAsync()
    {
        return await _context.TypeMovements
        .ToListAsync();
    }

    public override async Task<TypeMovement> GetByIdAsync(int id)
    {
        return await _context.TypeMovements
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}
