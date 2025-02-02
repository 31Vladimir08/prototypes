using Confluent.Kafka;

using Mapster;

using MapsterMapper;

using QuartzService.Interfaces.Services;
using QuartzService.Mappers;
using QuartzService.Services;

namespace QuartzService.Extensions;

public static class RegisterDI
{
    public static void RegisterInIoC(this IServiceCollection services, IConfiguration config)
    {
        SetServicesDJ(services);
        AddKafka(services, config);
    }

    private static void SetServicesDJ(IServiceCollection services)
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

    private static void AddKafka(IServiceCollection services, IConfiguration config)
    {
        var producerConfig = new ProducerConfig()
        {
            BootstrapServers = config.GetValue<string>("KafkaBus:BootstrapServers"),
            SaslUsername = config.GetValue<string>("KafkaBus:SaslUsername"),
            SaslPassword = config.GetValue<string>("KafkaBus:SaslPassword"),
            SecurityProtocol = SecurityProtocol.SaslPlaintext,
            SaslMechanism = SaslMechanism.Plain
        };

        services.AddSingleton(new ProducerBuilder<string, string>(producerConfig).Build());
        services.AddSingleton<KafkaService>();
    }
}
