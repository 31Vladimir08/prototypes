namespace FiasService.Models
{
    public class MessageBusModel
    {
        public string JobKey { get; init; }

        public string GroupName { get; init; }

        public TriggerModel? Trigger { get; init; }

        public string? Description { get; init; }

        public string? Data { get; init; }
    }

    public class TriggerModel
    {
        public string TriggerKey { get; init; }

        public string GroupName { get; init; }

        public string CronExpression { get; init; }

        public string? Description { get; init; }
    }
}
