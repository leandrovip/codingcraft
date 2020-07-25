using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using CodingCraft.Ex02.Models.Identity;
using CodingCraft.Ex02.Models.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraft.Ex02.Models
{
    public class CodingCraftEx02Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<GrupoProduto> GrupoProdutos { get; set; }
        public DbSet<CategoriaProduto> CategoriaProdutos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<GrupoProdutoTag> GrupoProdutoTags { get; set; }

        public CodingCraftEx02Context()
            : base("DefaultConnection", false)
        {
        }

        public static CodingCraftEx02Context Create()
        {
            return new CodingCraftEx02Context();
        }

        public override int SaveChanges()
        {
            try
            {
                ChecaEntidades(ChangeTracker.Entries());
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join(";", errorMessages);

                var excpetionsMessage = string.Concat(ex.Message, "Os erros de validações são: ", fullErrorMessage);

                throw new DbEntityValidationException(excpetionsMessage, ex.EntityValidationErrors);
            }
        }

        private void ChecaEntidades(IEnumerable<DbEntityEntry> entries)
        {
            var dataAtual = DateTime.Now;

            foreach (var entry in entries.Where(e => e.Entity != null && e.Entity is IEntidadeNaoEditavel))
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property("DataCadastro") != null)
                        entry.Property("DataCadastro").CurrentValue = dataAtual;

                    if (entry.Property("UsuarioCadastro") != null)
                        entry.Property("UsuarioCadastro").CurrentValue =
                            HttpContext.Current != null && HttpContext.Current.User.Identity.Name != string.Empty
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
                        entry.Property("UsuarioEdicao").CurrentValue =
                            HttpContext.Current != null && HttpContext.Current.User.Identity.Name != string.Empty
                                ? HttpContext.Current.User.Identity.Name
                                : "UsuarioEdicao";
                }
            }
        }
    }
}