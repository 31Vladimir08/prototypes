using DeliveryDatasService.HostedServices;
using DeliveryDatasService.Interfaces.Services;
using DeliveryDatasService.Services;

namespace DeliveryDatasService.Extensions
{
    public static class RegisterDI
    {
        public static void RegisterInIoC(this IServiceCollection services)
        {
            services.SetServicesDJ();
        }

        public static void SetServicesDJ(this IServiceCollection services)
        {
            services.AddSingleton<DeliveryHostedService>();
            services.AddScoped<IContextServiceFactory, ContextServiceFactory>();
            services.AddScoped<IDeliveryService, DeliveryService>();
        }
    }
}
