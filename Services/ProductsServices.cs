using ThreadAndDaringStore.Data;
using ThreadAndDaringStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ThreadAndDaringStore.Services
{
    public class ProductService
    {
        private readonly ThreadAndDaringStoreContext _context;

        public ProductService(ThreadAndDaringStoreContext context)
        {
            _context = context;
        }

        // GET all
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // GET by ID
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        // CREATE
        public async Task<Product> AddAsync(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // UPDATE
        public async Task<Product?> UpdateAsync(int id, Product updated)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return null;

            // map fields here
            // existing.Field = updated.Field;

            await _context.SaveChangesAsync();
            return existing;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity == null) return false;

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
