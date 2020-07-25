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
    [Authorize]
    public class ClienteController : Controller
    {
        // GET: Cliente
        public async Task<ActionResult> Index()
        {
            return View(await db.Clientes.OrderBy(x => x.RazaoSocial).ToListAsync());
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include =
                "ClienteId,RazaoSocial,Fantasia,CpfCnpj,Telefone,Celular,NomeContato,Email,Logradouro,Numero,Bairro,Cidade,Estado,Cep,Observacao")]
            Cliente cliente)
        {
            if (!ModelState.IsValid) return View(cliente);

            cliente.ClienteId = Guid.NewGuid();
            db.Clientes.Add(cliente);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include =
                "ClienteId,RazaoSocial,Fantasia,CpfCnpj,Telefone,Celular,NomeContato,Email,Logradouro,Numero,Bairro,Cidade,Estado,Cep,Observacao")]
            Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
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
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = await db.Clientes.FindAsync(id);

            #region Validação

            if (cliente == null)
                return View("Error");

            if (db.Atendimentos.Any(x => x.ClienteId == id))
            {
                ViewBag.Mensagem = "Impossível excluir o cliente selecionado pois existem Atendimentos cadastrados";
                return View("MensagemRetorno");
            }

            #endregion


            db.Clientes.Remove(cliente);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }

        public async Task<ActionResult> Pesquisar(string pesquisa)
        {
            var clientes = await db.Clientes
                .Where(c => c.Fantasia.Contains(pesquisa))
                .ToListAsync();

            return View("Index", clientes);
        }
    }
}