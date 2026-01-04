using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PM_API.Infrastructure.ModelConfiguration;

public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        builder.ToTable("role_claim", "identity");

        builder.Property(rc => rc.Id).HasColumnName("role_claim_id");
        builder.Property(rc => rc.RoleId).HasColumnName("role_id");
        builder.Property(rc => rc.ClaimType).HasColumnName("claim_type");
        builder.Property(rc => rc.ClaimValue).HasColumnName("claim_value");
    }
}
