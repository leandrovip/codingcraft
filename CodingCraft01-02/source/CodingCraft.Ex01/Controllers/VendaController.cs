using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using CodingCraft.Ex01.Models;

namespace CodingCraft.Ex01.Controllers
{
    public class VendaController : Controller
    {
        // GET: Venda
        public async Task<ActionResult> Index()
        {
            var vendas = await db.Vendas.Include(v => v.Cliente).ToListAsync();
            return View(vendas);
        }

        // GET: Venda/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var venda = await db.Vendas.FindAsync(id);

            if (venda == null)
                return HttpNotFound();

            return View(venda);
        }

        // GET: Venda/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome");
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Descricao");

            return View();
        }

        // POST: Venda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(
                Include =
                    "VendaId,ClienteId,DataVenda,ValorVenda,DataEdicao,UsuarioEdicao,DataCadastro,UsuarioCadastro,ItensVenda"
                )] Venda venda)
        {
            if (ModelState.IsValid && venda.ItensVenda != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    venda.VendaId = Guid.NewGuid();
                    venda.ValorVenda = venda.ItensVenda.Sum(item => item.ValorTotal);

                    venda.ItensVenda.Select(item =>
                    {
                        item.ItemVendaId = Guid.NewGuid();
                        item.Produto = null;
                        return item;
                    }).ToList();

                    db.Vendas.Add(venda);
                    await db.SaveChangesAsync();

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome", venda.ClienteId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Descricao");
            return View(venda);
        }

        // GET: Venda/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var venda = await db.Vendas.FindAsync(id);

            if (venda == null)
                return HttpNotFound();

            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome", venda.ClienteId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Descricao");

            return View(venda);
        }

        // POST: Venda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(
                Include =
                    "VendaId,ClienteId,DataVenda,ValorVenda,DataEdicao,UsuarioEdicao,DataCadastro,UsuarioCadastro,ItensVenda"
                )] Venda venda)
        {
            if (ModelState.IsValid && venda.ItensVenda != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    venda.ItensVenda.Select(item =>
                    {
                        item.VendaId = venda.VendaId;
                        item.Produto = null;
                        return item;
                    }).ToList();

                    var listaItensVenda = venda.ItensVenda.Select(item => item.ItemVendaId).ToList();

                    var itensDeletar =
                        db.ItensVenda.Where(
                            item => item.VendaId == venda.VendaId && !listaItensVenda.Contains(item.ItemVendaId));
                    db.ItensVenda.RemoveRange(itensDeletar);
                    //await db.SaveChangesAsync();

                    foreach (var itemVenda in venda.ItensVenda)
                    {
                        itemVenda.Produto = null;

                        if (itemVenda.ItemVendaId == Guid.Empty)
                        {
                            itemVenda.ItemVendaId = Guid.NewGuid();
                            db.ItensVenda.Add(itemVenda);
                        }
                        else
                        {
                            db.Entry(itemVenda).State = EntityState.Modified;
                        }
                    }

                    venda.ValorVenda = venda.ItensVenda.Sum(item => item.ValorTotal);

                    db.Entry(venda).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }

            if (venda.ItensVenda == null)
                venda.ItensVenda = new List<ItemVenda>();

            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nome", venda.ClienteId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Descricao");

            return View(venda);
        }

        // GET: Venda/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var venda = await db.Vendas.FindAsync(id);
            if (venda == null)
                return HttpNotFound();

            return View(venda);
        }

        // POST: Venda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var venda = await db.Vendas.FindAsync(id);
                db.Vendas.Remove(venda);
                await db.SaveChangesAsync();

                scope.Complete();
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }

        public async Task<ActionResult> AdicionarItemVenda(Guid id, string quantidade)
        {
            var model = new ItemVenda
            {
                ProdutoId = id,
                Produto = await db.Produtos.FindAsync(id),
                Quantidade = int.Parse(quantidade)
            };

            model.Valor = model.Produto.Valor;
            model.ValorTotal = model.Quantidade * model.Valor;

            return PartialView("_ItensVenda", model);
        }

        [HttpPost]
        public async Task<ActionResult> Pesquisar(string pesquisa)
        {
            var cliente = await db.Vendas.Include(v => v.Cliente)
                .Where(c => c.Cliente.Nome.Contains(pesquisa))
                .ToListAsync();

            return View("Index", cliente);
        }
    }
}