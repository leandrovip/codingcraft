using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CodingCraft.Ex02.Models;

namespace CodingCraft.Ex02.Controllers
{
    public class TagController : Controller
    {
        //
        // GET: /Tag/
        public ViewResult Index()
        {
            return View(db.Tags.Include(tag => tag.GrupoProdutoTags).ToList());
        }

        //
        // GET: /Tag/Detalhes/5
        public ViewResult Detalhes(Guid id)
        {
            var tag = db.Tags.Single(x => x.TagId == id);
            return View(tag);
        }

        //
        // GET: /Tag/Criar
        public ActionResult Criar()
        {
            return View();
        }

        //
        // POST: /Tag/Criar
        [HttpPost]
        public ActionResult Criar(Tag tag)
        {
            if (ModelState.IsValid)
            {
                tag.TagId = Guid.NewGuid();
                db.Tags.Add(tag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        //
        // GET: /Tag/Editar/5
        public ActionResult Editar(Guid id)
        {
            var tag = db.Tags.Single(x => x.TagId == id);
            return View(tag);
        }

        //
        // POST: /Tag/Editar/5
        [HttpPost]
        public ActionResult Editar(Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        //
        // GET: /Tag/Excluir/5
        public ActionResult Excluir(Guid id)
        {
            var tag = db.Tags.Single(x => x.TagId == id);
            return View(tag);
        }

        //
        // POST: /Tag/Excluir/5
        [HttpPost, ActionName("Excluir")]
        public ActionResult ConfirmarExclusao(Guid id)
        {
            var tag = db.Tags.Single(x => x.TagId == id);
            db.Tags.Remove(tag);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}