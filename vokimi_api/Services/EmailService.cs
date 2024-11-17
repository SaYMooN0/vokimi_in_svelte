using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using vokimi_api.Src;

namespace vokimi_api.Services
{
    public class EmailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public EmailService(string host, int port, string username, string password) {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
        }

        public async Task<Err> SendEmail(string to, string subject, string body, bool isHtml = false) {
            try {
                MimeMessage message = new();
                message.From.Add(new MailboxAddress("Vokimi", _username));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;

                if (isHtml) {
                    message.Body = new TextPart("html") { Text = body };
                } else {
                    message.Body = new TextPart("plain") { Text = body };
                }

                using (var client = await ConfigureSmtpClient()) {
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return Err.None;
            } catch {
                return new Err("Email could not be sent");
            }
        }

        public async Task<Err> SendConfirmationLink(string to, string confirmationLink) {
            string subject = "Please confirm your email";
            string body =
                "<p>Thank you for registering. Please click the link below to confirm your email:</p>" +
               $"<p><a href='{confirmationLink}'>Confirm Email</a></p>";

            return await SendEmail(to, subject, body, true);
        }
        private async Task<SmtpClient> ConfigureSmtpClient() {
            var client = new SmtpClient();
            await client.ConnectAsync(_host, _port, true);
            await client.AuthenticateAsync(_username, _password);
            return client;
        }

    }

}

