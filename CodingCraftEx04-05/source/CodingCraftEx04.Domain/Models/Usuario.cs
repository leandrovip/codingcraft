using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraftEx04.Domain.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Usuario : IdentityUser<Guid, UsuarioLogin, UsuarioGrupo, UsuarioIdentificacao>
    {
        public virtual ICollection<UsuarioEndereco> Enderecos { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Usuario, Guid> manager,
            string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            
            return userIdentity;
        }
    }
}