using System;
using System.Data.Entity.Utilities;
using System.Security.Claims;
using System.Threading.Tasks;
using CodingCraftEx04.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace CodingCraftEx04.Domain.Infra.Identity
{
    public class ApplicationSignInManager : SignInManager<Usuario, Guid>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            :
                base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(Usuario user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager) UserManager);
        }

        public override async Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            var userId = await GetVerifiedUserIdGuidAsync();
            if (userId == Guid.Empty)
                return false;

            var token = await UserManager.GenerateTwoFactorTokenAsync(userId, provider).WithCurrentCulture();
            await UserManager.NotifyTwoFactorTokenAsync(userId, provider, token).WithCurrentCulture();

            return true;
        }

        public override async Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent,
            bool rememberBrowser)
        {
            var userId = await GetVerifiedUserIdGuidAsync().WithCurrentCulture();
            if (userId == Guid.Empty)
                return SignInStatus.Failure;

            var user = await UserManager.FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
                return SignInStatus.Failure;

            if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
                return SignInStatus.LockedOut;

            if (await UserManager.VerifyTwoFactorTokenAsync(user.Id, provider, code).WithCurrentCulture())
            {
                await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
                await SignInAsync(user, isPersistent, rememberBrowser).WithCurrentCulture();
                return SignInStatus.Success;
            }

            await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
            return SignInStatus.Failure;
        }

        public async Task<Guid> GetVerifiedUserIdGuidAsync()
        {
            var result =
                await
                    AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorCookie)
                        .WithCurrentCulture();
            return result?.Identity == null ? default(Guid) : Guid.Parse(result.Identity.GetUserId());
        }

        public async Task<bool> HasHasBeenVerifiedAsync2()
        {
            return await GetVerifiedUserIdGuidAsync() != Guid.Empty;
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}