using DeliveryDatasService.Models.Options;
using DeliveryDatasService.Models;
using System.Data;

namespace DeliveryDatasService.Interfaces.Services
{
    public interface IDeliveryService
    {
        Task<DeliveryRequest> GetDatasFormTableAsync(TableInfo tableInfo);
    }
}
