using Ecommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ecommerce.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cart>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<CartDetail>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<Category>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<Collection>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<Contact>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<Coupon>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<CouponApply>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<Manufacturer>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<Order>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<OrderDetail>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<OrderHistory>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<Product>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<ProductComment>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<ProductCommentReply>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<ProductCommentImage>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<ProductImage>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<ProductAttribute>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<ProductSize>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<ProductColor>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<Supplier>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<User>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Entity<UserProfile>().Property(e => e.Sort).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponApply> CouponApplies { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ProductCommentReply> ProductCommentReplys { get; set; }
        public DbSet<ProductCommentImage> ProductCommentImages { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}