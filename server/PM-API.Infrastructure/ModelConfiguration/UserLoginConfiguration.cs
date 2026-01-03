using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PM_API.Infrastructure.ModelConfiguration;

public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
    {
        builder.ToTable("user_login", "identity");

        builder.Property(ul => ul.LoginProvider).HasColumnName("login_provider");
        builder.Property(ul => ul.ProviderKey).HasColumnName("provider_key");
        builder.Property(ul => ul.ProviderDisplayName).HasColumnName("provider_display_name");
        builder.Property(ul => ul.UserId).HasColumnName("user_id");
    }
}
