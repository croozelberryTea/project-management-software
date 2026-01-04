using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PM_API.Infrastructure.ModelConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<long>> builder)
    {
        builder.ToTable("role", "identity");

        builder.Property(r => r.Id).HasColumnName("role_id");
        builder.Property(r => r.Name).HasColumnName("name");
        builder.Property(r => r.NormalizedName).HasColumnName("normalized_name");
        builder.Property(r => r.ConcurrencyStamp).HasColumnName("concurrency_stamp");
    }
}
