using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PM_API.Infrastructure.ModelConfiguration;

public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> builder)
    {
        builder.ToTable("user_claim", "identity");

        builder.Property(uc => uc.Id).HasColumnName("user_claim_id");
        builder.Property(uc => uc.UserId).HasColumnName("user_id");
        builder.Property(uc => uc.ClaimType).HasColumnName("claim_type");
        builder.Property(uc => uc.ClaimValue).HasColumnName("claim_value");
    }
}
