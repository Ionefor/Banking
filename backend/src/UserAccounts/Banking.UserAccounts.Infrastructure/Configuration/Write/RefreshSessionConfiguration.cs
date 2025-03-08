﻿using Banking.UserAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.UserAccounts.Infrastructure.Configuration.Write;

public class RefreshSessionConfiguration : IEntityTypeConfiguration<RefreshSession>
{
    public void Configure(EntityTypeBuilder<RefreshSession> builder)
    {
        builder.ToTable("refresh_sessions");

        builder.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId);
    }
}