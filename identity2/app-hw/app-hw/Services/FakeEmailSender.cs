namespace app_hw.Services
{
    using Microsoft.Extensions.Logging;

    public class FakeEmailSender : IEmailSender
    {
        private readonly ILogger<FakeEmailSender> _logger;
        public FakeEmailSender(ILogger<FakeEmailSender> logger) => _logger = logger;

        public Task SendEmailAsync(string email, string subject, string message)
        {
     
            _logger.LogInformation("Fake email to {Email} | Subject: {Subject}\n{Message}", email, subject, message);
            Console.WriteLine($"--- FAKE EMAIL to {email} ---\nSubject: {subject}\n{message}\n---------------------");
            return Task.CompletedTask;
        }
    }
}
