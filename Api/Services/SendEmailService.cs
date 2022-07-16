using Application.Interfaces;
using Application.Models;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services
{
    public class SendEmailService : ISendMailService
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.From = new MailAddress("autoreply.academicblog@gmail.com");
            mailMessage.Body = htmlMessage;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new NetworkCredential("autoreply.academicblog@gmail.com", "liwxlzbdplnftrgu");
            smtp.Send(mailMessage);
        }

        public async Task SendMail(MailContent mailContent)
        {
            await SendEmailAsync(mailContent.To, mailContent.Subject, mailContent.Body);
        }
    }
}
