using PureFood.AccountManager.Shared;

namespace PureFood.EmailManager.Services
{
    public class EmailHostedService(
        ILogger<EmailHostedService> logger
    ) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("EmailHostedService is starting");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("EmailHostedService is stopping");
            return Task.CompletedTask;
        }
    }
}
