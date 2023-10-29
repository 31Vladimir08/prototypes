using System.Data.Common;

using DeliveryDatasService.Enums;
using DeliveryDatasService.Interfaces.Services;
using DeliveryDatasService.Models.Options;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

using Npgsql;

namespace DeliveryDatasService.Services
{
    public class ContextServiceFactory : IContextServiceFactory
    {
        private readonly DbSettingsOption _dbOptions;

        public ContextServiceFactory(IOptions<DbSettingsOption> dbOptions) 
        {
            _dbOptions = dbOptions.Value;
        }
        public DbConnection CreateContext()
        {
            if (_dbOptions.TypeDb == SupportedDb.PostgreSQL)
            {
                return new NpgsqlConnection(_dbOptions.ConnectionString);
            }
            else if (_dbOptions.TypeDb == SupportedDb.MSSQL)
            {
                return new SqlConnection(_dbOptions.ConnectionString);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
