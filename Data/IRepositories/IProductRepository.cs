using Domain.Entities;

namespace Data.IRepositories; 
public interface IProductRepository
{
    Task<Product> CreateAsync(Product product);
    Task<Product> UpdateAsync(int id,Product product);
    Task<bool> DeleteAsync(int id);
    Task<Product> GetAsync(int id);
    Task<List<Product>> GetAllAsync();
}
