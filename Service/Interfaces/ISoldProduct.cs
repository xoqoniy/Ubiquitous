using Domain.Entities;
using Service.DTOs;
using Service.Helpers;

namespace Service.Interfaces;
public interface ISoldProduct
{
    Task<Response<SoldProduct>> Create(SoldProduct product);
    Task<Response<SoldProduct>> Update(int id, SoldProductCreationDto product);
    Task<Response<bool>> Delete(int id);
    Task<Response<SoldProduct>> Get(int id);
    Task<Response<List<SoldProduct>>> GetAll();
    Task<Response<decimal>> CalculateTotalSells();
}
