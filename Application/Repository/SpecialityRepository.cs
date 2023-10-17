
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class SpecialityRepository : GenericRepository<Speciality>, ISpecialityRepository
{
    protected readonly ApiContext _context;
    
    public SpecialityRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Speciality>> GetAllAsync()
    {
        return await _context.Specialities
        .ToListAsync();
    }

    public override async Task<Speciality> GetByIdAsync(int id)
    {
        return await _context.Specialities
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}
