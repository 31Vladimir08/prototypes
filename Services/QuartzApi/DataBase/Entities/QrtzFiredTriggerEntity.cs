using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuartzApi.DataBase.Entities;

public partial class QrtzFiredTriggerEntity
{
    public string SchedName { get; set; } = null!;

    public string EntryId { get; set; } = null!;

    public string TriggerName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;

    public string InstanceName { get; set; } = null!;

    public long FiredTime { get; set; }

    public long SchedTime { get; set; }

    public int Priority { get; set; }

    public string State { get; set; } = null!;

    public string? JobName { get; set; }

    public string? JobGroup { get; set; }

    public bool? IsNonconcurrent { get; set; }

    public bool? RequestsRecovery { get; set; }
}

public class QrtzFiredTriggerEntityMSSQLConfig : IEntityTypeConfiguration<QrtzFiredTriggerEntity>
{
    public void Configure(EntityTypeBuilder<QrtzFiredTriggerEntity> builder)
    {
        builder.HasKey(e => new { e.SchedName, e.EntryId });

        builder.ToTable("QRTZ_FIRED_TRIGGERS");

        builder.HasIndex(e => new { e.SchedName, e.JobGroup, e.JobName }, "IDX_QRTZ_FT_G_J");

        builder.HasIndex(e => new { e.SchedName, e.TriggerGroup, e.TriggerName }, "IDX_QRTZ_FT_G_T");

        builder.HasIndex(e => new { e.SchedName, e.InstanceName, e.RequestsRecovery }, "IDX_QRTZ_FT_INST_JOB_REQ_RCVRY");

        builder.Property(e => e.SchedName)
            .HasMaxLength(120)
            .HasColumnName("SCHED_NAME");
        builder.Property(e => e.EntryId)
            .HasMaxLength(140)
            .HasColumnName("ENTRY_ID");
        builder.Property(e => e.FiredTime).HasColumnName("FIRED_TIME");
        builder.Property(e => e.InstanceName)
            .HasMaxLength(200)
            .HasColumnName("INSTANCE_NAME");
        builder.Property(e => e.IsNonconcurrent).HasColumnName("IS_NONCONCURRENT");
        builder.Property(e => e.JobGroup)
            .HasMaxLength(150)
            .HasColumnName("JOB_GROUP");
        builder.Property(e => e.JobName)
            .HasMaxLength(150)
            .HasColumnName("JOB_NAME");
        builder.Property(e => e.Priority).HasColumnName("PRIORITY");
        builder.Property(e => e.RequestsRecovery).HasColumnName("REQUESTS_RECOVERY");
        builder.Property(e => e.SchedTime).HasColumnName("SCHED_TIME");
        builder.Property(e => e.State)
            .HasMaxLength(16)
            .HasColumnName("STATE");
        builder.Property(e => e.TriggerGroup)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_GROUP");
        builder.Property(e => e.TriggerName)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_NAME");
    }
}
