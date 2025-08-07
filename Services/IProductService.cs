using ThreadAndDaringStore.Models;

namespace ThreadAndDaringStore.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsAsync();
        Task<Product> AddProductAsync(Product product);
    }
}
