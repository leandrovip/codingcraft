using System.Data.Entity.Migrations;
using CodingCraftEx07.Mvc.Context;

namespace CodingCraftEx07.Mvc.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CodingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CodingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}