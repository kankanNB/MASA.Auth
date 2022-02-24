﻿namespace MASA.Auth.Service.Infrastructure.EntityConfigurations.Subjects;

public class StaffRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(nameof(UserRole), AuthDbContext.SUBJECT_SCHEMA);
        builder.HasKey(s => s.Id);
    }
}

