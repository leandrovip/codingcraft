using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VipControle.Domain.Data.Context;
using VipControle.Domain.Models;

namespace VipControle.Domain.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser, string>(userStore);

                var usuario = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com",
                    EmailConfirmed = true
                };
                userManager.Create(usuario, "Admin@123456");

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var role = roleManager.FindByName("Admin");
                if (role == null)
                {
                    role = new IdentityRole("Admin");
                    roleManager.Create(role);
                }

                // Add user admin to Role Admin if not already added
                var rolesForUser = userManager.GetRoles(usuario.Id);
                if (!rolesForUser.Contains(role.Name))
                    userManager.AddToRole(usuario.Id, role.Name);
            }
        }
    }
}