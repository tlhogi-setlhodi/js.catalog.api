using Microsoft.AspNetCore.Mvc;
using ThreadAndDaringStore.Models;
using ThreadAndDaringStore.Services;

namespace ThreadAndDaringStore.ControllersController
{ [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? category,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection = "asc")
        {
            var products = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
                products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category != null && p.Category.Name.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

            products = sortBy?.ToLower() switch
            {
                "name" => sortDirection == "desc"
                    ? products.OrderByDescending(p => p.Name).ToList()
                    : products.OrderBy(p => p.Name).ToList(),
                "price" => sortDirection == "desc"
                    ? products.OrderByDescending(p => p.Price).ToList()
                    : products.OrderBy(p => p.Price).ToList(),
                _ => products
            };

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Add(Product entity)
        {
            var added = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = added.Id }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product entity)
        {
            if (id != entity.Id) return BadRequest();
            var updated = await _service.UpdateAsync(id, entity);
            return updated == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return !deleted ? NotFound() : NoContent();
        }
    }

}