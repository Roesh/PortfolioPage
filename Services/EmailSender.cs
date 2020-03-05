using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Net;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace PortfolioPage.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
            ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            if(Options.RPAGE_SMTP_SendingUserEmail == null){
                _logger.LogInformation("Environment variable RPAGE_SMTP_SendingUserEmail is null. Environment variables and secrets likely not set");
                return;
            }

            emailMessage.From.Add(new MailboxAddress (Options.RPAGE_SMTP_SendingUserName, Options.RPAGE_SMTP_SendingUserEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") {Text = message};
            try
            {
                var client = new SmtpClient();                
                await client.ConnectAsync(Options.RPAGE_SMTP_SMTPdomain, 587, false);
                await client.AuthenticateAsync(Options.RPAGE_SMTP_SendingUserEmail,Options.RPAGE_SMTP_SendingUserPassword);

                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
                
            }
            catch (Exception ex) //todo add another try to send email
            {
                var e = ex;
                throw;
            }          
            
        }
    }
}