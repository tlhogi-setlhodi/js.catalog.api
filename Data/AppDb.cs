using ThreadAndDaringStore.Models;
using ThreadAndDaringStore.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace ThreadAndDaringStore.Data
{
    public class ThreadAndDaringStoreContext : DbContext
    {
        public ThreadAndDaringStoreContext(DbContextOptions<ThreadAndDaringStoreContext> options) : base(options) { }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

    }
    
}