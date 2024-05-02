namespace Zad4.Services;

public interface IProductService
{
    public Task<int?> CheckProductPresence(int idProduct);
    
}