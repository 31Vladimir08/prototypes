using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuartzApi.DataBase.Entities;

public partial class QrtzSimpropTriggerEntity
{
    public string SchedName { get; set; } = null!;

    public string TriggerName { get; set; } = null!;

    public string TriggerGroup { get; set; } = null!;

    public string? StrProp1 { get; set; }

    public string? StrProp2 { get; set; }

    public string? StrProp3 { get; set; }

    public int? IntProp1 { get; set; }

    public int? IntProp2 { get; set; }

    public long? LongProp1 { get; set; }

    public long? LongProp2 { get; set; }

    public decimal? DecProp1 { get; set; }

    public decimal? DecProp2 { get; set; }

    public bool? BoolProp1 { get; set; }

    public bool? BoolProp2 { get; set; }

    public string? TimeZoneId { get; set; }

    public virtual QrtzTriggerEntity QrtzTrigger { get; set; } = null!;
}

public class QrtzSimpropTriggerEntityMSSQLConfig : IEntityTypeConfiguration<QrtzSimpropTriggerEntity>
{
    public void Configure(EntityTypeBuilder<QrtzSimpropTriggerEntity> builder)
    {
        builder.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

        builder.ToTable("QRTZ_SIMPROP_TRIGGERS");

        builder.Property(e => e.SchedName)
            .HasMaxLength(120)
            .HasColumnName("SCHED_NAME");
        builder.Property(e => e.TriggerName)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_NAME");
        builder.Property(e => e.TriggerGroup)
            .HasMaxLength(150)
            .HasColumnName("TRIGGER_GROUP");
        builder.Property(e => e.BoolProp1).HasColumnName("BOOL_PROP_1");
        builder.Property(e => e.BoolProp2).HasColumnName("BOOL_PROP_2");
        builder.Property(e => e.DecProp1)
            .HasColumnType("numeric(13, 4)")
            .HasColumnName("DEC_PROP_1");
        builder.Property(e => e.DecProp2)
            .HasColumnType("numeric(13, 4)")
            .HasColumnName("DEC_PROP_2");
        builder.Property(e => e.IntProp1).HasColumnName("INT_PROP_1");
        builder.Property(e => e.IntProp2).HasColumnName("INT_PROP_2");
        builder.Property(e => e.LongProp1).HasColumnName("LONG_PROP_1");
        builder.Property(e => e.LongProp2).HasColumnName("LONG_PROP_2");
        builder.Property(e => e.StrProp1)
            .HasMaxLength(512)
            .HasColumnName("STR_PROP_1");
        builder.Property(e => e.StrProp2)
            .HasMaxLength(512)
            .HasColumnName("STR_PROP_2");
        builder.Property(e => e.StrProp3)
            .HasMaxLength(512)
            .HasColumnName("STR_PROP_3");
        builder.Property(e => e.TimeZoneId)
            .HasMaxLength(80)
            .HasColumnName("TIME_ZONE_ID");

        builder.HasOne(d => d.QrtzTrigger).WithOne(p => p.QrtzSimpropTrigger)
            .HasForeignKey<QrtzSimpropTriggerEntity>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
            .HasConstraintName("FK_QRTZ_SIMPROP_TRIGGERS_QRTZ_TRIGGERS");
    }
}
