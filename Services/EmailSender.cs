using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Net;
using System.Threading.Tasks;
using System;
using PortfolioPage.Services;

namespace PortfolioPage.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

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