using ThreadAndDaringStore.Data;
using ThreadAndDaringStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ThreadAndDaringStore.Services
{
    public class OrderItemService
    {
        private readonly ThreadAndDaringStoreContext _context;

        public OrderItemService(ThreadAndDaringStoreContext context)
        {
            _context = context;
        }

        //GET all
        public async Task<List<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        //GET by Id
        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        //CREATE
        public async Task<OrderItem> AddAsync(OrderItem entity)
        {
            _context.OrderItems.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        //UPDATE
        public async Task<OrderItem?> UpdateAsync(int id, OrderItem updated)
        {
            var existing = await _context.OrderItems.FindAsync(id);
            if (existing == null) return null;

            await _context.SaveChangesAsync();
            return existing;
        }

        //DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.OrderItems.FindAsync(id);
            if (entity == null) return false;

            _context.OrderItems.Remove(entity);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}