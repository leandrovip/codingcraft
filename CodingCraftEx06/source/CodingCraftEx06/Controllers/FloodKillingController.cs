using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using CodingCraftEx06.Models;
using CodingCraftEx06.Models.Context;
using Dapper;

namespace CodingCraftEx06.Controllers
{
    public class FloodKillingController : Controller
    {
        private readonly FloodContext db = new FloodContext();

        // GET: FloodKilling
        public async Task<ActionResult> Index()
        {
            const string sqlQuery = "SELECT f.FloodKillingId, f.Amount, f.Year, c.CountryId, c.Name " +
                                    "FROM FloodKillings f " +
                                    "INNER JOIN Countries c ON c.CountryId = f.CountryId " +
                                    "ORDER BY c.Name";

            var floodKillings = await db.Database.Connection.QueryAsync<FloodKilling, Country, FloodKilling>(
                sqlQuery,
                (flood, country) =>
                {
                    flood.Country = country;
                    return flood;
                },
                splitOn: "Year,CountryId"
            );

            return View(floodKillings);
        }

        // GET: FloodKilling/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var floodKilling = await db.FloodKillings.FindAsync(id);
            if (floodKilling == null)
                return HttpNotFound();
            return View(floodKilling);
        }

        // GET: FloodKilling/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name");
            return View();
        }

        // POST: FloodKilling/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "FloodKillingId,CountryId,Amount,Year")] FloodKilling floodKilling)
        {
            if (ModelState.IsValid)
            {
                floodKilling.FloodKillingId = Guid.NewGuid();
                db.FloodKillings.Add(floodKilling);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name", floodKilling.CountryId);
            return View(floodKilling);
        }

        // GET: FloodKilling/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var floodKilling = await db.FloodKillings.FindAsync(id);
            if (floodKilling == null)
                return HttpNotFound();
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name", floodKilling.CountryId);
            return View(floodKilling);
        }

        // POST: FloodKilling/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "FloodKillingId,CountryId,Amount,Year")] FloodKilling floodKilling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(floodKilling).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name", floodKilling.CountryId);
            return View(floodKilling);
        }

        // GET: FloodKilling/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var floodKilling = await db.FloodKillings.FindAsync(id);
            if (floodKilling == null)
                return HttpNotFound();
            return View(floodKilling);
        }

        // POST: FloodKilling/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var floodKilling = await db.FloodKillings.FindAsync(id);

            if (floodKilling == null)
                return RedirectToAction("Index");

            db.FloodKillings.Remove(floodKilling);
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