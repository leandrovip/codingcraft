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
    public class CompraController : Controller
    {
        // GET: Compra
        public async Task<ActionResult> Index()
        {
            var compras = db.Compras.Include(c => c.Fornecedor);
            return View(await compras.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Pesquisar(string pesquisa)
        {
            var compras = await db.Compras.Include(c => c.Fornecedor)
                                    .Where(c => c.Fornecedor.RazaoSocial.Contains(pesquisa))
                                    .ToListAsync();

            return View("Index", compras);
        }

        // GET: Compra/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var compra = await db.Compras.FindAsync(id);

            if (compra == null)
                return HttpNotFound();

            return View(compra);
        }

        // GET: Compra/Create
        public ActionResult Create()
        {
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "RazaoSocial");
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Descricao");

            return View();
        }

        // POST: Compra/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(
                Include =
                    "CompraId,FornecedorId,DataCompra,DataPagamento,ValorTotalCompra,DataEdicao,UsuarioEdicao,DataCadastro,UsuarioCadastro,ItensCompra"
                )] Compra compra)
        {
            if (ModelState.IsValid && compra.ItensCompra != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    compra.CompraId = Guid.NewGuid();
                    compra.ValorTotalCompra = compra.ItensCompra.Sum(i => i.ValorTotal);

                    compra.ItensCompra.Select(item =>
                    {
                        item.ItemCompraId = Guid.NewGuid();
                        item.Produto = null;
                        return item;
                    }).ToList();

                    db.Compras.Add(compra);
                    await db.SaveChangesAsync();

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "RazaoSocial", compra.FornecedorId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Descricao");

            return View(compra);
        }

        // GET: Compra/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var compra = await db.Compras.FindAsync(id);

            if (compra == null)
                return HttpNotFound();

            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "RazaoSocial", compra.FornecedorId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Descricao");

            return View(compra);
        }

        // POST: Compra/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(
                Include =
                    "CompraId,FornecedorId,DataCompra,DataPagamento,ValorTotalCompra,DataEdicao,UsuarioEdicao,DataCadastro,UsuarioCadastro,ItensCompra"
                )] Compra compra)
        {
            if (ModelState.IsValid && compra.ItensCompra != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    compra.ItensCompra.Select(item =>
                    {
                        item.CompraId = compra.CompraId;
                        return item;
                    }).ToList();

                    var listaItemCompras = compra.ItensCompra.Select(item => item.ItemCompraId).ToList();

                    var itensCompraDeletar =
                        db.ItensCompra.Where(
                            item => item.CompraId == compra.CompraId && !listaItemCompras.Contains(item.ItemCompraId));
                    db.ItensCompra.RemoveRange(itensCompraDeletar);
                    await db.SaveChangesAsync();

                    foreach (var itemCompra in compra.ItensCompra)
                    {
                        itemCompra.Produto = null;

                        if (itemCompra.ItemCompraId == Guid.Empty)
                        {
                            itemCompra.ItemCompraId = Guid.NewGuid();
                            db.ItensCompra.Add(itemCompra);
                        }
                        else
                        {
                            db.Entry(itemCompra).State = EntityState.Modified;
                        }

                        await db.SaveChangesAsync();
                    }

                    compra.ValorTotalCompra = compra.ItensCompra.Sum(i => i.ValorTotal);
                    db.Entry(compra).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }

            if (compra.ItensCompra == null)
                compra.ItensCompra = new List<ItemCompra>();

            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "FornecedorId", "RazaoSocial", compra.FornecedorId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Descricao");

            return View(compra);
        }

        // GET: Compra/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var compra = await db.Compras.FindAsync(id);

            if (compra == null)
                return HttpNotFound();

            return View(compra);
        }

        // POST: Compra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var compra = await db.Compras.FindAsync(id);
                db.Compras.Remove(compra);
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

        public async Task<ActionResult> AdicionaItemCompra(Guid id, string quantidade)
        {
            var model = new ItemCompra
            {
                //ItemCompraId = Guid.NewGuid(),
                ProdutoId = id,
                Produto = await db.Produtos.FindAsync(id),
                Quantidade = int.Parse(quantidade)
            };

            model.Valor = model.Produto.Valor;
            model.ValorTotal = model.Quantidade * model.Valor;

            return PartialView("_ItensCompra", model);
        }
    }
}