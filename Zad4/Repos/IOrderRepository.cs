using Zad4.Models;

namespace Zad4.Repos;

public interface IOrderRepository
{
    public Task<int?> CheckOrderPresence(int idProduct, int amount, DateTime createdAt);
    public Task<Order> FindOrderByDataAsync(int idProduct, int amount, DateTime createdAt);
}