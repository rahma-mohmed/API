using Microsoft.EntityFrameworkCore;
using WebAPI2.Model;

namespace WebAPI2.Data
{
    public class ITIContext2 : DbContext
    {
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }
        public ITIContext2(DbContextOptions<ITIContext2> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
