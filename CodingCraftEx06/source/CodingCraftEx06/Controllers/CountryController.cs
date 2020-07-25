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
    public class CountryController : Controller
    {
        private readonly FloodContext db = new FloodContext();

        // GET: Country
        public async Task<ActionResult> Index()
        {
            return View(await db.Database.Connection.QueryAsync<Country>("SELECT * FROM Countries ORDER BY Name"));
        }

        // GET: Country/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var country = await db.Countries.FindAsync(id);
            if (country == null)
                return HttpNotFound();
            return View(country);
        }

        // GET: Country/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Country/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CountryId,Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                country.CountryId = Guid.NewGuid();
                db.Countries.Add(country);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET: Country/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var country = await db.Countries.FindAsync(id);
            if (country == null)
                return HttpNotFound();
            return View(country);
        }

        // POST: Country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CountryId,Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Country/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var country = await db.Countries.FindAsync(id);
            if (country == null)
                return HttpNotFound();
            return View(country);
        }

        // POST: Country/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var country = await db.Countries.FindAsync(id);
            db.Countries.Remove(country);
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