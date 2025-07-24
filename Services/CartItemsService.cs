using ThreadAndDaringStore.Data;
using ThreadAndDaringStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ThreadAndDaringStore.Services
{
    public class CartItemsService
    {
        private readonly ThreadAndDaringStoreContext _context;

        public CartItemsService(ThreadAndDaringStoreContext context)
        {
            _context = context;
        }

        //GET all
        public async Task<List<CartItems>> GetAllAsync()
        {
            return await _context.CartItems.ToListAsync();
        }
        
        //GET by id
          public async Task<CartItems?> GetByIdAsync(int id)
        {
            return await _context.CartItems.FindAsync(id);
        }
        //CREATE
        public async Task<CartItems> AddAsync(CartItems entity)
        {
            _context.CartItems.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        //Update
        public async Task<CartItems?> UpdateAsync(int id, CartItems updated)
        {
            var existing = await _context.CartItems.FindAsync(id);
            if (existing == null) return null;

            await _context.SaveChangesAsync();
            return existing;
        }

        //DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CartItems.FindAsync(id);
            if (entity == null) return false;

            _context.CartItems.Remove(entity);
            await _context.SaveChangesAsync();
            return true; 
        }
            
    }
} 
