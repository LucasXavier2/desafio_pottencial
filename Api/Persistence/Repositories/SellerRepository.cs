using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly ApiDbContext _dbContext;

        public SellerRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddSellerAsync(Seller seller)
        {
            await _dbContext.Sellers.AddAsync(seller);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Seller>> GetAllAsync()
        {
            return await _dbContext.Sellers.ToListAsync();
        }

        public async Task<Seller> GetByIdAsync(int id)
        {
            return await _dbContext.Sellers.SingleOrDefaultAsync(v => v.Id == id);
        }
    }
}