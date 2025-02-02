using Confluent.Kafka;

namespace QuartzService.Services;

public class KafkaService
{
    private readonly IProducer<string, string> _producer;

    public KafkaService(
        IProducer<string, string> producer)
    {
        _producer = producer;
    }

    public async Task<DeliveryResult<string, string>> ProduceAsync(string topic, string jsonData)
    {
        var result = await _producer.ProduceAsync(
            topic,
            new Message<string, string>
            {
                Value = jsonData
            });
        _producer.Flush();
        return result;
    }
}
