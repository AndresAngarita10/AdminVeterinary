
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicinePartnerRepository: GenericRepository<MedicinePartner>, IMedicinePartnerRepository
{
    protected readonly ApiContext _context;
    
    public MedicinePartnerRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<MedicinePartner>> GetAllAsync()
    {
        return await _context.MedicinePartners
        .ToListAsync();
    }

    public override async Task<MedicinePartner> GetByIdAsync(int id)
    {
        return await _context.MedicinePartners
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    
    public override async Task<(int totalRegistros, IEnumerable<MedicinePartner> registros)> GetAllAsync(int pageIndez, int pageSize, int search)
    {
        var query = _context.MedicinePartners as IQueryable<MedicinePartner>;

        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.Id == search);
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

