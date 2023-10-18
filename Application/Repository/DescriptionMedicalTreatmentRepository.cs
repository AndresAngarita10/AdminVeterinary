
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

    
    public override async Task<(int totalRegistros, IEnumerable<DescriptionMedicalTreatment> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.DescriptionMedicalTreatments as IQueryable<DescriptionMedicalTreatment>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Observation.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            /* .Include(p => p) */
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

}

