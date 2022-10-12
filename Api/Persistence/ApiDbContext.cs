using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Seller> Sellers { get; set; }
    }
}