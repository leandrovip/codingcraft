using System;
using System.Data.Entity;
using CodingCraftEx04.Domain.Infra.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraftEx04.Domain.Models.Context
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Grupo, Guid, UsuarioLogin, UsuarioGrupo, UsuarioIdentificacao>
    {
        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios", "dbo").Property(p => p.Id).HasColumnName("Id");

            modelBuilder.Entity<UsuarioGrupo>()
                .HasKey(r => new { r.UserId, r.RoleId })
                .ToTable("UsuariosGrupos");

            modelBuilder.Entity<UsuarioLogin>()
                .HasKey(r => new { r.LoginProvider, r.ProviderKey, r.UserId })
                .ToTable("UsuariosLogins");

            modelBuilder.Entity<Grupo>()
                .HasKey(r => new { r.Id })
                .ToTable("Grupos");

            modelBuilder.Entity<UsuarioIdentificacao>()
                .HasKey(r => new { r.Id })
                .ToTable("UsuariosIdentificacoes");

            modelBuilder.Entity<Arquivo>()
                .HasKey(p => new {p.ArquivoId})
                .ToTable("Arquivos");

            modelBuilder.Entity<ArquivoDetalhe>()
                .HasKey(p => new { p.ArquivoDetalheId })
                .ToTable("ArquivosDetalhes");
        }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            //base.Configuration.ProxyCreationEnabled = false;
        }

        public static ApplicationDbContext Create()

        {
            return new ApplicationDbContext();
        }

        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<ArquivoDetalhe> ArquivosDetalhes { get; set; }
    }
}