using Data.IRepositories;
using Domain.Entities;

namespace Data.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly DapperRepository<Product> dapperRepository = new DapperRepository<Product>();
    public async Task<Product> CreateAsync(Product product)
    {
        var createdTime = DateTime.Now.ToString("yyyy-MM-dd-hh-mm");
        string query = $"INSERT INTO product(name,count,price,CreatedAt)" +
            $" VALUES('{product.Name.Replace("'","''")}',{product.Count},{product.Count},now())";
       await dapperRepository.InsertAsync(query);
        return await dapperRepository.SelectAsync("SELECT * FROM products WHERE Id = (SELECT MAX(Id) FROM products)");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if(GetAsync(id) is null)
        {
            return false;
        }
        string query = $"delete from product where Id={id}";
        await dapperRepository.DeleteAsync(query);
        return true;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        string query = "select * from product";
        return (await dapperRepository.SelectAllAsync(query)).ToList();
    }

    public async Task<Product> GetAsync(int id)
    {
        string query = $"select * from product where id = {id}";
        return await dapperRepository.SelectAsync(query);
    }

    public async Task<Product> UpdateAsync(int id,Product product)
    {
        string query = "UPDATE product " +
            $"SET Name = '{product.Name.Replace("'", "''")}', Count = {product.Count}, Price = {product.Price}, UpdatedAt = now() " +
            $"WHERE Id = {id};";
        await dapperRepository.UpdateAsync(query);
        return await GetAsync(id);
    }
}
