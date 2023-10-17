
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicineRepository: GenericRepository<Medicine>, IMedicineRepository
{
    protected readonly ApiContext _context;
    
    public MedicineRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Medicine>> GetAllAsync()
    {
        return await _context.Medicines
        .ToListAsync();
    }

    public override async Task<Medicine> GetByIdAsync(int id)
    {
        return await _context.Medicines
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}

