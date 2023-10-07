using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuartzApi.DataBase.Entities;

public partial class QrtzBlobTriggerEntity
{
    public string SchedName { get; set; } = null!;

    public string TriggerName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;

    public byte[]? BlobData { get; set; }
}

public class QrtzBlobTriggerEntityMSSQLConfig : IEntityTypeConfiguration<QrtzBlobTriggerEntity>
{
    public void Configure(EntityTypeBuilder<QrtzBlobTriggerEntity> builder)
    {
        builder.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

        builder.ToTable("QRTZ_BLOB_TRIGGERS");

        builder.Property(e => e.SchedName)
            .HasMaxLength(120)
            .HasColumnName("SCHED_NAME");
        builder.Property(e => e.TriggerName)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_NAME");
        builder.Property(e => e.TriggerGroup)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_GROUP");
        builder.Property(e => e.BlobData).HasColumnName("BLOB_DATA");
    }
}
