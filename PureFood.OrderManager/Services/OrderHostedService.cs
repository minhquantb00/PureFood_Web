namespace PureFood.OrderManager.Services
{
    public class OrderHostedService(ILogger<OrderHostedService> logger) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("OrderHostedService is starting");
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("OrderHostedService is stopping");
            return Task.CompletedTask;
        }
    }
}
