namespace DeliveryDatasService.Models.Options
{
    public class TablesOption
    {
        public List<TableInfo> Tables { get; set; }
    }

    public class TableInfo
    {
        public string Schema { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, string> Columns { get; set; }
    }
}

