using System.Data.SqlClient;

namespace Zad4.Repos;

public class ProductRepository : IProductRepository
{
    private readonly IConfiguration _configuration;

    public ProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int?> CheckProductPresence(int idProduct)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();
        await using var command = new SqlCommand("COUNT (*) FROM Product WHERE IdProduct = @id", connection);
        command.Parameters.AddWithValue("@id", idProduct);
        var result = (int)(await command.ExecuteScalarAsync())!;
        return result;
    }
}