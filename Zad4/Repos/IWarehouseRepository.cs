using System.Runtime.InteropServices.JavaScript;
using Zad4.Models;

namespace Zad4.Repos;

public interface IWarehouseRepository
{
    public Task<int?> RegisterProductInWarehouseAsync(int idWarehouse, int idProduct, int idOrder, int amount);
    public Task<int?> CheckWarehousePresence(int idWarehouse);
    public Task<bool> CheckFulfilledStatusAsync(int idOder);
}