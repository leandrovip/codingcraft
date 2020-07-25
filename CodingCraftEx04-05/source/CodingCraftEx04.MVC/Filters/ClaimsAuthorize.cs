using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CodingCraftEx04.MVC.Filters
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _claimName;
        private readonly string _claimValue;

        public ClaimsAuthorizeAttribute(string claimName, string claimValue)
        {
            _claimName = claimName;
            _claimValue = claimValue;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userIdentity = (ClaimsIdentity)httpContext.User.Identity;

            var claim = userIdentity.Claims.FirstOrDefault(c => c.Type == _claimName);

            if (claim != null)
                return claim.Value == _claimValue;

            return false;
        }
    }
}
