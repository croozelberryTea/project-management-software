using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM_API.Infrastructure.Model;

namespace PM_API.Infrastructure.ModelConfiguration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("ticket", "project");

        builder.HasKey(t => t.TicketId).HasName("ticket_pkey");

        builder.Property(t => t.TicketId)
            .HasColumnName("ticket_id")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Title)
            .HasColumnName("title")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasColumnName("description")
            .IsRequired();

        builder.Property(t => t.Priority)
            .HasColumnName("priority")
            .IsRequired();

        builder.Property(t => t.CreatedDateTime)
            .HasColumnName("created_date_time")
            .IsRequired();

        builder.Property(t => t.LastModifiedDateTime)
            .HasColumnName("last_modified_date_time")
            .IsRequired(false);

        builder.Property(t => t.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.HasOne(t => t.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Attachments)
            .WithOne(ta => ta.Ticket)
            .HasForeignKey(ta => ta.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Comments)
            .WithOne(tc => tc.Ticket)
            .HasForeignKey(tc => tc.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.History)
            .WithOne(th => th.Ticket)
            .HasForeignKey(th => th.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.LinkedTickets)
            .WithOne(tlt => tlt.ParentTicket)
            .HasForeignKey(tlt => tlt.ParentTicketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}