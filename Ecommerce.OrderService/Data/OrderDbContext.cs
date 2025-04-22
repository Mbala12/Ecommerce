using Microsoft.EntityFrameworkCore;
using Ecommerce.Model;

namespace Ecommerce.OrderService.Data
{
    public class OrderDbContext: DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<OrderModel> Orders { get; set; }
    }
}
