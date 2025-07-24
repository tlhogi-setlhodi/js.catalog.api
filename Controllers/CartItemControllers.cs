using Microsoft.AspNetCore.Mvc;
using ThreadAndDaringStore.Models;
using ThreadAndDaringStore.Services;

namespace ThreadAndDaringStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemsController : ControllerBase
    {
        private readonly CartItemsService _service;

        public CartItemsController(CartItemsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItems>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection = "asc")
        {
            var items = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
                items = items.Where(p => p.Product != null && p.Product.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            items = sortBy?.ToLower() switch
            {
                "productname" => sortDirection == "desc"
                    ? items.OrderByDescending(p => p.Product).ToList()
                    : items.OrderBy(p => p.Product).ToList(),
                "quantity" => sortDirection == "desc"
                    ? items.OrderByDescending(p => p.Quantity).ToList()
                    : items.OrderBy(p => p.Quantity).ToList(),
                _ => items
            };

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItems>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<CartItems>> Add(CartItems entity)
        {
            var added = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = added.Id }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CartItems entity)
        {
            if (id != entity.Id) return BadRequest();
            var updated = await _service.UpdateAsync(id, entity);
            if (updated == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}    