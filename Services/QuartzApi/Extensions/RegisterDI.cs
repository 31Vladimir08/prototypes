using QuartzApi.Interfaces.Services;
using QuartzApi.Services;

namespace QuartzApi.Extensions
{
    public static class RegisterDI
    {
        public static void RegisterInIoC(this IServiceCollection services)
        {
            services.SetServicesDJ();
        }

        public static void SetServicesDJ(this IServiceCollection services)
        {
            services.AddScoped<IQuartzService, QuartzService>();
        }
    }
}
