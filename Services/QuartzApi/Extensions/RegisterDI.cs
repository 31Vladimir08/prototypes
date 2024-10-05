using Mapster;

using MapsterMapper;

using QuartzService.Interfaces.Services;
using QuartzService.Mappers;

namespace QuartzService.Extensions;

public static class RegisterDI
{
    public static void RegisterInIoC(this IServiceCollection services)
    {
        services.SetServicesDJ();
    }

    public static void SetServicesDJ(this IServiceCollection services)
    {
        services.AddSingleton(x =>
        {
            var config = new TypeAdapterConfig();

            new RegisterMapper().Register(config);
            return config;
        });
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddScoped<IQuartzService, Services.QuartzService>();
    }
}
