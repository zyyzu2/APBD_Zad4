using Zad4.Models;

namespace Zad4.Services;

public interface IOrderService
{
    public Task<int?> CheckOrderPresence(int idProduct, int amount, DateTime createdAt);
    public Task<Order?> FindOrderByDataAsync(int idProduct, int amount, DateTime createdAt);
}