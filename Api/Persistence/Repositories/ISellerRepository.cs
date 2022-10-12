using Api.Entities;

namespace Api.Persistence.Repositories
{
    public interface ISellerRepository
    {
        Task<Seller> GetByIdAsync(int id);
        Task<List<Seller>> GetAllAsync();
        Task AddSellerAsync(Seller seller);
    }
}