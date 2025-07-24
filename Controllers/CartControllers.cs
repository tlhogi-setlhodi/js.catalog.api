using Microsoft.AspNetCore.Mvc;
using ThreadAndDaringStore.Models;
using ThreadAndDaringStore.Services;

namespace ThreadAndDaringStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetById(int id)
        {
            var cart = await _service.GetByIdAsync(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> Add(Cart entity)
        {
            var added = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = added.Id }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cart entity)
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
