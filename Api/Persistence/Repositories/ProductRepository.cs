using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApiDbContext _dbContext;

        public ProductRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(prod => prod.Id == id);
        }
    }
}