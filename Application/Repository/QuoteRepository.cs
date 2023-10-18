
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class QuoteRepository : GenericRepository<Quote>, IQuoteRepository
{
    protected readonly ApiContext _context;
    
    public QuoteRepository(ApiContext context) : base (context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Quote>> GetAllAsync()
    {
        return await _context.Quotes
        .ToListAsync();
    }

    public override async Task<Quote> GetByIdAsync(int id)
    {
        return await _context.Quotes
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
    
    public override async Task<(int totalRegistros, IEnumerable<Quote> registros)> GetAllAsync(int pageIndez, int pageSize, int search)
    {
        var query = _context.Quotes as IQueryable<Quote>;

        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.VeterinarianIdFk == search);
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

