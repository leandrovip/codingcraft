using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bogus;
using Bogus.Extensions.Brazil;
using CodingCraftEx07.Mvc.Context;
using CodingCraftEx07.Mvc.Models;

namespace CodingCraftEx07.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GerarClientesFake()
        {
            var clienteFaker = new Faker<Cliente>()
                //Use an enum outside scope.
                .RuleFor(u => u.ClienteId, Guid.NewGuid)

                //Basic rules using built-in generators
                .RuleFor(u => u.Nome, (f, u) => f.Name.FullName())
                .RuleFor(u => u.Documento, (f, u) => f.Person.Cpf())
                .RuleFor(u => u.Telefone, (f, u) => f.Phone.PhoneNumber())
                .RuleFor(u => u.Telefone, (f, u) => f.Phone.PhoneNumber())

                //Optional: After all rules are applied finish with the following action
                .FinishWith((f, u) => { Console.WriteLine("User Created! Id={0}", u.ClienteId); });

            var clientes = clienteFaker.Generate(10000);

            using (var db = new CodingContext())
            {
                db.Clientes.AddRange(clientes);
                await db.SaveChangesAsync();
            }

            foreach (var cliente in clientes)
                await _redisCacheClient.AddAsync("cliente:" + cliente.ClienteId, cliente, TimeSpan.FromHours(24));

            ViewBag.Mensagem = "Clientes gerados com sucesso!";

            return View("Index");
        }

        public async Task<ActionResult> LimparCacheRedis()
        {
            await _redisCacheClient.FlushDbAsync();

            ViewBag.Mensagem = "Cache apagado com sucesso!";

            return View("Index");
        }

        public ActionResult LimparBancoDeDados()
        {
            _db.Database.ExecuteSqlCommand("TRUNCATE TABLE Clientes");

            ViewBag.Mensagem = "Banco de dados limpo com sucesso!";

            return View("Index");
        }
    }
}