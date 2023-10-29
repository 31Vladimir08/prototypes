namespace DeliveryDatasService.HostedServices
{
    public class DeliveryHostedService : BackgroundService
    {
        private event Action _executeAsyncNotify;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _executeAsyncNotify += async () =>
            {
                
            };

            return Task.CompletedTask;
        }
    }
}
