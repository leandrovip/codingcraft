using System.Web.Mvc;

namespace VipControle.MVC.Core.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}