
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
}

