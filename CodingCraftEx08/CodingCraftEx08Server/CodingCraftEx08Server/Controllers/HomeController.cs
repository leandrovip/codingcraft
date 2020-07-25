using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CodingCraftEx08Server.Models;
using Microsoft.Ajax.Utilities;
using NuGet.Server;
using RestSharp;

namespace CodingCraftEx08Server.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar()
        {
            var client = new RestClient(Helpers.GetPushUrl(Request.Url, Request.ApplicationPath));
            var request = new RestRequest("/Packages", Method.GET, DataFormat.Xml) {RootElement = "properties"};
            var response = client.Execute<List<Entry>>(request);

            return View(response.Data);
        }

        public ActionResult LimparCache()
        {
            var client = new RestClient(Helpers.GetPushUrl(Request.Url, Request.ApplicationPath));
            var request = new RestRequest("/clear-cache", Method.GET);
            var response = client.Execute(request);

            return View(response.StatusCode);
        }

        [HttpPost]
        public async Task<ActionResult> Enviar(HttpPostedFileBase file)
        {
            if (file == null)
                return View("Index");

            if (file.ContentLength == 0)
                return View("Index");

            if (!file.FileName.Contains(".nupkg"))
                return View("Index");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Helpers.GetPushUrl(Request.Url, Request.ApplicationPath));

                var data = new MultipartFormDataContent();
                var fileContent = new StreamContent(file.InputStream);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = file.FileName,
                    Size = file.ContentLength
                };

                data.Add(fileContent, file.FileName);

                var response = await client.PutAsync("", data);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Listar");
            }

            return View("Index");
        }

        public ActionResult Excluir(string nomePacote, string versao)
        {
            if (nomePacote.IsNullOrWhiteSpace() || versao.IsNullOrWhiteSpace())
                return View("Index");

            var client = new RestClient(Helpers.GetPushUrl(Request.Url, Request.ApplicationPath));
            var request = new RestRequest($"/{nomePacote}/{versao}", Method.DELETE);
            var response = client.Execute(request);

            if (response.IsSuccessful)
                return RedirectToAction("Listar");

            return View("Index");
        }
    }
}