using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CodingCraft.Ex02.Models;

namespace CodingCraft.Ex02.Controllers
{
    public class ProdutoController : Controller
    {
        //
        // GET: /Produto/
        public ViewResult Index()
        {
            return View(db.Produtos.Include(produto => produto.GrupoProduto).ToList());
        }

        //
        // GET: /Produto/Detalhes/5
        public ViewResult Detalhes(Guid id)
        {
            var produto = db.Produtos.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // GET: /Produto/Criar
        public ActionResult Criar()
        {
            ViewBag.GrupoProdutoId = new SelectList(db.GrupoProdutos, "GrupoProdutoId", "Nome");
            return View();
        }

        //
        // POST: /Produto/Criar
        [HttpPost]
        public ActionResult Criar(Produto produto)
        {
            if (ModelState.IsValid && produto.GrupoProdutoId != Guid.Empty)
            {
                produto.ProdutoId = Guid.NewGuid();
                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GrupoProdutoId = new SelectList(db.GrupoProdutos, "GrupoProdutoId", "Nome", produto.GrupoProdutoId);
            return View(produto);
        }

        //
        // GET: /Produto/Editar/5
        public ActionResult Editar(Guid id)
        {
            var produto = db.Produtos.Single(x => x.ProdutoId == id);
            ViewBag.GrupoProdutoId = new SelectList(db.GrupoProdutos, "GrupoProdutoId", "Nome",
                produto.GrupoProdutoId);
            return View(produto);
        }

        //
        // POST: /Produto/Editar/5
        [HttpPost]
        public ActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GrupoProdutoId = new SelectList(db.GrupoProdutos, "GrupoProdutoId", "Nome",
                produto.GrupoProdutoId);
            return View(produto);
        }

        //
        // GET: /Produto/Excluir/5
        public ActionResult Excluir(Guid id)
        {
            var produto = db.Produtos.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // POST: /Produto/Excluir/5
        [HttpPost, ActionName("Excluir")]
        public ActionResult ConfirmarExclusao(Guid id)
        {
            var produto = db.Produtos.Single(x => x.ProdutoId == id);
            db.Produtos.Remove(produto);
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