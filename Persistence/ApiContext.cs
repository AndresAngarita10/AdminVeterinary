
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }
}
