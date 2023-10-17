
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicalTreatmentRepository: GenericRepository<MedicalTreatment>, IMedicalTreatmentRepository
{
    protected readonly ApiContext _context;
    
    public MedicalTreatmentRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<MedicalTreatment>> GetAllAsync()
    {
        return await _context.MedicalTreatments
        .ToListAsync();
    }

    public override async Task<MedicalTreatment> GetByIdAsync(int id)
    {
        return await _context.MedicalTreatments
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}
