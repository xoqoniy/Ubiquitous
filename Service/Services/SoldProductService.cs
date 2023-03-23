using Data.IRepositories;
using Data.Repositories;
using Domain.Entities;
using Service.DTOs;
using Service.Helpers;

namespace Service.Services; 
public class SoldProductService 
{
    private readonly ISoldProductRepository soldProductRepository = new SoldProductRepository();

    public async Task<Response<decimal>> CalculateTotalSells()
    {
        var products = await soldProductRepository.GetAllAsync();
        decimal result = 0;

        foreach (var product in products)
        {
            result += product.Price * product.Amount;
        }

        return new Response<decimal>
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    public async Task<Response<SoldProduct>> CreateAsync(SoldProductCreationDto product)
    {
        var mappedProduct = new SoldProduct
        {
            Amount = product.Amount,
            Price = product.Price,
            ProductId = product.ProductId
        };

        SoldProduct createdProduct = await soldProductRepository.CreateAsync(mappedProduct);            

        return new Response<SoldProduct>
        {
            StatusCode = 200,
            Message = "Success",
            Value = createdProduct
        };
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var isDeleted = await soldProductRepository.DeleteAsync(id);

        if (isDeleted)
            return new Response<bool>
            {
                StatusCode = 200,
                Message = "Success",
                Value = true
            };

        return new Response<bool>
        {
            StatusCode = 404,
            Message = "Not found",
            Value = false
        };
    }

    public async Task<Response<SoldProduct>> GetAsync(int id)
    {
        var product = await soldProductRepository.GetAsync(id);

        if (product is null)
            return new Response<SoldProduct>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        return new Response<SoldProduct>
        {
            StatusCode = 200,
            Message = "Success",
            Value = product
        };
    }

    public async Task<Response<List<SoldProduct>>> GetAllAsync()
    {
        var soldProducts = await soldProductRepository.GetAllAsync();

        if (soldProducts is null)
            soldProducts = new List<SoldProduct>();

        return new Response<List<SoldProduct>>
        {
            StatusCode = 200,
            Message = "Success",
            Value = soldProducts
        };
    }

    public async Task<Response<SoldProduct>> UpdateAsync(int id, SoldProductCreationDto productDto)
    {
        var product = await soldProductRepository.GetAsync(id);

        if (product is null)
            new Response<SoldProduct>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        var mappedProduct = new SoldProduct
        {
            Price = productDto.Price,
            Amount = productDto.Amount,
            ProductId = productDto.ProductId
        };

        var updatedProduct = await soldProductRepository.UpdateAsync(id, mappedProduct);

        return new Response<SoldProduct>
        {
            StatusCode = 200,
            Message = "Success",
            Value = updatedProduct
        };
    }
}
