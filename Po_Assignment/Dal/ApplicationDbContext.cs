using Microsoft.EntityFrameworkCore;
using Po_Assignment.Models;

namespace Po_Assignment.Dal
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }

        public DbSet<MaterialEntry> MaterialMasters { get; set; }

        public DbSet<VendorMaster> VendorMasters { get; set; }

        public DbSet<Po_Details> PoDetailsMaster { get; set; }

        public DbSet<Po_header> PoHeadersMaster { get; set; }

    }
}
