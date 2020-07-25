using System.Web.Mvc;

namespace CodingCraft.Ex02.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MudarLayout(string layout, string urlAtual)
        {
            Session["layout"] = layout;
            return Redirect(urlAtual);
        }
    }
}