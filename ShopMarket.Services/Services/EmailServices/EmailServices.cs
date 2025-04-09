using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using ShopMarket.Services.DTOS.EmailDto;
using ShopMarket.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ShopMarket.Services.Services.EmailServices
{
    public class EmailServices (IOptions<EmailHelper>options): IEmailServices
    {
        public  async Task SendEmail(EmailDto emailDto)
        {
            var Email = new MimeMessage();
            Email.Sender = MailboxAddress.Parse(options.Value.Email);
            Email.To.Add(MailboxAddress.Parse(emailDto.ToEmail));
            Email.Subject = emailDto.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = emailDto.Body;
            Email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(options.Value.Host, options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(options.Value.Email, options.Value.Password);
            await smtp.SendAsync(Email);
            smtp.Disconnect(true);

             
             
        }
    }
}
