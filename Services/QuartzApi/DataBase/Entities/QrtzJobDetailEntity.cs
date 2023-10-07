using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuartzApi.DataBase.Entities;

public partial class QrtzJobDetailEntity
{
    public string SchedName { get; set; } = null!;

    public string JobName { get; set; } = null!;

    public string JobGroup { get; set; } = null!;

    public string? Description { get; set; }

    public string JobClassName { get; set; } = null!;

    public bool IsDurable { get; set; }

    public bool IsNonconcurrent { get; set; }

    public bool IsUpdateData { get; set; }

    public bool RequestsRecovery { get; set; }

    public byte[]? JobData { get; set; }

    public virtual ICollection<QrtzTriggerEntity> QrtzTriggers { get; set; } = new List<QrtzTriggerEntity>();
}

public class QrtzJobDetailEntityMSSQLConfig : IEntityTypeConfiguration<QrtzJobDetailEntity>
{
    public void Configure(EntityTypeBuilder<QrtzJobDetailEntity> builder)
    {
        builder.HasKey(e => new { e.SchedName, e.JobName, e.JobGroup });

        builder.ToTable("QRTZ_JOB_DETAILS");

        builder.Property(e => e.SchedName)
            .HasMaxLength(120)
            .HasColumnName("SCHED_NAME");
        builder.Property(e => e.JobName)
            .HasMaxLength(150)
            .HasColumnName("JOB_NAME");
        builder.Property(e => e.JobGroup)
            .HasMaxLength(150)
            .HasColumnName("JOB_GROUP");
        builder.Property(e => e.Description)
            .HasMaxLength(250)
            .HasColumnName("DESCRIPTION");
        builder.Property(e => e.IsDurable).HasColumnName("IS_DURABLE");
        builder.Property(e => e.IsNonconcurrent).HasColumnName("IS_NONCONCURRENT");
        builder.Property(e => e.IsUpdateData).HasColumnName("IS_UPDATE_DATA");
        builder.Property(e => e.JobClassName)
            .HasMaxLength(250)
            .HasColumnName("JOB_CLASS_NAME");
        builder.Property(e => e.JobData).HasColumnName("JOB_DATA");
        builder.Property(e => e.RequestsRecovery).HasColumnName("REQUESTS_RECOVERY");
    }
}
