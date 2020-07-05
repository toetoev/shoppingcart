using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoppingCart.Models;

namespace ShoppingCart.DB
{
    public class CartContext : DbContext
    {
        protected IConfiguration configuration;

        public CartContext(DbContextOptions<CartContext> options)
            : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            // unique name within a column
            model.Entity<Customer>().HasIndex(tbl => tbl.Name).IsUnique();

            // composite keys
            model.Entity<OrderDetails>().HasKey(tbl =>
                new { tbl.OrderId, tbl.ProductId }
            );

           /* model.Entity<ProductCode>().HasKey(tbl =>
                new { tbl.ProductID, tbl.OrderID }
            );*/
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<ProductCode> ProductCodes { get; set; }

        public DbSet<Review> Reviews { get; set; }
    }
}
