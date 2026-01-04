using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PM_API.Infrastructure.ModelConfiguration;

public class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<long>> builder)
    {
        builder.ToTable("user_token", "identity");

        builder.Property(ut => ut.UserId).HasColumnName("user_id");
        builder.Property(ut => ut.LoginProvider).HasColumnName("login_provider");
        builder.Property(ut => ut.Name).HasColumnName("name");
        builder.Property(ut => ut.Value).HasColumnName("value");
    }
}
