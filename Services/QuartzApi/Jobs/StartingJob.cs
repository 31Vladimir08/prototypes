using System.Text.Json;

using Confluent.Kafka;

using Quartz;

using QuartzService.Models;
using QuartzService.Services;

namespace QuartzService.Jobs;

public class StartingJob : IJob
{
    private readonly KafkaService _kafkaService;
    private readonly ILogger<StartingJob> _logger;

    public StartingJob(
        KafkaService kafkaService,
        ILogger<StartingJob> logger)
    {
        _kafkaService = kafkaService;
        _logger = logger;
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
        _logger.LogInformation(jsonMessage);
        var key = $"schedule_{message.GroupName}_{message.JobKey}";
        var result = await _kafkaService.ProduceAsync(key, jsonMessage);
        if (result.Status == PersistenceStatus.NotPersisted)
        {
            _logger.LogError($"Can't send the message to kafka. Message: {jsonMessage}");
        }
    }
}
