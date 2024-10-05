using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuartzService.DataBase.Entities;

public partial class QrtzLockEntity
{
    public string SchedName { get; set; } = null!;

    public string LockName { get; set; } = null!;
}

public class QrtzLockEntityMSSQLConfig : IEntityTypeConfiguration<QrtzLockEntity>
{
    public void Configure(EntityTypeBuilder<QrtzLockEntity> builder)
    {
        builder.HasKey(e => new { e.SchedName, e.LockName });

        builder.ToTable("QRTZ_LOCKS");

        builder.Property(e => e.SchedName)
            .HasMaxLength(120)
            .HasColumnName("SCHED_NAME");
        builder.Property(e => e.LockName)
            .HasMaxLength(40)
            .HasColumnName("LOCK_NAME");
    }
}
