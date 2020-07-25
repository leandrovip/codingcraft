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
    public class ClienteController : Controller
    {
        // GET: Cliente
        public async Task<ActionResult> Index()
        {
            return View(await db.Clientes.ToListAsync());
        }

        // GET: Cliente/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
                return HttpNotFound();

            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "ClienteId,Nome,Email,DataEdicao,UsuarioEdicao,DataCadastro,UsuarioCadastro")] Cliente
                cliente)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    cliente.ClienteId = Guid.NewGuid();
                    db.Clientes.Add(cliente);
                    await db.SaveChangesAsync();

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }

            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
                return HttpNotFound();

            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "ClienteId,Nome,Email,DataEdicao,UsuarioEdicao,DataCadastro,UsuarioCadastro")] Cliente
                cliente)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(cliente).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
                return HttpNotFound();

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var cliente = await db.Clientes.FindAsync(id);
                db.Clientes.Remove(cliente);
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
            var cliente = await db.Clientes
                .Where(c => c.Nome.Contains(pesquisa))
                .ToListAsync();

            return View("Index", cliente);
        }
    }
}