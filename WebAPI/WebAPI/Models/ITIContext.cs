using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class ITIContext : DbContext
    {
        public DbSet<Department> Department { get; set; }

        public ITIContext(DbContextOptions<ITIContext> options):base(options)
        {
            
        }
    }
}
