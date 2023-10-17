
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class DescriptionMedicalTreatmentRepository: GenericRepository<DescriptionMedicalTreatment>, IDescriptionMedicalTreatmentRepository
{
    protected readonly ApiContext _context;
    
    public DescriptionMedicalTreatmentRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<DescriptionMedicalTreatment>> GetAllAsync()
    {
        return await _context.DescriptionMedicalTreatments
        .ToListAsync();
    }

    public override async Task<DescriptionMedicalTreatment> GetByIdAsync(int id)
    {
        return await _context.DescriptionMedicalTreatments
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}

