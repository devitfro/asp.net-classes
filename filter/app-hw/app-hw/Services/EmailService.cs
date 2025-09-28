using System.Net;
using System.Net.Mail;

namespace app_hw.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var from = _config["Email:From"];
            var password = _config["Email:Password"];

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(from, password);
                smtp.EnableSsl = true;

                var mail = new MailMessage(from, to, subject, body);
                smtp.Send(mail);
            }
        }
    }
}
