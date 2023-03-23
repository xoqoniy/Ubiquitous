using Domain.Entities;

namespace Data.IRepositories;

public interface ISoldProductRepository
{
    Task<SoldProduct> CreateAsync(SoldProduct product);
    Task<SoldProduct> UpdateAsync(int id, SoldProduct product);
    Task<bool> DeleteAsync(int id);
    Task<SoldProduct> GetAsync(int id);
    Task<List<SoldProduct>> GetAllAsync();
}
