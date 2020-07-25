using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using VipControle.Domain.Extensions;
using VipControle.Domain.Interfaces;
using VipControle.Domain.Models;

namespace VipControle.Domain.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Atendente> Atendentes { get; set; }
        public DbSet<TipoAtendimento> TiposAtendimento { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Cfop> Cfop { get; set; }
        public DbSet<CstIcms> CstIcms { get; set; }
        public DbSet<CstIpi> CstIpi { get; set; }
        public DbSet<CstPis> CstPis { get; set; }
        public DbSet<CstCofins> CstCofins { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", false) { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar").HasMaxLength(100));

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync()
        {
            try
            {
                VerificaEntidades(ChangeTracker.Entries());
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErroMessage = string.Join(";", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, "Os erros de validação são: ", fullErroMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public void VerificaEntidades(IEnumerable<DbEntityEntry> entries)
        {
            var dataAtual = DateTime.Now.NowToBrazil();

            foreach (var entry in entries.Where(e =>
                e.Entity != null && typeof(IEntidadeNaoEditavel).IsAssignableFrom(e.Entity.GetType())))
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property("DataCadastro") != null)
                        entry.Property("DataCadastro").CurrentValue = dataAtual;

                    if (entry.Property("UsuarioCadastro") != null)
                        entry.Property("UsuarioCadastro").CurrentValue = HttpContext.Current != null &&
                                                                         HttpContext.Current.User.Identity.Name !=
                                                                         string.Empty
                            ? HttpContext.Current.User.Identity.Name
                            : "Usuario";
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                    entry.Property("UsuarioCadastro").IsModified = false;

                    if (entry.Property("DataEdicao") != null)
                        entry.Property("DataEdicao").CurrentValue = dataAtual;

                    if (entry.Property("UsuarioEdicao") != null)
                        entry.Property("UsuarioEdicao").CurrentValue = HttpContext.Current != null &&
                                                                       HttpContext.Current.User.Identity.Name !=
                                                                       string.Empty
                            ? HttpContext.Current.User.Identity.Name
                            : "Usuario";
                }
            }
        }
    }
}