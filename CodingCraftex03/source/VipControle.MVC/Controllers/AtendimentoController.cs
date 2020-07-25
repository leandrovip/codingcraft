using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using VipControle.Domain.Enum;
using VipControle.Domain.Models;
using Controller = VipControle.MVC.Core.Controllers.Controller;

namespace VipControle.MVC.Controllers
{
    public class AtendimentoController : Controller
    {
        // GET: Atendimento
        public async Task<ActionResult> Index()
        {
            var atendimentos = db.Atendimentos.Include(a => a.Atendente).Include(a => a.Cliente)
                .Include(a => a.TipoAtendimento).OrderByDescending(x => x.DataCadastro);
            return View(await atendimentos.ToListAsync());
        }

        // GET: Atendimento/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var atendimento = await db.Atendimentos.FindAsync(id);
            if (atendimento == null)
                return HttpNotFound();
            return View(atendimento);
        }

        // GET: Atendimento/Create
        public ActionResult Create()
        {
            ViewBag.AtendenteId = new SelectList(db.Atendentes.OrderBy(x => x.Nome), "AtendenteId", "Nome");
            ViewBag.ClienteId = new SelectList(db.Clientes.OrderBy(x => x.Fantasia), "ClienteId", "Fantasia");
            ViewBag.TipoAtendimentoId = new SelectList(db.TiposAtendimento.OrderBy(x => x.Descricao), "TipoAtendimentoId", "Descricao");
            return View();
        }

        // POST: Atendimento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include =
                "AtendimentoId,ClienteId,AtendenteId,TipoAtendimentoId,Solicitante,Status,Prioridade,DescricaoAtendimento,DescricaoConclusao,Observacao,DataPrevisao,DataConclusao")]
            Atendimento atendimento)
        {
            #region Validações

            if (atendimento.ClienteId == Guid.Empty)
                ModelState.AddModelError("Cliente", "Nenhum cliente selecionado");

            if (atendimento.AtendenteId == Guid.Empty)
                ModelState.AddModelError("Atendente", "Nenhum atendente selecionado");

            if (atendimento.TipoAtendimentoId == Guid.Empty)
                ModelState.AddModelError("TipoAtendimento", "Nenhum tipo de atendimento selecionado.");

            if ((atendimento.Status == Status.Concluido) && (atendimento.DataConclusao == null))
                ModelState.AddModelError("Conclusao", "É necessário preencher a data de conclusão para alterar este status");

            #endregion

            if (ModelState.IsValid)
            {
                atendimento.AtendimentoId = Guid.NewGuid();
                db.Atendimentos.Add(atendimento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AtendenteId = new SelectList(db.Atendentes, "AtendenteId", "Nome", atendimento.AtendenteId);
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Fantasia", atendimento.ClienteId);
            ViewBag.TipoAtendimentoId = new SelectList(db.TiposAtendimento, "TipoAtendimentoId", "Descricao",
                atendimento.TipoAtendimentoId);
            return View(atendimento);
        }

        // GET: Atendimento/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var atendimento = await db.Atendimentos.FindAsync(id);

            if (atendimento == null)
                return HttpNotFound();

            ViewBag.AtendenteId = new SelectList(db.Atendentes, "AtendenteId", "Nome", atendimento.AtendenteId);
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Fantasia", atendimento.ClienteId);
            ViewBag.TipoAtendimentoId = new SelectList(db.TiposAtendimento, "TipoAtendimentoId", "Descricao",
                atendimento.TipoAtendimentoId);

            return View(atendimento);
        }

        // POST: Atendimento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include =
                "AtendimentoId,ClienteId,AtendenteId,TipoAtendimentoId,Solicitante,Status,Prioridade,DescricaoAtendimento,DescricaoConclusao,Observacao,DataPrevisao,DataConclusao")]
            Atendimento atendimento)
        {

            #region Validações

            if ((atendimento.Status == Status.Concluido) && (atendimento.DataConclusao == null))
                ModelState.AddModelError("Conclusao", "É necessário preencher a data de conclusão para alterar este status");

            #endregion


            if (ModelState.IsValid)
            {
                db.Entry(atendimento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AtendenteId = new SelectList(db.Atendentes, "AtendenteId", "Nome", atendimento.AtendenteId);
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Fantasia", atendimento.ClienteId);
            ViewBag.TipoAtendimentoId = new SelectList(db.TiposAtendimento, "TipoAtendimentoId", "Descricao",
                atendimento.TipoAtendimentoId);
            return View(atendimento);
        }

        // GET: Atendimento/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var atendimento = await db.Atendimentos.FindAsync(id);
            if (atendimento == null)
                return HttpNotFound();
            return View(atendimento);
        }

        // POST: Atendimento/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var atendimento = await db.Atendimentos.FindAsync(id);
            db.Atendimentos.Remove(atendimento);
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