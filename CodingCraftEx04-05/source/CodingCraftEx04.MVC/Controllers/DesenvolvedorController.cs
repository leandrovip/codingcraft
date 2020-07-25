using System.Web.Mvc;
using CodingCraftEx04.MVC.Filters;

namespace CodingCraftEx04.MVC.Controllers
{
    [ClaimsAuthorize("Desenvolvedor","True")]
    public class DesenvolvedorController : Controller
    {
        // GET: Desenvolvedor
        public ActionResult Index()
        {
            return View();
        }
    }
}