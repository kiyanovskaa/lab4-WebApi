using Microsoft.EntityFrameworkCore;

namespace kpz_test4.Models
{
    public class Context:DbContext
    {
        public DbSet<Order> orders { get; set; }
        public DbSet<Jewelry> jewelry { get; set; }
        public DbSet<Customer> customers { get; set; }

        public Context(DbContextOptions<Context> options): base(options) 
        {
        }
    }
}
