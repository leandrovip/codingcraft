using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using VipControle.Domain.Data.Context;
using VipControle.Domain.Models;
using Controller = VipControle.MVC.Core.Controllers.Controller;

namespace VipControle.MVC.Controllers
{
    public class AtendenteController : Controller
    {
        // GET: Atendente
        public async Task<ActionResult> Index()
        {
            return View(await db.Atendentes.ToListAsync());
        }

        // GET: Atendente/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var atendente = await db.Atendentes.FindAsync(id);
            if (atendente == null)
                return HttpNotFound();
            return View(atendente);
        }

        // GET: Atendente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Atendente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AtendenteId,Nome,Email")] Atendente atendente)
        {
            if (ModelState.IsValid)
            {
                atendente.AtendenteId = Guid.NewGuid();
                db.Atendentes.Add(atendente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(atendente);
        }

        // GET: Atendente/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var atendente = await db.Atendentes.FindAsync(id);
            if (atendente == null)
                return HttpNotFound();
            return View(atendente);
        }

        // POST: Atendente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AtendenteId,Nome,Email")] Atendente atendente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atendente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(atendente);
        }

        // GET: Atendente/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var atendente = await db.Atendentes.FindAsync(id);
            if (atendente == null)
                return HttpNotFound();
            return View(atendente);
        }

        // POST: Atendente/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var atendente = await db.Atendentes.FindAsync(id);
            if (atendente == null)
                return View("Error");

            if (db.Atendimentos.Any(x => x.AtendenteId == id))
            {
                ViewBag.Mensagem = "Impossível excluir o atendente selecionado pois existem Atendimentos cadastrados";
                return View("MensagemRetorno");
            }

            db.Atendentes.Remove(atendente);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}