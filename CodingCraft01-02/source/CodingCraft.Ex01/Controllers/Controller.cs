using System.Web;
using CodingCraft.Ex01.Models.Context;

namespace CodingCraft.Ex01.Controllers
{
    public abstract class Controller: System.Web.Mvc.Controller
    {
         protected ApplicationDbContext db = new ApplicationDbContext();
    }
}