
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicineMovementRepository: GenericRepository<MedicineMovement>, IMedicineMovementRepository
{
    protected readonly ApiContext _context;
    
    public MedicineMovementRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<MedicineMovement>> GetAllAsync()
    {
        return await _context.MedicineMovements
        .ToListAsync();
    }

    public override async Task<MedicineMovement> GetByIdAsync(int id)
    {
        return await _context.MedicineMovements
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}
