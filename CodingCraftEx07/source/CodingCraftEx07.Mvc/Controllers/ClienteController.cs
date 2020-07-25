using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodingCraftEx07.Mvc.Models;

namespace CodingCraftEx07.Mvc.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public async Task<ActionResult> Index()
        {
            var clientes = (await _redisCacheClient.SearchKeysAsync("cliente:*"))
                .Select(x => _redisCacheClient.Get<Cliente>(x)).ToList();

            if (clientes.Count == 0)
            {
                clientes = await _db.Clientes.ToListAsync();

                foreach (var cliente in clientes)
                    await _redisCacheClient.AddAsync("cliente:" + cliente.ClienteId, cliente, TimeSpan.FromHours(24));
            }

            return View(clientes);
        }

        // GET: Cliente/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var cliente = await _redisCacheClient.GetAsync<Cliente>("cliente:" + id) ??
                          await _db.Clientes.FindAsync(id);

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
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ClienteId,Nome,Documento,Telefone")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.ClienteId = Guid.NewGuid();
                _db.Clientes.Add(cliente);
                await _db.SaveChangesAsync();

                await _redisCacheClient.AddAsync("cliente:" + cliente.ClienteId, cliente, TimeSpan.FromHours(24));

                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cliente = await _db.Clientes.FindAsync(id);
            if (cliente == null)
                return HttpNotFound();
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClienteId,Nome,Documento,Telefone")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cliente).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                await _redisCacheClient.ReplaceAsync("cliente:" + cliente.ClienteId, cliente, TimeSpan.FromHours(24));

                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cliente = await _db.Clientes.FindAsync(id);
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
            var cliente = await _db.Clientes.FindAsync(id);

            _db.Clientes.Remove(cliente);
            await _db.SaveChangesAsync();

            await _redisCacheClient.RemoveAsync("cliente:" + id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();
            base.Dispose(disposing);
        }

        public async Task<ActionResult> ExportExcel()
        {
            var clientes = (await _redisCacheClient.SearchKeysAsync("cliente:*"))
                .Select(x => _redisCacheClient.Get<Cliente>(x)).ToList();

            if (!clientes.Any())
                clientes = await _db.Clientes.ToListAsync();

            var gv = new GridView {DataSource = clientes};
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=CodingCraftExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";

            var objStringWriter = new StringWriter();
            var objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index", "Home");
        }
    }
}