using System.Data.Common;

namespace DeliveryDatasService.Interfaces.Services
{
    public interface IContextServiceFactory
    {
        DbConnection CreateContext();
    }
}
