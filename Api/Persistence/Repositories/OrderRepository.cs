using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApiDbContext _dbContext;

        public OrderRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _dbContext.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _dbContext.Orders
                .Include(obj => obj.OrderedProducts).ThenInclude(ordProd => ordProd.Product)
                .Include(obj => obj.Seller)
                .SingleOrDefaultAsync(order => order.Id == id);
        }

        public async Task UpdateStatusAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}