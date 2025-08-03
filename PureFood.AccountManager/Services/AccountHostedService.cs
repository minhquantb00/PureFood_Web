using Microsoft.AspNetCore.Authentication;
using PureFood.AccountManager.Shared;

namespace PureFood.AccountManager.Services
{
    public class AccountHostedService(
        ILogger<AccountHostedService> logger,
        IAccountService accountService
    ) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("AccountHostedService is starting");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("AccountHostedService is stopping");
            return Task.CompletedTask;
        }
    }
}
