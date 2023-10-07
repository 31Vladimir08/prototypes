using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuartzApi.DataBase.Entities;

public partial class QrtzTriggerEntity
{
    public string SchedName { get; set; } = null!;

    public string TriggerName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;

    public string JobName { get; set; } = null!;

    public string JobGroup { get; set; } = null!;

    public string? Description { get; set; }

    public long? NextFireTime { get; set; }

    public long? PrevFireTime { get; set; }

    public int? Priority { get; set; }

    public string TriggerState { get; set; } = null!;

    public string TriggerType { get; set; } = null!;

    public long StartTime { get; set; }

    public long? EndTime { get; set; }

    public string? CalendarName { get; set; }

    public int? MisfireInstr { get; set; }

    public byte[]? JobData { get; set; }

    public virtual QrtzCronTriggerEntity? QrtzCronTrigger { get; set; }

    public virtual QrtzJobDetailEntity QrtzJobDetail { get; set; } = null!;

    public virtual QrtzSimpleTriggerEntity? QrtzSimpleTrigger { get; set; }

    public virtual QrtzSimpropTriggerEntity? QrtzSimpropTrigger { get; set; }
}

public class QrtzTriggerEntityMSSQLConfig : IEntityTypeConfiguration<QrtzTriggerEntity>
{
    public void Configure(EntityTypeBuilder<QrtzTriggerEntity> builder)
    {
        builder.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

        builder.ToTable("QRTZ_TRIGGERS");

        builder.HasIndex(e => new { e.SchedName, e.CalendarName }, "IDX_QRTZ_T_C");

        builder.HasIndex(e => new { e.SchedName, e.JobGroup, e.JobName }, "IDX_QRTZ_T_G_J");

        builder.HasIndex(e => new { e.SchedName, e.NextFireTime }, "IDX_QRTZ_T_NEXT_FIRE_TIME");

        builder.HasIndex(e => new { e.SchedName, e.TriggerState, e.NextFireTime }, "IDX_QRTZ_T_NFT_ST");

        builder.HasIndex(e => new { e.SchedName, e.MisfireInstr, e.NextFireTime, e.TriggerState }, "IDX_QRTZ_T_NFT_ST_MISFIRE");

        builder.HasIndex(e => new { e.SchedName, e.MisfireInstr, e.NextFireTime, e.TriggerGroup, e.TriggerState }, "IDX_QRTZ_T_NFT_ST_MISFIRE_GRP");

        builder.HasIndex(e => new { e.SchedName, e.TriggerGroup, e.TriggerState }, "IDX_QRTZ_T_N_G_STATE");

        builder.HasIndex(e => new { e.SchedName, e.TriggerName, e.TriggerGroup, e.TriggerState }, "IDX_QRTZ_T_N_STATE");

        builder.HasIndex(e => new { e.SchedName, e.TriggerState }, "IDX_QRTZ_T_STATE");

        builder.Property(e => e.SchedName)
            .HasMaxLength(120)
            .HasColumnName("SCHED_NAME");
        builder.Property(e => e.TriggerName)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_NAME");
        builder.Property(e => e.TriggerGroup)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_GROUP");
        builder.Property(e => e.CalendarName)
            .HasMaxLength(200)
            .HasColumnName("CALENDAR_NAME");
        builder.Property(e => e.Description)
            .HasMaxLength(250)
            .HasColumnName("DESCRIPTION");
        builder.Property(e => e.EndTime).HasColumnName("END_TIME");
        builder.Property(e => e.JobData).HasColumnName("JOB_DATA");
        builder.Property(e => e.JobGroup)
            .HasMaxLength(150)
            .HasColumnName("JOB_GROUP");
        builder.Property(e => e.JobName)
            .HasMaxLength(150)
            .HasColumnName("JOB_NAME");
        builder.Property(e => e.MisfireInstr).HasColumnName("MISFIRE_INSTR");
        builder.Property(e => e.NextFireTime).HasColumnName("NEXT_FIRE_TIME");
        builder.Property(e => e.PrevFireTime).HasColumnName("PREV_FIRE_TIME");
        builder.Property(e => e.Priority).HasColumnName("PRIORITY");
        builder.Property(e => e.StartTime).HasColumnName("START_TIME");
        builder.Property(e => e.TriggerState)
            .HasMaxLength(16)
            .HasColumnName("TRIGGER_STATE");
        builder.Property(e => e.TriggerType)
            .HasMaxLength(8)
            .HasColumnName("TRIGGER_TYPE");

        builder.HasOne(d => d.QrtzJobDetail).WithMany(p => p.QrtzTriggers)
            .HasForeignKey(d => new { d.SchedName, d.JobName, d.JobGroup })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_QRTZ_TRIGGERS_QRTZ_JOB_DETAILS");
    }
}
