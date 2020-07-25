using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CodingCraft.Ex02.Models;

namespace CodingCraft.Ex02.Controllers
{
    public class GrupoProdutoTagController : Controller
    {
        //
        // GET: /GrupoProdutoTag/
        public ViewResult Index()
        {
            return
                View(
                    db.GrupoProdutoTags.Include(grupoprodutotag => grupoprodutotag.GrupoProduto)
                        .Include(grupoprodutotag => grupoprodutotag.Tag)
                        .ToList());
        }

        //
        // GET: /GrupoProdutoTag/Criar
        public ActionResult Criar()
        {
            ViewBag.GrupoProdutoId = new SelectList(db.GrupoProdutos, "GrupoProdutoId", "Nome");
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "Nome");
            return View();
        }

        //
        // POST: /GrupoProdutoTag/Criar
        [HttpPost]
        public ActionResult Criar(GrupoProdutoTag grupoprodutotag)
        {
            if (ModelState.IsValid)
            {
                grupoprodutotag.GrupoProdutoTagId = Guid.NewGuid();
                db.GrupoProdutoTags.Add(grupoprodutotag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GrupoProdutoId = new SelectList(db.GrupoProdutos, "GrupoProdutoId", "Nome",
                grupoprodutotag.GrupoProdutoId);
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "Nome", grupoprodutotag.TagId);
            return View(grupoprodutotag);
        }

        //
        // GET: /GrupoProdutoTag/Excluir/5
        public ActionResult Excluir(Guid id)
        {
            var grupoprodutotag = db.GrupoProdutoTags.Single(x => x.GrupoProdutoTagId == id);
            return View(grupoprodutotag);
        }

        //
        // POST: /GrupoProdutoTag/Excluir/5
        [HttpPost, ActionName("Excluir")]
        public ActionResult ConfirmarExclusao(Guid id)
        {
            var grupoprodutotag = db.GrupoProdutoTags.Single(x => x.GrupoProdutoTagId == id);
            db.GrupoProdutoTags.Remove(grupoprodutotag);
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