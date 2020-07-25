using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using VipControle.Domain.Infra.Identity;

namespace VipControle.MVC.Core.Extensions
{
    public static class IdentityExtensions
    {
        public static bool IsEmailConfirmation(this IIdentity user)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var currentUser = userManager.FindById(user.GetUserId());

            return currentUser?.EmailConfirmed ?? false;
        }
    }
}