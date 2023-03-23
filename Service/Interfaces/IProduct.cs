using Domain.Entities;
using Service.DTOs;
using Service.Helpers;

namespace Service.Interfaces;
public interface IProduct
{
    Task<Response<Product>> Create(ProductCreationDto product);
    Task<Response<Product>> Update(int id, ProductCreationDto product);
    Task<Response<bool>> Delete(int id);
    Task<Response<Product>> Get(int id);
    Task<Response<List<Product>>> GetAll();
    Task<Response<bool>> Sell(int id, int amount);
}
