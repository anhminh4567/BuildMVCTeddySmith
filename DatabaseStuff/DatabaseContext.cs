using BuildMVCTeddySmith.DatabaseStuff.Model;
using Microsoft.EntityFrameworkCore;

namespace BuildMVCTeddySmith.DatabaseStuff
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresss { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }

}
