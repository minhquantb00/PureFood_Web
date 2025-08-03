using PureFood.Cache;
using PureFood.EventBus;

namespace PureFood.BaseApplication.HostedServices
{
    public class AppInitHostedService(ILogger<AppInitHostedService> logger, IServiceProvider serviceProvider) : IHostedService
    {
        private readonly ILogger<AppInitHostedService> _logger = logger;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("AppInitHostedService is starting.");

             using var scope =  serviceProvider.CreateScope();
            var connectionPersistence = scope.ServiceProvider.GetRequiredService<RedisConnectionPersistence>();
            if(connectionPersistence != null)
            {
                await connectionPersistence.MakeConnection();
            }

            var redisConnectionPool = scope.ServiceProvider.GetRequiredService<RedisConnectionPool>();
            if(redisConnectionPool != null)
            {
                await redisConnectionPool.Init(() => scope.ServiceProvider.GetRequiredService<ILogger<RedisConnection>>());
            }

            var rabbitMqConnectionPool = scope.ServiceProvider.GetService<RabbitMqConnectionPool>();

            rabbitMqConnectionPool?.Init(scope.ServiceProvider);

            var scopedProccessingService = scope.ServiceProvider.GetService(typeof(IEventProcessor));

            if(scopedProccessingService != null)
            {
                IEventProcessor eventProcessor = (IEventProcessor)scopedProccessingService;
                eventProcessor.Register();
                await eventProcessor.Start();
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("AppInitHostedService is stopping.");
            return Task.CompletedTask;
        }
    }
}
