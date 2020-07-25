using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CodingCraft.Ex02.Models;

namespace CodingCraft.Ex02.Controllers
{
    public class CategoriaProdutoController : Controller
    {
        //
        // GET: /CategoriaProduto/
        public ViewResult Index()
        {
            return View(db.CategoriaProdutos.Include(categoriaproduto => categoriaproduto.GrupoProdutos).ToList());
        }

        //
        // GET: /CategoriaProduto/Detalhes/5
        public ViewResult Detalhes(Guid id)
        {
            var categoriaproduto = db.CategoriaProdutos.Single(x => x.CategoriaProdutoId == id);
            return View(categoriaproduto);
        }

        //
        // GET: /CategoriaProduto/Criar
        public ActionResult Criar()
        {
            return View();
        }

        //
        // POST: /CategoriaProduto/Criar
        [HttpPost]
        public ActionResult Criar(CategoriaProduto categoriaproduto)
        {
            if (ModelState.IsValid)
            {
                categoriaproduto.CategoriaProdutoId = Guid.NewGuid();
                db.CategoriaProdutos.Add(categoriaproduto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriaproduto);
        }

        //
        // GET: /CategoriaProduto/Editar/5
        public ActionResult Editar(Guid id)
        {
            var categoriaproduto = db.CategoriaProdutos.Single(x => x.CategoriaProdutoId == id);
            return View(categoriaproduto);
        }

        //
        // POST: /CategoriaProduto/Editar/5
        [HttpPost]
        public ActionResult Editar(CategoriaProduto categoriaproduto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriaproduto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriaproduto);
        }

        //
        // GET: /CategoriaProduto/Excluir/5
        public ActionResult Excluir(Guid id)
        {
            var categoriaproduto = db.CategoriaProdutos.Single(x => x.CategoriaProdutoId == id);
            return View(categoriaproduto);
        }

        //
        // POST: /CategoriaProduto/Excluir/5
        [HttpPost, ActionName("Excluir")]
        public ActionResult ConfirmarExclusao(Guid id)
        {
            var categoriaproduto = db.CategoriaProdutos.Single(x => x.CategoriaProdutoId == id);
            db.CategoriaProdutos.Remove(categoriaproduto);
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