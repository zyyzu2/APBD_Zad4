using System.Data.SqlClient;
using Zad4.Models;

namespace Zad4.Repos;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<int?> RegisterProductInWarehouseAsync(int idWarehouse, int idProduct, int idOrder, int amount)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        try
        {
            await using var command =
                new SqlCommand("UPDATE Order SET FullfiledAt = CURRENT_TIMESTAMP WHERE IdOrder = @idOrder");
            command.Transaction = (SqlTransaction)transaction;
            command.Parameters.AddWithValue("@idOrder", idOrder);
            await command.ExecuteNonQueryAsync();

            command.CommandText = "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, CreatedAt, Amount, Price) VALUES(" +
                                  "@idWarehouse, @idProduct, @idOrder, CURRENT_TIMESTAMP, @Amount, @Amount*(SELECT Price FROM Product WHERE IdProduct = @idProduct));";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@idWarehouse", idWarehouse);
            command.Parameters.AddWithValue("@idProduct", idProduct);
            command.Parameters.AddWithValue("@idOrder", idOrder);
            command.Parameters.AddWithValue("@Amount", amount);
            var idProductWarehouse = (int)(await command.ExecuteScalarAsync())!;
            return idProductWarehouse;
        }
        catch
        {
            await transaction.RollbackAsync();
            return null;
        }
    }

    public async Task<int?> CheckWarehousePresence(int idWarehouse)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();
        await using var command = new SqlCommand("COUNT (*) FROM Warehouse WHERE IdWarehouse = @id", connection);
        command.Parameters.AddWithValue("@id", idWarehouse);
        var result = (int)(await command.ExecuteScalarAsync())!;
        return result;
    }

    public async Task<bool> CheckFulfilledStatusAsync(int idOrder)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();
        await using var command = new SqlCommand("COUNT (*) FROM Product_Warehouse WHERE IdOrder = @id", connection);
        command.Parameters.AddWithValue("@id", idOrder);
        var result = (int)(await command.ExecuteScalarAsync())!;
        return result == 0;
    }
}