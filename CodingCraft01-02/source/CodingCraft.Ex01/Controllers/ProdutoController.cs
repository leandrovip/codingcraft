using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using CodingCraft.Ex01.Models;

namespace CodingCraft.Ex01.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produtos
        public async Task<ActionResult> Index()
        {
            var produtos = db.Produtos.Include(p => p.Categoria);
            return View(await produtos.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var produto = await db.Produtos.FindAsync(id);
            if (produto == null)
                return HttpNotFound();

            return View(produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(
                Include = "ProdutoId,Descricao,Valor,CategoriaId,DataEdicao,UsuarioEdicao,DataCadastro,UsuarioCadastro")
            ] Produto produto)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    produto.ProdutoId = Guid.NewGuid();
                    db.Produtos.Add(produto);
                    await db.SaveChangesAsync();

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var produto = await db.Produtos.FindAsync(id);
            if (produto == null)
                return HttpNotFound();

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(
                Include = "ProdutoId,Descricao,Valor,CategoriaId,DataEdicao,UsuarioEdicao,DataCadastro,UsuarioCadastro")
            ] Produto produto)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(produto).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var produto = await db.Produtos.FindAsync(id);
            if (produto == null)
                return HttpNotFound();

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var produto = await db.Produtos.FindAsync(id);
                db.Produtos.Remove(produto);
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

        [HttpPost]
        public async Task<ActionResult> Pesquisar(string pesquisa)
        {
            var produto = await db.Produtos
                .Where(p => p.Descricao.Contains(pesquisa))
                .ToListAsync();

            return View("Index", produto);
        }
    }
}