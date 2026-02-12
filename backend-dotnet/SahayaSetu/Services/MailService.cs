namespace SahayaSetu.Services
{
    public interface IMailService
    {
        Task SendNgoApprovalMailAsync(string toEmail, string ngoName);
        Task SendDisapprovalEmailAsync(string toEmail, string ngoName);
        Task SendNgoDisableEmailAsync(string toEmail, string ngoName);
    }

    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;

        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;
        }

        public Task SendNgoApprovalMailAsync(string toEmail, string ngoName)
        {
            _logger.LogInformation($"[Mock Email] Sending Approval Email to {toEmail} (NGO: {ngoName})");
            return Task.CompletedTask;
        }

        public Task SendDisapprovalEmailAsync(string toEmail, string ngoName)
        {
            _logger.LogInformation($"[Mock Email] Sending Disapproval Email to {toEmail} (NGO: {ngoName})");
            return Task.CompletedTask;
        }

        public Task SendNgoDisableEmailAsync(string toEmail, string ngoName)
        {
             _logger.LogInformation($"[Mock Email] Sending Disable Notification to {toEmail} (NGO: {ngoName})");
             return Task.CompletedTask;
        }
    }
}
