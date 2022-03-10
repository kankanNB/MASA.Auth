﻿namespace Masa.Auth.Service.Infrastructure.EntityConfigurations.Sso;

public class ClientSecretEntityTypeConfiguration : IEntityTypeConfiguration<ClientSecret>
{
    public void Configure(EntityTypeBuilder<ClientSecret> builder)
    {
        builder.ToTable(nameof(ClientSecret));
        builder.Property(x => x.Value).HasMaxLength(4000).IsRequired();
        builder.Property(x => x.Type).HasMaxLength(250).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(2000);
    }
}
