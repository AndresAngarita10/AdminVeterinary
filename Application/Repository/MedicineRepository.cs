
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
{
    protected readonly ApiContext _context;

    public MedicineRepository(ApiContext context) : base(context)
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
        .FirstOrDefaultAsync(p => p.Id == id);
    }


    public override async Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Medicines as IQueryable<Medicine>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
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

    public async Task<IEnumerable<Medicine>> MedicamentoLaboratorioGenfar()
    {
        return await (
            from m in _context.Medicines
            join l in _context.Laboratories on m.LaboratoryIdFk equals l.Id
            where l.Name.Contains("Genfar")
            select m
        ).ToListAsync();
    }

    public async Task<(int totalRegistros, List<Medicine> registros)> MedicamentoLaboratorioGenfar(int pageIndex, int pageSize, string search)
    {
        var query = from m in _context.Medicines
                    join l in _context.Laboratories on m.LaboratoryIdFk equals l.Id
                    where l.Name.Contains("Genfar")
                    select m;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Name);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public async Task<IEnumerable<Object>> MedicamentoMayor10Usd()
    {
        return await (
            from med in _context.Medicines
            join lab in _context.Laboratories on med.LaboratoryIdFk equals lab.Id
            where med.Price >= 10
            select new
            {
                Name = med.Name,
                Price = med.Price,
                QuantityAvalible = med.QuantityAvalible,
                Laboratory = lab.Name
            }
        ).ToListAsync();
    }

    public async Task<(int totalRegistros, IEnumerable<object> registros)> MedicamentoMayor10Usd(int pageIndex, int pageSize, string search)
    {
        var query = from med in _context.Medicines
                    join lab in _context.Laboratories on med.LaboratoryIdFk equals lab.Id
                    where med.Price >= 10
                    select new
                    {
                        Name = med.Name,
                        Price = med.Price,
                        QuantityAvalible = med.QuantityAvalible,
                        Laboratory = lab.Name
                    };

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Name);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}

