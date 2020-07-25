using System;
using CodingCraftEx04.Domain.Infra.Identity;
using CodingCraftEx04.Domain.Models;
using CodingCraftEx04.Domain.Models.Context;
using KatanaContrib.Security.Foursquare;
using KatanaContrib.Security.Github;
using KatanaContrib.Security.LinkedIn;
using KatanaContrib.Security.Meetup;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace CodingCraftEx04.MVC
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and role manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity =
                        SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, Usuario, Guid>(
                            TimeSpan.FromMinutes(30), (manager, user) => user.GenerateUserIdentityAsync(manager),
                            user => new Guid(user.GetUserId()))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            app.UseTwitterAuthentication("iHimqjULxtJ22WtGbGg8KlW76",
                "xBXh3CKY9EppkTnkgxudxNTICn1n4LOP1BP2VkeD7s3HKGLBwU");

            app.UseLinkedInAuthentication("78vvuvq1pg9973", "ZL61bwHTaXUAI0Ku");

            app.UseMeetupAuthentication("bleqrvbggttqdravb56pp2d1kt", "o0673upcajn1qmkg8htab0sfeb");

            app.UseFoursquareAuthentication("CZQY1EQQKZJNHSAFLFURRMF3QJWPWYBZJ4AO5DHJCY5I3JHO",
                "QM5VRYMSANBKA5XKMC55C4RSKTIGAYGTDVIBE0EAYJ45N5SI");

            app.UseGithubAuthentication("d33d8a58847b2f96503a", "02383bab0d11286d6d78d42d306d36f768c25686");

            app.UseFacebookAuthentication("250429938811989", "c1431fb628665bf55cc2baafd04416ca");

            app.UseGoogleAuthentication("712122543609-p29op2pvkbp9plld1rqvufb2biu5qm5k.apps.googleusercontent.com",
                "Ze53bLjZHjAi_uoSEagdHLNY");
        }
    }
}