using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodingCraft.Ex02.Startup))]
namespace CodingCraft.Ex02
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
