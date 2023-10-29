using FiasService.Models;

namespace FiasService.Interfaces
{
    public interface IEventConsumer
    {
        void Consume(string topic, Action<MessageBusModel?> action);
    }
}
