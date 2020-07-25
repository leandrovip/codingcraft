using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CodingCraft.Ex02.Models;

namespace CodingCraft.Ex02.Controllers
{
    public class GrupoProdutoController : Controller
    {
        //
        // GET: /GrupoProduto/
        public ViewResult Index()
        {
            return
                View(
                    db.GrupoProdutos.Include(grupoproduto => grupoproduto.CategoriaProduto)
                        .Include(grupoproduto => grupoproduto.Produtos)
                        .Include(grupoproduto => grupoproduto.GrupoProdutoTags)
                        .ToList());
        }

        //
        // GET: /GrupoProduto/Detalhes/5
        public ViewResult Detalhes(Guid id)
        {
            var grupoproduto = db.GrupoProdutos.Single(x => x.GrupoProdutoId == id);
            return View(grupoproduto);
        }

        //
        // GET: /GrupoProduto/Criar
        public ActionResult Criar()
        {
            ViewBag.CategoriaProdutoId = new SelectList(db.CategoriaProdutos, "CategoriaProdutoId", "Nome");
            return View();
        }

        //
        // POST: /GrupoProduto/Criar
        [HttpPost]
        public ActionResult Criar(GrupoProduto grupoproduto)
        {
            if (ModelState.IsValid && grupoproduto.CategoriaProdutoId != Guid.Empty)
            {
                grupoproduto.GrupoProdutoId = Guid.NewGuid();
                db.GrupoProdutos.Add(grupoproduto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaProdutoId = new SelectList(db.CategoriaProdutos, "CategoriaProdutoId", "Nome", grupoproduto.CategoriaProdutoId);
            return View(grupoproduto);
        }

        //
        // GET: /GrupoProduto/Editar/5
        public ActionResult Editar(Guid id)
        {
            var grupoproduto = db.GrupoProdutos.Single(x => x.GrupoProdutoId == id);
            ViewBag.CategoriaProdutoId = new SelectList(db.CategoriaProdutos, "CategoriaProdutoId", "Nome", grupoproduto.CategoriaProdutoId);
            return View(grupoproduto);
        }

        //
        // POST: /GrupoProduto/Editar/5
        [HttpPost]
        public ActionResult Editar(GrupoProduto grupoproduto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupoproduto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaProdutoId = new SelectList(db.CategoriaProdutos, "CategoriaProdutoId", "Nome", grupoproduto.CategoriaProdutoId);
            return View(grupoproduto);
        }

        //
        // GET: /GrupoProduto/Excluir/5
        public ActionResult Excluir(Guid id)
        {
            var grupoproduto = db.GrupoProdutos.Single(x => x.GrupoProdutoId == id);
            return View(grupoproduto);
        }

        //
        // POST: /GrupoProduto/Excluir/5
        [HttpPost, ActionName("Excluir")]
        public ActionResult ConfirmarExclusao(Guid id)
        {
            var grupoproduto = db.GrupoProdutos.Single(x => x.GrupoProdutoId == id);
            db.GrupoProdutos.Remove(grupoproduto);
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