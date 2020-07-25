using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace CodingCraftEx04.Domain.Infra
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            const string tokenTotalVoice = "c94378eb616a2b1e9c31b9378f749030";

            var body = new Dictionary<string, dynamic>
            {
                {"numero_destino", message.Destination},
                {"mensagem", message.Body}
            };
            var bodySms = JsonConvert.SerializeObject(body);

            var request = WebRequest.Create("https://api.totalvoice.com.br/sms");
            request.Method = "POST";
            request.Headers.Add("Access-Token", tokenTotalVoice);
            if (bodySms != null)
            {
                var bytes = Encoding.UTF8.GetBytes(bodySms);
                request.ContentLength = bytes.Length;

                var reqStream = request.GetRequestStream();
                reqStream.Write(bytes, 0, bytes.Length);
                reqStream.Close();
            }

            return Task.FromResult(0);
        }
    }
}