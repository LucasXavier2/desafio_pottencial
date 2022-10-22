using Api.Entities;

namespace Api.Persistence.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task AddProductAsync(Product product);
    }
}