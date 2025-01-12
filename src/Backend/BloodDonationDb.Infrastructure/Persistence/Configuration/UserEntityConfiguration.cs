using BloodDonationDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonationDb.Infrastructure.Persistence.Configuration;
public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.CreateOn)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(user => user.Name)
            .IsRequired()
            .HasColumnType("nvarchar(255)");

        builder.Property(user => user.Email)
            .IsRequired()
            .HasColumnType("nvarchar(255)");

        builder.Property(user => user.Password)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(user => user.Active)
            .IsRequired()
            .HasColumnType("bit");            
    }
}
