using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace VipControle.Domain.Infra
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var mailClient = new SmtpClient
            {
                Host = "mail.vipsolucoes.com",
                Port = 587,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("contato@vipsolucoes.com", "Vip33924084")
            };

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("contato@vipsolucoes.com");
            mailMessage.To.Add(new MailAddress(message.Destination));
            mailMessage.Subject = message.Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message.Body;
            mailClient.Send(mailMessage);

            return Task.FromResult(0);
        }
    }
}