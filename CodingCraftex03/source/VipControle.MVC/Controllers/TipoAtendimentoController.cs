using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using VipControle.Domain.Models;
using Controller = VipControle.MVC.Core.Controllers.Controller;

namespace VipControle.MVC.Controllers
{
    public class TipoAtendimentoController : Controller
    {
        // GET: TipoAtendimento
        public async Task<ActionResult> Index()
        {
            return View(await db.TiposAtendimento.ToListAsync());
        }

        // GET: TipoAtendimento/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tipoAtendimento = await db.TiposAtendimento.FindAsync(id);
            if (tipoAtendimento == null)
                return HttpNotFound();
            return View(tipoAtendimento);
        }

        // GET: TipoAtendimento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoAtendimento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "TipoAtendimentoId,Descricao")] TipoAtendimento tipoAtendimento)
        {
            if (ModelState.IsValid)
            {
                tipoAtendimento.TipoAtendimentoId = Guid.NewGuid();
                db.TiposAtendimento.Add(tipoAtendimento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoAtendimento);
        }

        // GET: TipoAtendimento/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tipoAtendimento = await db.TiposAtendimento.FindAsync(id);
            if (tipoAtendimento == null)
                return HttpNotFound();
            return View(tipoAtendimento);
        }

        // POST: TipoAtendimento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "TipoAtendimentoId,Descricao")] TipoAtendimento tipoAtendimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoAtendimento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoAtendimento);
        }

        // GET: TipoAtendimento/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tipoAtendimento = await db.TiposAtendimento.FindAsync(id);
            if (tipoAtendimento == null)
                return HttpNotFound();
            return View(tipoAtendimento);
        }

        // POST: TipoAtendimento/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var tipoAtendimento = await db.TiposAtendimento.FindAsync(id);
            if (tipoAtendimento == null)
                return View("Error");

            if (db.Atendimentos.Any(x => x.TipoAtendimentoId == id))
            {
                ViewBag.Mensagem = "Impossível excluir o tipo de atendimento selecionado pois existem Atendimentos cadastrados";
                return View("MensagemRetorno");
            }

            db.TiposAtendimento.Remove(tipoAtendimento);
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