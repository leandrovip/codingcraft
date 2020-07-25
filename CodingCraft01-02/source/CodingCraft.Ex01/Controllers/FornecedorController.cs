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
    public class FornecedorController : Controller
    {
        // GET: Fornecedor
        public async Task<ActionResult> Index()
        {
            return View(await db.Fornecedores.ToListAsync());
        }

        // GET: Fornecedor/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fornecedor = await db.Fornecedores.FindAsync(id);
            if (fornecedor == null)
                return HttpNotFound();

            return View(fornecedor);
        }

        // GET: Fornecedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fornecedor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "FornecedorId,RazaoSocial,Email,DataEdicao,UsuarioEdicao,DataCadastro,UsuarioCadastro")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    fornecedor.FornecedorId = Guid.NewGuid();
                    db.Fornecedores.Add(fornecedor);
                    await db.SaveChangesAsync();

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }

            return View(fornecedor);
        }

        // GET: Fornecedor/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fornecedor = await db.Fornecedores.FindAsync(id);
            if (fornecedor == null)
                return HttpNotFound();

            return View(fornecedor);
        }

        // POST: Fornecedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "FornecedorId,RazaoSocial,Email,DataEdicao,UsuarioEdicao,DataCadastro,UsuarioCadastro")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(fornecedor).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            return View(fornecedor);
        }

        // GET: Fornecedor/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var fornecedor = await db.Fornecedores.FindAsync(id);
            if (fornecedor == null)
                return HttpNotFound();

            return View(fornecedor);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var fornecedor = await db.Fornecedores.FindAsync(id);
                db.Fornecedores.Remove(fornecedor);
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
            var fornecedor = await db.Fornecedores
                .Where(f => f.RazaoSocial.Contains(pesquisa))
                .ToListAsync();

            return View("Index", fornecedor);
        }
    }
}