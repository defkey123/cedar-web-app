using System;
using Microsoft.EntityFrameworkCore;

namespace CedarWebApp.Models
{
    public class CedarContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public CedarContext(DbContextOptions<CedarContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FoodItem> Foods { get; set; }
        public DbSet<CategoryJoined> CategoriesJoined { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<FoodJoined> FoodItemsJoined { get; set; }
    }
}