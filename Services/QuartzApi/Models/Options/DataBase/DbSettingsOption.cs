using QuartzApi.Enums;

namespace QuartzApi.Models.Options.DataBase
{
    public class DbSettingsOption
    {
        public SupportedDb TypeDb { get; init; }
        public string ConnectionString { get; init; }
    }
}
