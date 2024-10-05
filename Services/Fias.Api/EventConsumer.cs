using System.Text.Json;

using Confluent.Kafka;

using FiasService.Interfaces;
using FiasService.Models;

using Microsoft.Extensions.Options;

namespace FiasService
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _config;
        
        public EventConsumer(IOptions<ConsumerConfig> config)
        {
            _config = config.Value;
        }

        public async Task ConsumeAsync(string topic, Func<MessageBusModel?, Task> action)
        {
            using (var consumer = new ConsumerBuilder<string, string>(_config)
                        .SetKeyDeserializer(Deserializers.Utf8)
                        .SetValueDeserializer(Deserializers.Utf8)
                        .Build())
            {
                consumer.Subscribe(topic);

                while (true)
                {
                    var consumeResult = consumer.Consume();

                    if (consumeResult is null)
                        continue;

                    var eventMessage = JsonSerializer.Deserialize<MessageBusModel>(consumeResult.Message.Value);

                    await action(eventMessage);
                    consumer.Commit(consumeResult);
                }
            }
        }
    }
}
