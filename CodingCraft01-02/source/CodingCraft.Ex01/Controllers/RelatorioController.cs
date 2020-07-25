using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CodingCraft.Ex01.ViewModel;

namespace CodingCraft.Ex01.Controllers
{
    public class RelatorioController : Controller
    {
        public ActionResult VendasPorPeriodo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VendasPorPeriodo(string dataInicial, string dataFinal)
        {
            DateTime dataInicio;
            DateTime dataFim;

            if (!DateTime.TryParse(dataInicial, out dataInicio) || !DateTime.TryParse(dataFinal, out dataFim))
                return View();

            var relatorio = db.Vendas
                .Include(x => x.Cliente)
                .Where(x => x.DataVenda >= dataInicio && x.DataVenda <= dataFim)
                .ToList()
                .GroupBy(x => x.ClienteId)
                .Select(x => new VendasPorPeriodoViewModel
                {
                    Nome = x.ElementAt(0).Cliente.Nome,
                    Valor = x.Sum(s => s.ValorVenda),
                    QuantidadeVendas = x.Count()
                }).ToList();

            return View(relatorio);
        }
    }
}