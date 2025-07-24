using ThreadAndDaringStore.Data;
using ThreadAndDaringStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ThreadAndDaringStore.Services
{
    public class CartService
    {
        private readonly ThreadAndDaringStoreContext _context;
        public CartService(ThreadAndDaringStoreContext context)
        {
            _context = context;
        }

        //GET all
        public async Task<List<Cart>> GetAllAsync()
        {
            {
                return await _context.Carts.ToListAsync();
            }
        }
        /*        public async Task<List<CartItems>> GetAllAsync()
        {
            return await _context.CartItems.ToListAsync();
        }*/

        //GET by ID
        public async Task<Cart?> GetByIdAsync(int Id)
        {
            return await _context.Carts.FindAsync(Id);

        }
        //CREATE
        public async Task<Cart> AddAsync(Cart entity)
        {
            _context.Carts.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        //Update
        public async Task<Cart?> UpdateAsync(int id, Cart updated)
        {
            var existing = await _context.Carts.FindAsync(id);
            if (existing == null) return null;

            await _context.SaveChangesAsync();
            return existing;
        }

        //DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Carts.FindAsync(id);
            if (entity == null) return false;

            _context.Carts.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}




