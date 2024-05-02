using Zad4.DTOs;
using Zad4.Models;

namespace Zad4.Services;

public interface IWarehouseService
{
    public Task<int?> RegisterProductAsync(WarehouseDto dto);
    public Task<bool> CheckFulfilledStatus(int idOrder);
}