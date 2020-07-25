using System.Data.Entity;

namespace CodingCraftEx06.Models.Context
{
    public class FloodContext : DbContext
    {
        public DbSet<FloodKilling> FloodKillings { get; set; }
        public DbSet<Country> Countries { get; set; }

        public FloodContext() :
            base("DefaultConnection") { }
    }
}