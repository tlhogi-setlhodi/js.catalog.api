using ThreadAndDaringStore.Data;
using ThreadAndDaringStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;

namespace ThreadAndDaringStore.Services
{
    public class CategoryService
    {
        private readonly ThreadAndDaringStoreContext _context;

        public CategoryService(ThreadAndDaringStoreContext context)
        {
            _context = context;
        }

        //GET all
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        //GET by ID
        public async Task<Category?> GetByIdAsync(int id)
        {
        
            return await _context.Categories.FindAsync(id);

        }
    
        //CREATE
        public async Task<Category> AddAsync(Category entity)
        {
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        //UPDATE
        public async Task<Category?> UpdateAsync(int id, Category updated)
        {
            var existing = await _context.Categories.FindAsync(id);
            if (existing == null) return null;


            await _context.SaveChangesAsync();
            return existing;
        }

        //DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null) return false;

            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}