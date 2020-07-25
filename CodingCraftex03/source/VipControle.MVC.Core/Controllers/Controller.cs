using VipControle.Domain.Data.Context;

namespace VipControle.MVC.Core.Controllers
{
    public abstract class Controller : System.Web.Mvc.Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
    }
}