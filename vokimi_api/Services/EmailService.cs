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

        private async Task<Err> SendEmailWithHtmlBody(
            string to,
            string subject,
            string body
        ) {
            try {
                MimeMessage message = new();
                message.From.Add(new MailboxAddress("Vokimi", _username));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;
                message.Body = new TextPart("html") { Text = body };

                using (var client = await ConfigureSmtpClient()) {
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return Err.None;
            } catch {
                return new Err("Email could not be sent");
            }
        }

        public async Task<Err> SendRegistrationConfirmationLink(string to, string confirmationLink) {
            string subject = "Please confirm your email";
            string body =
                "<p>Thank you for registering. Please click the link below to confirm your email:</p>" +
               $"<p><a href='{confirmationLink}'>Confirm Email</a></p>";

            return await SendEmailWithHtmlBody(to, subject, body);
        }
        public async Task<Err> SendPasswordUpdateLink(
            string to,
            string confirmationLink,
            string accountUsername
        ) {
            string subject = "Password update request";
            string body =
               $"<p>If you want to update the password on the {accountUsername} account, click the link. </p>" +
               $"<p><a href='{confirmationLink}'>Update my password</a></p>"+
               $"If you didn't make the password change request, just ignore this message.</p>";
            
            return await SendEmailWithHtmlBody(to, subject, body);
        }
        public async Task<Err> SendPasswordUpdatedMessage(
           string to
       ) {
            string subject = "Password has been updated";
            string body =
               $"<p>Password on the vokimi account registered on this email, has been updated.</p>" +
               $"<p>If it was not you please change password to the stronger one as soon as you see this message. " +
               $"Also ensure that nobody else has access to your email client</p>";

            return await SendEmailWithHtmlBody(to, subject, body);
        }
        private async Task<SmtpClient> ConfigureSmtpClient() {
            var client = new SmtpClient();
            await client.ConnectAsync(_host, _port, true);
            await client.AuthenticateAsync(_username, _password);
            return client;
        }

    }

}

