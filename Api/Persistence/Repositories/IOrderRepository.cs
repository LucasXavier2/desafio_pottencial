using Api.Entities;

namespace Api.Persistence.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateStatusAsync(Order order);
    }
}