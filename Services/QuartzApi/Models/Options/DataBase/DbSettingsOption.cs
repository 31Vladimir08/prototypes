using QuartzService.Enums;

namespace QuartzService.Models.Options.DataBase;

public class DbSettingsOption
{
    public SupportedDb TypeDb { get; init; }
    public string ConnectionString { get; init; }
}
