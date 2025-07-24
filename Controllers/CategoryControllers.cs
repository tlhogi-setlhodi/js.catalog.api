using Microsoft.AspNetCore.Mvc;
using ThreadAndDaringStore.Models;
using ThreadAndDaringStore.Services;

namespace ThreadAndDaringStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItems>>> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItems>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Add(Category entity)
        {
            var added = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = added.Id }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category entity)
        {
            if (id != entity.Id)
                return BadRequest("ID mismatch between URL and entity.");

            var updated = await _service.UpdateAsync(id, entity);

            if (updated == null)
                return NotFound();
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