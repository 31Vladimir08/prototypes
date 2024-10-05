using FiasService.Models;

namespace FiasService.Interfaces
{
    public interface IEventConsumer
    {
        Task ConsumeAsync(string topic, Func<MessageBusModel?, Task> action);
    }
}
