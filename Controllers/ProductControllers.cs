using Microsoft.AspNetCore.Mvc;
using ThreadAndDaringStore.Models;
using ThreadAndDaringStore.Services;
using System.Text.Json;

namespace ThreadAndDaringStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IProductService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _service.GetProductsAsync();

            var formattedProducts = products.Select(p => new
            {
                id = p.Id,
                name = p.Name,
                description = p.Description,
                price = p.Price,
                currency = "ZAR",
                vatIncluded = true,
                vatRate = 15,
                available = p.Stock > 0,
                category = p.Category,
                tags = p.Tags ?? new List<string>(),
                imageUrl = p.ImageUrl,
                stock = p.Stock
            });

            var response = new
            {
                products = formattedProducts,
                total = formattedProducts.Count(),
                page = 1,
                pageSize = 10
            };

            return Ok(response);
        }

        // POST: api/product
        [HttpPost]
        [RequestSizeLimit(10_000_000)] // limit upload size (10MB)
        public async Task<IActionResult> Post([FromForm] ProductUploadDto dto)
        {
            if (dto.Image == null || dto.Image.Length == 0)
                return BadRequest("Image file is required.");

            // Save the image
            var imageFileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.Image.FileName)}";
            var imagePath = Path.Combine(_environment.WebRootPath, "images", imageFileName);

            // Ensure the folder exists
            Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            var product = new Product
            {
                Id = dto.ProductCode,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Category = dto.Category,
                Stock = dto.Stock,
                ImageUrl = $"/images/{imageFileName}",
                Tags = string.Join(",", dto.Tags ?? new List<string>())
            };

            var created = await _service.AddProductAsync(product);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
    }
}
