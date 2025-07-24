using ThreadAndDaringStore.Data;
using ThreadAndDaringStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ThreadAndDaringStore.Services
{
    public class OrderService
    {
        private readonly ThreadAndDaringStoreContext _context;

        public OrderService(ThreadAndDaringStoreContext context)
        {
            _context = context;
        }

        // GET all
        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET by ID
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        // CREATE
        public async Task<Order> AddAsync(Order entity)
        {
            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // UPDATE
        public async Task<Order?> UpdateAsync(int id, Order updated)
        {
            var existing = await _context.Orders.FindAsync(id);
            if (existing == null) return null;

            // map fields here
            // existing.Field = updated.Field;

            await _context.SaveChangesAsync();
            return existing;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Orders.FindAsync(id);
            if (entity == null) return false;

            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
