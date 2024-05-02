namespace Zad4.Repos;

public interface IProductRepository
{
    public Task<int?> CheckProductPresence(int idProduct);
}