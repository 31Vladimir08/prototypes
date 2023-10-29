using System.Data;

using DeliveryDatasService.Extensions;
using DeliveryDatasService.Interfaces.Services;
using DeliveryDatasService.Models;
using DeliveryDatasService.Models.Options;

namespace DeliveryDatasService.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IContextServiceFactory _factory;

        public DeliveryService(IContextServiceFactory factory) 
        {
            _factory = factory;
        }

        public async Task<DeliveryRequest> GetDatasFormTableAsync(TableInfo tableInfo)
        {
            var deliveryRequest = new DeliveryRequest(tableInfo.Schema, tableInfo.TableName);
            using (var connection = _factory.CreateContext())
            {
                await connection.OpenAsync();
                var sql = $"SELECT * FROM [{tableInfo.Schema}].[{tableInfo.TableName}]";
                var adapter = connection.CreateAdapter(sql);
                var ds = new DataSet();
                adapter.Fill(ds);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var rows = new Dictionary<string, object>();
                    foreach (var cell in tableInfo.Columns)
                    {
                        if (row.Table.Columns.Contains(cell.Key))
                        {
                            rows.Add(cell.Value, row[cell.Key]);
                        }
                    }

                    deliveryRequest.Rows.Add(rows);
                }                
            }

            return deliveryRequest;
        }
    }
}
