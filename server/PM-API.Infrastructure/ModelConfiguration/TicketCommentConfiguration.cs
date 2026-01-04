using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM_API.Infrastructure.Model;

namespace PM_API.Infrastructure.ModelConfiguration;

public class TicketCommentConfiguration : IEntityTypeConfiguration<TicketComment>
{
    public void Configure(EntityTypeBuilder<TicketComment> builder)
    {
        builder.ToTable("ticket_comment");

        builder.HasKey(tc => tc.TicketCommentId).HasName("ticket_comment_pkey");

        builder.Property(tc => tc.TicketCommentId)
            .HasColumnName("ticket_comment_id")
            .ValueGeneratedOnAdd();

        builder.Property(tc => tc.Comment)
            .HasColumnName("comment")
            .HasMaxLength(2000);

        builder.Property(tc => tc.CreatedDateTime)
            .HasColumnName("created_date_time")
            .IsRequired();

        builder.Property(tc => tc.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(tc => tc.TicketId)
            .HasColumnName("ticket_id")
            .IsRequired();

        builder.HasOne(tc => tc.User)
            .WithMany()
            .HasForeignKey(tc => tc.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(tc => tc.Ticket)
            .WithMany(t => t.Comments)
            .HasForeignKey(tc => tc.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
