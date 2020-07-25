using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace CodingCraftEx04.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional});

            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Add(new XmlMediaTypeFormatter());
            config.Formatters.Add(new FormUrlEncodedMediaTypeFormatter());

            // Cria diretorios se não existir
            var diretorios = new[] {"Imagem", "Video", "Word", "Excel", "PowerPoint", "Musica", "Texto", "Outros"};
            var caminhoServidor = HttpContext.Current.Server.MapPath("~/Uploads/");

            foreach (var diretorio in diretorios)
                if (!Directory.Exists(caminhoServidor + diretorio))
                    Directory.CreateDirectory(caminhoServidor + diretorio);
        }
    }
}