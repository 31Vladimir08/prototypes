namespace DeliveryDatasService.Models
{
    public class DeliveryRequest
    {
        public DeliveryRequest(string schema, string tableName) 
        {
            Rows = new List<Dictionary<string, object>>();
            Schema = schema;
            TableName = tableName;
        }

        public string TableName { get; }

        public string Schema { get; }

        public List<Dictionary<string, object>> Rows { get; }
    }
}
