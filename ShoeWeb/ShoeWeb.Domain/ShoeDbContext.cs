using Microsoft.EntityFrameworkCore;
using ShoeWeb.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoeWeb.Domain
{
    public class ShoeDbContext : DbContext
    {
        public ShoeDbContext(DbContextOptions<ShoeDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Model.Attribute> Attributes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
