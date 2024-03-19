using Microsoft.EntityFrameworkCore;
using Po_Assignment.Models;

namespace Po_Assignment.Dal
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        public DbSet<VendorMaster>VendorMasters { get; set; }
    }
}
