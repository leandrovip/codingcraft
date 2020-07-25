using System.Web.Mvc;
using CodingCraftEx04.MVC.Filters;

namespace CodingCraftEx04.MVC.Controllers
{
    [ClaimsAuthorize("Premium", "True")]
    public class PremiumController : Controller
    {
        // GET: Premium
        public ActionResult Index()
        {
            return View();
        }
    }
}