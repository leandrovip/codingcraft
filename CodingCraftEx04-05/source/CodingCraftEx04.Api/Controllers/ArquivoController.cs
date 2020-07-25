using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CodingCraftEx04.Api.Dictionary;
using CodingCraftEx04.Api.Providers;
using CodingCraftEx04.Api.ViewModels;
using CodingCraftEx04.Domain.Models;
using CodingCraftEx04.Domain.Models.Context;
using CodingCraftEx04.Domain.Models.Enum;
using Microsoft.AspNet.Identity;

namespace CodingCraftEx04.Api.Controllers
{

    [RoutePrefix("api/arquivos")]
    public class ArquivoController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        [Route("upload")]
        public async Task<HttpResponseMessage> Upload()
        {
            // Verifica se o tipo é multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var caminhoPasta = HttpContext.Current.Server.MapPath("~/Uploads");
            var streamProvider = new CustomMultiPartFormDataStreamProvider(caminhoPasta);

            try
            {
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                foreach (var file in streamProvider.FileData)
                {
                    var arquivo = new Arquivo();
                    arquivo.Nome = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                    arquivo.ArquivoId = Guid.NewGuid();
                    arquivo.DataUpload = DateTime.Now;
                    arquivo.Tamanho = file.Headers.ContentDisposition.Size.ToString();
                    arquivo.UsuarioId = new Guid(HttpContext.Current.User.Identity.GetUserId());
                    arquivo.Usuario = HttpContext.Current.User.Identity.GetUserName();

                    arquivo.Caminho = Path.Combine(caminhoPasta,
                        TipoDeArquivo.BuscarNome(file.LocalFileName.Split('.').LastOrDefault()),
                        arquivo.ArquivoId.ToString());

                    arquivo.TipoDeArquivo = TipoDeArquivo.BuscarNome(file.LocalFileName
                        .Split('.').LastOrDefault());

                    // Move arquivo para pasta correta
                    File.Move(file.LocalFileName, arquivo.Caminho);

                    db.Arquivos.Add(arquivo);
                }

                await db.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.OK, "Upload com sucesso!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("download/{arquivoId}")]
        public async Task<HttpResponseMessage> Download(string arquivoId)
        {
            if (arquivoId == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Falha na Requisição");

            // Busca o Arquivo
            var arquivo = db.Arquivos.FirstOrDefault(a => a.ArquivoId == new Guid(arquivoId));
            if (arquivo == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Arquivo não encontrado");
            try
            {
                if (File.Exists(arquivo.Caminho))
                {
                    var arquivoDetalhe = new ArquivoDetalhe
                    {
                        ArquivoId = arquivo.ArquivoId,
                        ArquivoDetalheId = Guid.NewGuid(),
                        DataDownload = DateTime.Now,
                        Usuario = HttpContext.Current.User.Identity.GetUserName(),
                        UsuarioId = new Guid(HttpContext.Current.User.Identity.GetUserId())
                    };

                    db.ArquivosDetalhes.Add(arquivoDetalhe);
                    await db.SaveChangesAsync();

                    var result = new HttpResponseMessage(HttpStatusCode.OK);
                    var stream = new FileStream(arquivo.Caminho, FileMode.Open, FileAccess.Read);
                    result.Content = new StreamContent(stream);
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = arquivo.Nome;
                    return result;
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Arquivo não encontrado");
        }

        [Authorize]
        [HttpGet]
        [Route("excluir/{arquivoId}")]
        public async Task<HttpResponseMessage> Excluir(string arquivoId)
        {
            if (!Guid.TryParse(arquivoId, out _))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id do Arquivo é inválida");

            try
            {
                var arquivo = await db.Arquivos.FindAsync(new Guid(arquivoId));

                if (arquivo == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, arquivoId);

                File.Delete(arquivo.Caminho);

                db.Arquivos.Remove(arquivo);
                await db.SaveChangesAsync();

                var retorno = new ExcluirViewModel()
                {
                    ArquivoId = arquivo.ArquivoId,
                    DataUpload = arquivo.DataUpload,
                    Nome = arquivo.Nome,
                    Usuario = arquivo.Usuario,
                    TipoArquivo = arquivo.TipoDeArquivo
                };

                return Request.CreateResponse(HttpStatusCode.OK, retorno);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("lista")]
        public HttpResponseMessage Lista()
        {
            try
            {
                //Verificar se esta instrução está correta ou se existe uma forma melhor/performatica.
                var listagem = db.Arquivos
                    .Select(p => new { p.ArquivoId, p.Nome, p.DataUpload, p.TipoDeArquivo })
                    .GroupBy(x => x.TipoDeArquivo)
                    .Select(g => new
                    {
                        TipoArquivo = g.Key,
                        Arquivos = g.Select(p => new { p.ArquivoId, p.Nome, p.DataUpload }).ToList()
                    })
                    .ToList();

                var lista = new List<ListaViewModel>();

                foreach (var linha in listagem)
                {
                    var arquivos = new ListaViewModel();
                    arquivos.TipoArquivo = linha.TipoArquivo;

                    var listaArquivos = linha.Arquivos.Select(t => new ArquivoViewModel
                    {
                        ArquivoId = t.ArquivoId,
                        DataUpload = t.DataUpload,
                        Nome = t.Nome
                    });

                    arquivos.Arquivos = new List<ArquivoViewModel>(listaArquivos);
                    lista.Add(arquivos);
                }

                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("listadetalhe")]
        public HttpResponseMessage ListaDetalhesPorData(string arquivoId, string dataInicial, string dataFinal)
        {
            if (!Guid.TryParse(arquivoId, out _))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id do Arquivo não é válida");

            // Verifica Datas
            if (!DateTime.TryParse(dataInicial, out var dataInicio))
                dataInicio = new DateTime(2017, 01, 01);

            if (!DateTime.TryParse(dataFinal, out var dataFim))
                dataFim = new DateTime(2018, 12, 01);

            try
            {
                var result = db.ArquivosDetalhes
                    .Select(a => new { a.ArquivoDetalheId, a.ArquivoId, a.DataDownload, a.Usuario })
                    .Where(a => a.ArquivoId == new Guid(arquivoId) &&
                                a.DataDownload >= dataInicio && a.DataDownload <= dataFim)
                    .ToList();


                var lista = result.Select(detalhe => new ListaDetalheViewModel()
                {
                    ArquivoId = detalhe.ArquivoId,
                    ArquivoDetalheId = detalhe.ArquivoDetalheId,
                    DataDownload = detalhe.DataDownload,
                    Usuario = detalhe.Usuario
                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpGet]
        [Route("audio/{arquivoId}")]
        public async Task<HttpResponseMessage> Audio(string arquivoId)
        {
            if (!Guid.TryParse(arquivoId, out _))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id do Arquivo inválida.");

            try
            {
                var arquivo = await db.Arquivos.FindAsync(new Guid(arquivoId));

                if (arquivo == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, arquivoId);

                if (arquivo.TipoDeArquivo != TipoDeArquivoEnum.Musica.ToString())
                    return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Arquivo não suportado.");

                var audio = new AudioStream(arquivo.Caminho);

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)audio.WriteToStream,
                        new MediaTypeHeaderValue("audio/" + arquivo.Nome.Split('.').LastOrDefault()));

                return response;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("video/{arquivoId}")]
        public async Task<HttpResponseMessage> Video(string arquivoId)
        {
            if (!Guid.TryParse(arquivoId, out _))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id do Arquivo inválida.");

            try
            {
                var arquivo = await db.Arquivos.FindAsync(new Guid(arquivoId));

                if (arquivo == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, arquivoId);

                if (arquivo.TipoDeArquivo != TipoDeArquivoEnum.Video.ToString())
                    return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Arquivo não suportado.");

                var video = new VideoStream(arquivo.Caminho);

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)video.WriteToStream,
                    new MediaTypeHeaderValue("video/" + arquivo.Nome.Split('.').LastOrDefault()));

                return response;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}