using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QuartzApi.DataBase.Entities;

public partial class QrtzCalendarEntity
{
    public string SchedName { get; set; } = null!;

    public string CalendarName { get; set; } = null!;

    public byte[] Calendar { get; set; } = null!;
}

public class QrtzCalendarEntityMSSQLConfig : IEntityTypeConfiguration<QrtzCalendarEntity>
{
    public void Configure(EntityTypeBuilder<QrtzCalendarEntity> builder)
    {
        builder.HasKey(e => new { e.SchedName, e.CalendarName });

        builder.ToTable("QRTZ_CALENDARS");

        builder.Property(e => e.SchedName)
            .HasMaxLength(120)
            .HasColumnName("SCHED_NAME");
        builder.Property(e => e.CalendarName)
            .HasMaxLength(200)
            .HasColumnName("CALENDAR_NAME");
        builder.Property(e => e.Calendar).HasColumnName("CALENDAR");
    }
}