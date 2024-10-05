using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuartzService.DataBase.Entities;

public partial class QrtzPausedTriggerGrpEntity
{
    public string SchedName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;
}

public class QrtzPausedTriggerGrpEntityMSSQLConfig : IEntityTypeConfiguration<QrtzPausedTriggerGrpEntity>
{
    public void Configure(EntityTypeBuilder<QrtzPausedTriggerGrpEntity> builder)
    {
        builder.HasKey(e => new { e.SchedName, e.TriggerGroup });

        builder.ToTable("QRTZ_PAUSED_TRIGGER_GRPS");

        builder.Property(e => e.SchedName)
            .HasMaxLength(120)
            .HasColumnName("SCHED_NAME");
        builder.Property(e => e.TriggerGroup)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_GROUP");
    }
}
