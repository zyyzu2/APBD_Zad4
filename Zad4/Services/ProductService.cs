using Zad4.Repos;

namespace Zad4.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<int?> CheckProductPresence(int idProduct)
    {
        return _repository.CheckProductPresence(idProduct);
    }
}