namespace PureFood.CartManager.Services
{
    public class CartHostedService(
        ILogger<CartHostedService> logger
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
