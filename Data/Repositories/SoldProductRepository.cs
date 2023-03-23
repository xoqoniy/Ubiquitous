using Data.IRepositories;
using Domain.Entities;

namespace Data.Repositories;
public class SoldProductRepository : ISoldProductRepository
{
    private readonly IDapperRepository<SoldProduct> dapperRepository = new DapperRepository<SoldProduct>();
    public async Task<SoldProduct> CreateAsync(SoldProduct product)
    {
        string query = "INSERT INTO sold_products (ProductId, Amount, Price, CreatedAt) " +
            $"VALUES ({product.ProductId}, {product.Amount}, {product.Price}, now())";

        await dapperRepository.InsertAsync(query);

        return await dapperRepository.SelectAsync("SELECT * FROM sold_products WHERE Id = (SELECT MAX(Id) FROM sold_products)");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (await GetAsync(id) is null)
            return false;

        string query = $"DELETE FROM sold_products WHERE Id = {id}";
        await dapperRepository.DeleteAsync(query);
        return true;
    }

    public async Task<SoldProduct> GetAsync(int id)
    {
        string query = $"SELECT * FROM sold_products WHERE Id = {id}";
        return await dapperRepository.SelectAsync(query);
    }

    public async Task<List<SoldProduct>> GetAllAsync()
    {
        string query = $"SELECT * FROM sold_products";
        return (await dapperRepository.SelectAllAsync(query)).ToList();
    }

    public async Task<SoldProduct> UpdateAsync(int id, SoldProduct product)
    {
        string query = "UPDATE sold_products " +
            $"SET ProductId = {product.ProductId}, Amount = {product.Amount}, Price = {product.Price}, UpdatedAt = now() " +
            $"WHERE Id = {id}";
        await dapperRepository.UpdateAsync(query);
        return await GetAsync(id);
    }
}
