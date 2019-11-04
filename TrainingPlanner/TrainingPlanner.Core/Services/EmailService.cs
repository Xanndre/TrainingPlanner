using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Core.Options;

namespace TrainingPlanner.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly IHostingEnvironment _env;

        public EmailService(
            IOptions<EmailOptions> emailOptions,
            IHostingEnvironment env)
        {
            _emailOptions = emailOptions.Value;
            _env = env;
        }

        public async Task<MimeMessage> SendEmail(string email, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_emailOptions.SenderName, _emailOptions.Sender));

                mimeMessage.To.Add(new MailboxAddress(email));

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        await client.ConnectAsync(_emailOptions.MailServer, _emailOptions.MailPort, true);
                    }
                    else
                    {
                        await client.ConnectAsync(_emailOptions.MailServer);
                    }

                    await client.AuthenticateAsync(_emailOptions.Sender, _emailOptions.Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }
                return mimeMessage;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
