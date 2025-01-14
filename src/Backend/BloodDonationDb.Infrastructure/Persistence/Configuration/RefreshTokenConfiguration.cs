using BloodDonationDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonationDb.Infrastructure.Persistence.Configuration;
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(refresh => refresh.Id);

        builder.Property(refresh => refresh.Value)
            .IsRequired();

        builder.Property(refresh => refresh.UserId)
            .IsRequired();

        builder.HasOne(refresh => refresh.User)
            .WithOne()
            .HasForeignKey<RefreshToken>(refresh => refresh.UserId)
            .HasConstraintName("FK_User_RefreshToken")
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}
