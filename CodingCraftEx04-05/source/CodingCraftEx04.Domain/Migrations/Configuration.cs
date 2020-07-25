using System;
using System.Data.Entity.Migrations;
using System.Linq;
using CodingCraftEx04.Domain.Models;
using CodingCraftEx04.Domain.Models.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraftEx04.Domain.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CodingCraftEx04.Domain.Models.Context.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // Seed
            if (!(context.Users.Any(u => u.UserName == "admin@admin.com")))
            {
                var userStore =
                    new UserStore<Usuario, Grupo, Guid, UsuarioLogin, UsuarioGrupo, UsuarioIdentificacao>(context);
                var userManager = new UserManager<Usuario, Guid>(userStore);

                var usuario = new Usuario
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com",
                    EmailConfirmed = true
                };
                userManager.Create(usuario, "Admin@123456");

                var roleStore = new RoleStore<Grupo, Guid, UsuarioGrupo>(context);
                var roleManager = new RoleManager<Grupo, Guid>(roleStore);

                var role = roleManager.FindByName("Admin");
                if (role == null)
                {
                    role = new Grupo {Name = "Admin", Id = Guid.NewGuid()};
                    roleManager.Create(role);
                }

                // Add user admin to Role Admin if not already added
                var rolesForUser = userManager.GetRoles(usuario.Id);
                if (!rolesForUser.Contains(role.Name))
                {
                    userManager.AddToRole(usuario.Id, role.Name);
                }
            }
        }
    }
}