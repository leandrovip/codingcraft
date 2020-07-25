using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodingCraft.Ex01.Startup))]
namespace CodingCraft.Ex01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
