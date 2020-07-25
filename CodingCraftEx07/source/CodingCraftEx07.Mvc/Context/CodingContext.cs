using System.Data.Entity;
using CodingCraftEx07.Mvc.Models;

namespace CodingCraftEx07.Mvc.Context
{
    public class CodingContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public CodingContext()
            : base("aspnet-CodingCraftEx07") { }
    }
}