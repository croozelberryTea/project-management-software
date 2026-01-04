using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM_API.Infrastructure.Model;

namespace PM_API.Infrastructure.ModelConfiguration;

public class TicketHistoryConfiguration : IEntityTypeConfiguration<TicketHistory>
{
    public void Configure(EntityTypeBuilder<TicketHistory> builder)
    {
        builder.ToTable("ticket_history");

        builder.HasKey(th => th.TicketHistoryId).HasName("ticket_history_pkey");

        builder.Property(th => th.TicketHistoryId)
            .HasColumnName("ticket_history_id")
            .ValueGeneratedOnAdd();

        builder.Property(th => th.Action)
            .HasColumnName("action")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(th => th.Details)
            .HasColumnName("details")
            .HasColumnType("jsonb");

        builder.Property(th => th.CreatedDateTime)
            .HasColumnName("created_date_time")
            .IsRequired();

        builder.Property(th => th.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(th => th.TicketId)
            .HasColumnName("ticket_id")
            .IsRequired();

        builder.HasOne(th => th.User)
            .WithMany()
            .HasForeignKey(th => th.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(th => th.Ticket)
            .WithMany(t => t.History)
            .HasForeignKey(th => th.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
