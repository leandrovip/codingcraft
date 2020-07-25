using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CodingCraftEx04.MVC.GED.ViewModels;
using Newtonsoft.Json;

namespace CodingCraftEx04.MVC.GED.Controllers
{
    public class ArquivoController : Controller
    {
        public async Task<ActionResult> Index()
        {
            if (Session["token"] == null)
                return RedirectToAction("Login", "Home");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50399/");
                client.DefaultRequestHeaders.Authorization =
                    AuthenticationHeaderValue.Parse(Session["token"].ToString());

                var response = await client.GetAsync("api/arquivos/lista");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<ListaViewModel>>(content);
                    return View(result);
                }
            }

            return View();
        }

        public async Task<FileResult> Download(Guid id)
        {
            if (Session["token"] == null)
                return null;

            if (id == Guid.Empty)
                return null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50399/");
                client.DefaultRequestHeaders.Authorization =
                    AuthenticationHeaderValue.Parse(Session["token"].ToString());

                var response = await client.GetAsync("api/arquivos/download/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    var nomeDoArquivo =
                        response.Content.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                    var contentType = response.Content.Headers.ContentType.MediaType;

                    return File(content, contentType, nomeDoArquivo);
                }
            }

            return null;
        }

        public async Task<ActionResult> Detalhe(Guid id, DateTime? dataInicial, DateTime? dataFinal)
        {
            if (Session["token"] == null)
                return RedirectToAction("Login", "Home");

            if (id == Guid.Empty)
                return View("Error");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50399/");
                client.DefaultRequestHeaders.Authorization =
                    AuthenticationHeaderValue.Parse(Session["token"].ToString());

                var idParameter = "arquivoId=" + id;
                var dataInicioParameter = "dataInicial=";
                var dataFimParameter = "dataFinal=";

                var response = await client.GetAsync("api/arquivos/listadetalhe?" + idParameter + "&" +
                                                     dataInicioParameter + "&" + dataFimParameter);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<ListaDetalheViewModel>>(content);

                    return View(result);
                }
            }

            return View();
        }

        public ActionResult Upload()
        {
            if (Session["token"] == null)
                return RedirectToAction("Login", "Home");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadArquivo(IEnumerable<HttpPostedFileBase> files )
        {
            if (Session["token"] == null)
                return RedirectToAction("Login", "Home");

            if (files == null)
                return RedirectToAction("Upload", "Arquivo");

            var arquivosCarregados = 0;

            foreach (var file in files)
            {
                if (file == null)
                    continue;

                if (file.ContentLength == 0)
                    continue;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50399/");
                    client.DefaultRequestHeaders.Authorization =
                        AuthenticationHeaderValue.Parse(Session["token"].ToString());

                    var data = new MultipartFormDataContent();

                    var fileContent = new StreamContent(file.InputStream);
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = file.FileName,
                        Size = file.ContentLength
                    };

                    data.Add(fileContent, file.FileName);

                    var response = await client.PostAsync("api/arquivos/upload", data);
                    if (response.IsSuccessStatusCode)
                        arquivosCarregados++;
                }
            }
            
            return arquivosCarregados > 0 ? new JsonResult {Data = "Boa! Total de arquivos carregados: " + arquivosCarregados} :
                new JsonResult { Data = "Nenhum arquivo carregado"}; 
        }

        public async Task<ActionResult> Excluir(Guid id)
        {
            if (Session["token"] == null)
                return null;

            if (id == Guid.Empty)
                return null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50399/");
                client.DefaultRequestHeaders.Authorization =
                    AuthenticationHeaderValue.Parse(Session["token"].ToString());

                var response = await client.GetAsync("api/arquivos/excluir/" + id);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}