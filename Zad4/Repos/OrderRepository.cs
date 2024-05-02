using System.Data.SqlClient;
using Zad4.Models;

namespace Zad4.Repos;

public class OrderRepository : IOrderRepository
{
    private readonly IConfiguration _configuration;

    public OrderRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int?> CheckOrderPresence(int idProduct, int amount, DateTime createdAt)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();
        await using var command = new SqlCommand("COUNT (*) FROM Order WHERE IdProduct = @idProduct AND Amount = @amount AND CreatedAt < @date", connection);
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@date", createdAt);
        var result = (int)(await command.ExecuteScalarAsync())!;
        return result;
    }

    public async Task<Order> FindOrderByDataAsync(int idProduct, int amount, DateTime createdAt)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();
        await using var command = new SqlCommand("SELECT * FROM Order WHERE IdProduct = @idproduct AND Amount = @amount AND CreatedAt < @date AND FulfilledAt = NULL", connection);
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@date", createdAt);
        var reader = await command.ExecuteReaderAsync();
        while (reader.Read())
        {
            var order = new Order()
            {
                IdOrder = (int)reader["IdOrder"],
                IdProduct = (int)reader["IdProduct"],
                Amount = (int)reader["Amount"],
                CreatedAt = DateTime.Parse(reader["IdOrder"].ToString()!),
                FullfilledAt = DateTime.Parse(reader["FulfilledAt"].ToString()!),
            };
            return order;
        }
        return null!;
    }

    public async Task<bool> SetFulfilledAsync(int idOrder)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();
        await using var command = new SqlCommand("UPDATE Order SET FulfilledAt = CURRENT_TIMESTAMP WHERE IdOrder = @id", connection);
        command.Parameters.AddWithValue("@id", idOrder);
        var reader = await command.ExecuteReaderAsync();
        return reader.RecordsAffected == 1;
    }
}