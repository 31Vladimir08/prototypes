using System.Text.Json;

using Confluent.Kafka;

using Microsoft.Extensions.Options;

using Quartz;

using QuartzApi.Exceptions;
using QuartzApi.Models;

namespace QuartzApi.Jobs
{
    public class StartingJob : IJob
    {
        private readonly ProducerConfig _config;

        public StartingJob(IOptions<ProducerConfig> config)
        {
            _config = config.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var message = new MessageBusModel();
            message.JobKey = context.JobDetail.Key.Name;
            message.GroupName = context.JobDetail.Key.Group;
            message.Data = context.JobDetail.JobDataMap.GetString("data");
            message.Description = context.JobDetail.Description;

            message.Trigger = new TriggerModel();
            message.Trigger.GroupName = context.Trigger.Key.Group;
            message.Trigger.TriggerKey = context.Trigger.Key.Name;
            message.Trigger.Description = context.Trigger.Description;
            message.Trigger.CronExpression = context.Trigger is ICronTrigger cronTrigger
                    ? cronTrigger.CronExpressionString ?? string.Empty
                    : string.Empty;

            var jsonMessage = JsonSerializer.Serialize(message);

            using (var producer = new ProducerBuilder<string, string?>(_config)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8)
                .Build())
            {
                var result = await producer.ProduceAsync(message.GroupName, new Message<string, string?>
                {
                    Key = message.JobKey,
                    Value = jsonMessage
                });
                producer.Flush(TimeSpan.FromSeconds(10));
                if (result.Status == PersistenceStatus.NotPersisted)
                {
                    throw new UserException($"Could not produce topic: {message.GroupName}; message: {message.JobKey}; error: {result.Message}.");
                }
            }
        }
    }
}
