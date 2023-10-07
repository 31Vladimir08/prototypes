using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuartzApi.DataBase.Entities;

public partial class QrtzSchedulerStateEntity
{
    public string SchedName { get; set; } = null!;

    public string InstanceName { get; set; } = null!;

    public long LastCheckinTime { get; set; }

    public long CheckinInterval { get; set; }
}

public class QrtzSchedulerStateEntityMSSQLConfig : IEntityTypeConfiguration<QrtzSchedulerStateEntity>
{
    public void Configure(EntityTypeBuilder<QrtzSchedulerStateEntity> builder)
    {
        builder.HasKey(e => new { e.SchedName, e.InstanceName });

        builder.ToTable("QRTZ_SCHEDULER_STATE");

        builder.Property(e => e.SchedName)
            .HasMaxLength(120)
            .HasColumnName("SCHED_NAME");
        builder.Property(e => e.InstanceName)
            .HasMaxLength(200)
            .HasColumnName("INSTANCE_NAME");
        builder.Property(e => e.CheckinInterval).HasColumnName("CHECKIN_INTERVAL");
        builder.Property(e => e.LastCheckinTime).HasColumnName("LAST_CHECKIN_TIME");
    }
}
