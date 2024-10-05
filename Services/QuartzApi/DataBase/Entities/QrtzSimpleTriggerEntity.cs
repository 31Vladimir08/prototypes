using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuartzService.DataBase.Entities;

public partial class QrtzSimpleTriggerEntity
{
    public string SchedName { get; set; } = null!;

    public string TriggerName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;

    public int RepeatCount { get; set; }

    public long RepeatInterval { get; set; }

    public int TimesTriggered { get; set; }

    public virtual QrtzTriggerEntity QrtzTrigger { get; set; } = null!;
}

public class QrtzSimpleTriggerEntityMSSQLConfig : IEntityTypeConfiguration<QrtzSimpleTriggerEntity>
{
    public void Configure(EntityTypeBuilder<QrtzSimpleTriggerEntity> builder)
    {
        builder.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

        builder.ToTable("QRTZ_SIMPLE_TRIGGERS");

        builder.Property(e => e.SchedName)
            .HasMaxLength(120)
            .HasColumnName("SCHED_NAME");
        builder.Property(e => e.TriggerName)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_NAME");
        builder.Property(e => e.TriggerGroup)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_GROUP");
        builder.Property(e => e.RepeatCount).HasColumnName("REPEAT_COUNT");
        builder.Property(e => e.RepeatInterval).HasColumnName("REPEAT_INTERVAL");
        builder.Property(e => e.TimesTriggered).HasColumnName("TIMES_TRIGGERED");

        builder.HasOne(d => d.QrtzTrigger).WithOne(p => p.QrtzSimpleTrigger)
            .HasForeignKey<QrtzSimpleTriggerEntity>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
            .HasConstraintName("FK_QRTZ_SIMPLE_TRIGGERS_QRTZ_TRIGGERS");
    }
}
