using Fias.Api.HostedServices;
using Fias.Api.Interfaces.Services;
using Fias.Api.Services;

using FiasService.Interfaces;
using FiasService;
using Fias.Api.Filters;

namespace Fias.Api.Extensions
{
    public static class RegisterDI
    {
        public static void RegisterInIoC(this IServiceCollection services)
        {
            services.SetServicesDJ();
            services.AddScoped<UploadCallsActionFilter>();
            services.AddScoped<IEventConsumer, EventConsumer>();
        }

        public static void SetServicesDJ(this IServiceCollection services)
        {
            services.AddSingleton<FiasUpdateDbService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IXmlService, XmlService>();
        }
    }
}
