using Data.IRepositories;
using Data.Repositories;
using Domain.Entities;
using Service.DTOs;
using Service.Helpers;
using Service.Interfaces;

namespace Service.Services;
public class ProductService : IProduct
{
    private readonly IProductRepository productRepository = new ProductRepository();
    private readonly SoldProductService soldProductService = new SoldProductService();
    public async Task<Response<Product>> Create(ProductCreationDto product)
    {
        var mappedProduct = new Product
        {
            Count = product.Count,
            Price = product.Price,
            Name = product.Name
        };

        Product createdProduct = await productRepository.CreateAsync(mappedProduct);

        return new Response<Product>
        {
            StatusCode = 200,
            Message = "Success",
            Value = createdProduct
        };
    }

    public async Task<Response<bool>> Delete(int id)
    {
        var isDeleted = await productRepository.DeleteAsync(id);

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

    public async Task<Response<Product>> Get(int id)
    {
        var product = await productRepository.GetAsync(id);

        if (product is null)
            return new Response<Product>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        return new Response<Product>
        {
            StatusCode = 200,
            Message = "Success",
            Value = product
        };
    }

    public async Task<Response<List<Product>>> GetAll()
    {
        var products = await productRepository.GetAllAsync();

        if (products is null)
            products = new List<Product>();

        return new Response<List<Product>>
        {
            StatusCode = 200,
            Message = "Success",
            Value = products
        };
    }

    public async Task<Response<bool>> Sell(int id, int amount)
    {
        var product = await productRepository.GetAsync(id);

        if (product is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        if (product.Count < amount)
            return new Response<bool>
            {
                StatusCode = 401,
                Message = "Product is not enough"
            };

        product.Count -= amount;

        var soldProductDto = new SoldProductCreationDto
        {
            Amount = amount,
            Price = product.Price,
            ProductId = id
        };

        await soldProductService.CreateAsync(soldProductDto);

        await productRepository.UpdateAsync(id, product);

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Value = true
        };
    }

    public async Task<Response<Product>> Update(int id, ProductCreationDto productDto)
    {
        var product = await productRepository.GetAsync(id);

        if (product is null)
            new Response<Product>
            {
                StatusCode = 404,
                Message = "Not found"
            };

        Product mappedProduct = new Product
        {
            Count = productDto.Count,
            Name = productDto.Name,
            Price = productDto.Price
        };

        var updatedProduct = await productRepository.UpdateAsync(id, mappedProduct);

        return new Response<Product>
        {
            StatusCode = 200,
            Message = "Success",
            Value = updatedProduct
        };
    }
}
