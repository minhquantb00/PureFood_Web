using PureFood.AccountManager.Shared;

namespace PureFood.ProductManager.Services
{
    public class ProductHostedService(
        ILogger<ProductHostedService> logger
    ) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("ProductHostedService is starting");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("ProductHostedService is stopping");
            return Task.CompletedTask;
        }
    }
}
