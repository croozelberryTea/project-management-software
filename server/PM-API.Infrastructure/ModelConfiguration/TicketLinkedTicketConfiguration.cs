using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM_API.Infrastructure.Model;

namespace PM_API.Infrastructure.ModelConfiguration;

public class TicketLinkedTicketConfiguration : IEntityTypeConfiguration<TicketLinkedTicket>
{
    public void Configure(EntityTypeBuilder<TicketLinkedTicket> builder)
    {
        builder.ToTable("ticket_linked_ticket", "project");

        builder.HasKey(tlt => tlt.TicketLinkedTicketId).HasName("ticket_linked_ticket_pkey");

        builder.Property(tlt => tlt.TicketLinkedTicketId)
            .HasColumnName("ticket_linked_ticket_id")
            .ValueGeneratedOnAdd();

        builder.Property(tlt => tlt.Relation)
            .HasColumnName("relation")
            .IsRequired()
            .HasConversion<int>();

        builder.Property(tlt => tlt.ParentTicketId)
            .HasColumnName("parent_ticket_id")
            .IsRequired();

        builder.Property(tlt => tlt.ChildTicketId)
            .HasColumnName("child_ticket_id")
            .IsRequired();

        builder.HasOne(tlt => tlt.ParentTicket)
            .WithMany(t => t.LinkedTickets)
            .HasForeignKey(tlt => tlt.ParentTicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tlt => tlt.ChildTicket)
            .WithMany()
            .HasForeignKey(tlt => tlt.ChildTicketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
