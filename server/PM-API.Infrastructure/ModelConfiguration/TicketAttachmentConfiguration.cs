using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM_API.Infrastructure.Model;

namespace PM_API.Infrastructure.ModelConfiguration;

public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
{
    public void Configure(EntityTypeBuilder<TicketAttachment> builder)
    {
        builder.ToTable("ticket_attachment");

        builder.HasKey(ta => ta.TicketAttachmentId).HasName("ticket_attachment_pkey");

        builder.Property(ta => ta.TicketAttachmentId)
            .HasColumnName("ticket_attachment_id")
            .ValueGeneratedOnAdd();

        builder.Property(ta => ta.FileName)
            .HasColumnName("file_name")
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(ta => ta.FileContent)
            .HasColumnName("file_content")
            .IsRequired();

        builder.Property(ta => ta.ContentType)
            .HasColumnName("content_type")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ta => ta.Description)
            .HasColumnName("description")
            .HasMaxLength(500);

        builder.Property(ta => ta.CreatedDateTime)
            .HasColumnName("created_date_time")
            .IsRequired();

        builder.Property(ta => ta.TicketId)
            .HasColumnName("ticket_id")
            .IsRequired();

        builder.Property(ta => ta.UserId)
            .HasColumnName("user_id");

        builder.HasOne(ta => ta.Ticket)
            .WithMany(t => t.Attachments)
            .HasForeignKey(ta => ta.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ta => ta.User)
            .WithMany()
            .HasForeignKey(ta => ta.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
