using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using QuartzApi.DataBase.Entities;
using QuartzApi.Enums;
using QuartzApi.Models.Options.DataBase;

namespace QuartzApi.Models;

public partial class QuartzContext : DbContext
{
    private readonly DbSettingsOption _dbOptions;

    public QuartzContext(DbContextOptions<QuartzContext> options, IOptions<DbSettingsOption> dbOptions)
        : base(options)
    {
        _dbOptions = dbOptions.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        switch (_dbOptions.TypeDb)
        {
            case SupportedDb.MSSQL:
                optionsBuilder.UseSqlServer(_dbOptions.ConnectionString);
                break;
            default:
                throw new InvalidOperationException();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        switch (_dbOptions.TypeDb)
        {
            case SupportedDb.MSSQL:
                OnMSSQLModelCreating(modelBuilder);
                break;
            default:
                throw new InvalidOperationException();
        }
    }

    private void OnMSSQLModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new QrtzBlobTriggerEntityMSSQLConfig());
        modelBuilder.ApplyConfiguration(new QrtzCalendarEntityMSSQLConfig());
        modelBuilder.ApplyConfiguration(new QrtzCronTriggerEntityMSSQLConfig());
        modelBuilder.ApplyConfiguration(new QrtzFiredTriggerEntityMSSQLConfig());
        modelBuilder.ApplyConfiguration(new QrtzJobDetailEntityMSSQLConfig());
        modelBuilder.ApplyConfiguration(new QrtzLockEntityMSSQLConfig());
        modelBuilder.ApplyConfiguration(new QrtzPausedTriggerGrpEntityMSSQLConfig());
        modelBuilder.ApplyConfiguration(new QrtzSchedulerStateEntityMSSQLConfig());
        modelBuilder.ApplyConfiguration(new QrtzSimpleTriggerEntityMSSQLConfig());
        modelBuilder.ApplyConfiguration(new QrtzSimpropTriggerEntityMSSQLConfig());
        modelBuilder.ApplyConfiguration(new QrtzTriggerEntityMSSQLConfig());
    }
}
