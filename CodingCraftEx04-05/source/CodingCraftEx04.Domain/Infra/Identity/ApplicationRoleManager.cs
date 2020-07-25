using System;
using CodingCraftEx04.Domain.Models;
using CodingCraftEx04.Domain.Models.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace CodingCraftEx04.Domain.Infra.Identity
{
    public class ApplicationRoleManager : RoleManager<Grupo,Guid>
    {
        public ApplicationRoleManager(IRoleStore<Grupo,Guid> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var dbContext = context != null ? context.Get<ApplicationDbContext>() : new ApplicationDbContext();
            return new ApplicationRoleManager(new RoleStore<Grupo,Guid,UsuarioGrupo>(dbContext));
        }
    }
}