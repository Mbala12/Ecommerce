using Microsoft.EntityFrameworkCore;
using Ecommerce.Model;

namespace Ecommerce.ProductService.Data
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id=1, Name="HP", Quantity=60, Price=260 });
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id=2, Name="DELL", Quantity=70, Price=180 });
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id=3, Name="LENOVO", Quantity=30, Price=290 });
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id=4, Name="SAMSUNG", Quantity=80, Price=250 });
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id=5, Name="ASUS", Quantity=50, Price=120 });


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ProductModel> Products { get; set; }
    }
    
}
