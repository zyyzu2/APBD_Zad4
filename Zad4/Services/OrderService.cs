using System.Data.SqlClient;
using System.Runtime.InteropServices.JavaScript;
using Zad4.Models;
using Zad4.Repos;

namespace Zad4.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<int?> CheckOrderPresence(int idProduct, int amount, DateTime createdAt)
    {
        return await _repository.CheckOrderPresence(idProduct, amount, createdAt);
    }

    public async Task<Order?> FindOrderByDataAsync(int idProduct, int amount, DateTime createdAt)
    {
        return await _repository.FindOrderByDataAsync(idProduct, amount, createdAt);
    }
    
}