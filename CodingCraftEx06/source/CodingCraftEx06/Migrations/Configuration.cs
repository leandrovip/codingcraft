using System.Data.Entity.Migrations;
using CodingCraftEx06.Models.Context;

namespace CodingCraftEx06.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<FloodContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FloodContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}