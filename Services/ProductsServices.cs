using Microsoft.EntityFrameworkCore;
using ThreadAndDaringStore.Data;
using ThreadAndDaringStore.Models;

namespace ThreadAndDaringStore.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Currency = p.Currency,
                VatIncluded = p.VatIncluded,
                VatRate = p.VatRate,
                Available = p.Available,
                Category = p.Category,
                Tags = p.Tags.Split(',').Select(t => t.Trim()).ToList(),
                ImageUrl = p.ImageUrl,
                Stock = p.Stock
            }).ToList();
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}

