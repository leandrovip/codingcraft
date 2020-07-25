using Microsoft.Owin;
using Owin;
using VipControle.MVC;

[assembly: OwinStartup(typeof(Startup))]

namespace VipControle.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}