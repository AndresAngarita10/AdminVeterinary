
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
}

