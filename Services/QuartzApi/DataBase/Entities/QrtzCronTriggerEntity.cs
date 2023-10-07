using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuartzApi.DataBase.Entities;

public partial class QrtzCronTriggerEntity
{
    public string SchedName { get; set; } = null!;

    public string TriggerName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;

    public string CronExpression { get; set; } = null!;

    public string? TimeZoneId { get; set; }

    public virtual QrtzTriggerEntity QrtzTrigger { get; set; } = null!;
}

public class QrtzCronTriggerEntityMSSQLConfig : IEntityTypeConfiguration<QrtzCronTriggerEntity>
{
    public void Configure(EntityTypeBuilder<QrtzCronTriggerEntity> builder)
    {
        builder.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

        builder.ToTable("QRTZ_CRON_TRIGGERS");

        builder.Property(e => e.SchedName)
            .HasMaxLength(120)
            .HasColumnName("SCHED_NAME");
        builder.Property(e => e.TriggerName)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_NAME");
        builder.Property(e => e.TriggerGroup)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_GROUP");
        builder.Property(e => e.CronExpression)
            .HasMaxLength(120)
            .HasColumnName("CRON_EXPRESSION");
        builder.Property(e => e.TimeZoneId)
            .HasMaxLength(80)
            .HasColumnName("TIME_ZONE_ID");

        builder.HasOne(d => d.QrtzTrigger).WithOne(p => p.QrtzCronTrigger)
            .HasForeignKey<QrtzCronTriggerEntity>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
            .HasConstraintName("FK_QRTZ_CRON_TRIGGERS_QRTZ_TRIGGERS");
    }
}
