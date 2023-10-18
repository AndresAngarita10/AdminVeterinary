
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
    
    public override async Task<(int totalRegistros, IEnumerable<MedicalTreatment> registros)> GetAllAsync(int pageIndez, int pageSize, int search)
    {
        var query = _context.MedicalTreatments as IQueryable<MedicalTreatment>;

        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.Id == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.DescriptionMedicalTreatments)
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}
