using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Infrastructure
{
        public class DataContext : IdentityDbContext<AppUser>
        {
                public DataContext(DbContextOptions<DataContext> options) : base(options)
                { }
                public DbSet<Product> Products { get; set; }
                public DbSet<Category> Categories { get; set; }
                public DbSet<User> Users { get; set; }
                public DbSet<CartViewModel> OrderCarts { get; set; }
                public DbSet<CartItem> CartItems { get; set; }

    }
}
