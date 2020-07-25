using System;
using CodingCraftEx04.Domain.Models;
using CodingCraftEx04.Domain.Models.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace CodingCraftEx04.Domain.Infra.Identity
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<Usuario, Guid>
    {
        public ApplicationUserManager(IUserStore<Usuario, Guid> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var dbcontext = context != null ? context.Get<ApplicationDbContext>() : new ApplicationDbContext();
            var store = new UserStore<Usuario,Grupo,Guid,UsuarioLogin,UsuarioGrupo,UsuarioIdentificacao>(dbcontext);
            var manager = new ApplicationUserManager(store);

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<Usuario, Guid>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<Usuario,Guid>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });
            manager.RegisterTwoFactorProvider("Código via e-mail", new EmailTokenProvider<Usuario,Guid>
            {
                Subject = "Código de segurança",
                BodyFormat = "Seu código de segurança é {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<Usuario, Guid>(dataProtectionProvider.Create("Coding Craft"));
            }
            return manager;
        }
    }
}