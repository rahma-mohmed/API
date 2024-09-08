using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class ITIContext : IdentityDbContext<User>
    {
        public DbSet<Department> Department { get; set; }

        public ITIContext(DbContextOptions<ITIContext> options):base(options)
        {
            
        }
    }
}
