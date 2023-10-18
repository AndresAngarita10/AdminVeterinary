
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class BreedRepository : GenericRepository<Breed>, IBreedRepository
{
    protected readonly ApiContext _context;

    public BreedRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Breed>> GetAllAsync()
    {
        return await _context.Breeds
        .ToListAsync();
    }

    public override async Task<Breed> GetByIdAsync(int id)
    {
        return await _context.Breeds
        .FirstOrDefaultAsync(p => p.Id == id);
    }


    public override async Task<(int totalRegistros, IEnumerable<Breed> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Breeds as IQueryable<Breed>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Pets)
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    
    public async Task<IEnumerable<Breed>> GetAllAsync2()
    {
        return await _context.Breeds
        .ToListAsync();
    }
    public async Task<(int totalRegistros, List<Breed> registros)> GetAllAsync2(int pageIndex, int pageSize, string search)
    {
        var query = _context.Breeds as IQueryable<Breed>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Pets)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

}
