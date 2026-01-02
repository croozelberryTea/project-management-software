using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM_API.Infrastructure.Model;

namespace PM_API.Infrastructure.ModelConfiguration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("ticket");

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
    }
}