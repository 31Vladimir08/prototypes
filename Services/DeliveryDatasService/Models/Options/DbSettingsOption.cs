using DeliveryDatasService.Enums;

namespace DeliveryDatasService.Models.Options
{
    public class DbSettingsOption
    {
        public SupportedDb TypeDb { get; init; }
        public string ConnectionString { get; init; }
    }
}
