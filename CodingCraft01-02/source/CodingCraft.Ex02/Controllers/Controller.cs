using System.Web.UI;
using CodingCraft.Ex02.Models;

namespace CodingCraft.Ex02.Controllers
{
    public abstract class Controller: System.Web.Mvc.Controller
    {
        protected CodingCraftEx02Context db = new CodingCraftEx02Context();
    }
}