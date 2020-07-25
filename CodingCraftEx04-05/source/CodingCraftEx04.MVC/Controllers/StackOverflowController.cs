using System.Web.Mvc;
using CodingCraftEx04.MVC.Filters;

namespace CodingCraftEx04.MVC.Controllers
{
    [ClaimsAuthorize("StackOverflow", "True")]
    public class StackOverflowController : Controller
    {
        // GET: StackOverflow
        public ActionResult Index()
        {
            return View();
        }
    }
}