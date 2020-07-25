using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CodingCraft.Ex01.Models.Identity;
using CodingCraft.Ex01.Models.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraft.Ex01.Models.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<ItemCompra> ItensCompra { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override Task<int> SaveChangesAsync()
        {
            try
            {
                ChecaEntidades(ChangeTracker.Entries());
                return base.SaveChangesAsync();
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

        public void ChecaEntidades(IEnumerable<DbEntityEntry> entries)
        {
            var dataAtual = DateTime.Now;

            foreach (
                var entry in
                    entries.Where(
                        e => e.Entity != null && typeof (IEntidadeNaoEditavel).IsAssignableFrom(e.Entity.GetType())))
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