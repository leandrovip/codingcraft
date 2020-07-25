using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CodingCraftEx08Server;
using NuGet.Server;
using NuGet.Server.Infrastructure;
using NuGet.Server.V2;
using WebActivatorEx;
using HttpMethodConstraint = System.Web.Http.Routing.HttpMethodConstraint;

[assembly: PreApplicationStartMethod(typeof(NuGetODataConfigVip), "Start")]

namespace CodingCraftEx08Server
{
    public static class NuGetODataConfigVip
    {
        public static void Start()
        {
            ServiceResolver.SetServiceResolver(new DefaultServiceResolver());

            var config = GlobalConfiguration.Configuration;
            config.UseNuGetV2WebApiFeed("NuGetDefault", "nuget", "PackagesOData");
            config.Services.Replace(typeof(IExceptionLogger), new TraceExceptionLogger());

            RouteConfig(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void RouteConfig(RouteCollection routes)
        {
            routes.MapHttpRoute(
                "NuGetDefault_ClearCache",
                "nuget/clear-cache",
                new {controller = "PackagesOData", action = "ClearCache"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)}
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );

        }
    }
}