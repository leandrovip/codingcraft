using System.Collections.Generic;

namespace CodingCraftEx04.Api.ViewModels
{
    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }
        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }
}