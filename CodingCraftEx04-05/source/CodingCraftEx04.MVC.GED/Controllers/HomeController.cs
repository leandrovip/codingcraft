using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace CodingCraftEx04.MVC.GED.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string email, string senha)
        {
            ViewBag.Retorno = "Usuário ou senha inválidos.";
            Session["Logado"] = "0";

            if (email.IsNullOrWhiteSpace() || senha.IsNullOrWhiteSpace())
                return View();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50399/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var data = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", email),
                    new KeyValuePair<string, string>("password", senha)
                });

                var response = await client.PostAsync("token", data);

                if (!response.IsSuccessStatusCode)
                    return View();

                var content = await response.Content.ReadAsStringAsync();
                dynamic token = JsonConvert.DeserializeObject(content);
                ViewBag.Retorno = token.error;

                Session["logado"] = "1";
                Session["token"] = token.token_type + " " + token.access_token;
                Session["usuario"] = token.userName;
            }

            return View("Index");
        }

        public ActionResult Logoff()
        {
            Session["logado"] = "0";
            Session["token"] = null;
            Session["usuario"] = null;

            return View("Index");
        }
    }
}