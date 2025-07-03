using ChatGPTIntegration.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTIntegration.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "Alice", LastName = "Smith", Email = "alice@example.com" },
                new Customer { CustomerId = 2, FirstName = "Bob", LastName = "Johnson", Email = "bob@example.com" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Laptop", Price = 999.99m },
                new Product { ProductId = 2, Name = "Smartphone", Price = 599.99m }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, CustomerId = 1, ProductId = 1, Quantity = 1, OrderDate = new DateTime(2024, 7, 2) },
                new Order { OrderId = 2, CustomerId = 2, ProductId = 2, Quantity = 2, OrderDate = new DateTime(2024, 7, 2) }
            );

            modelBuilder.Entity<Setting>().HasData(
                new Setting { SettingId = 1, Key = "OpenAI_API_Key", Value = "YOUR_KEY_HERE" }
            );
        }
    }
}
