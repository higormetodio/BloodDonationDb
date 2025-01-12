using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonationDb.Infrastructure.Persistence.Configuration;

public class BloodEntityConfiguration : IEntityTypeConfiguration<BloodStock>
{
    public void Configure(EntityTypeBuilder<BloodStock> builder)
    {
        builder.ToTable("BloodsStock");
        
        builder.HasKey(blood => blood.Id);
        
        builder.Property(blood => blood.BloodType)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(blood => blood.RhFactor)
            .IsRequired()
            .HasColumnType("int");
        
        builder.Property(blood => blood.Quantity)
            .IsRequired()
            .HasColumnType("int");

        builder.HasMany(blood => blood.DonationDonors)
            .WithOne(donation => donation.BloodStock)
            .HasForeignKey(donation => donation.BloodStockId)
            .HasConstraintName("FK_BloodStock_DonationDonor")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(blood => blood.DonationReceivers)
            .WithOne(donation => donation.BloodStock)
            .HasForeignKey(donation => donation.BloodStockId)
            .HasConstraintName("FK_BloodStock_DonationReceiver")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
            new BloodStock(BloodType.A, RhFactor.Positive),
            new BloodStock(BloodType.A, RhFactor.Negative),
            new BloodStock(BloodType.B, RhFactor.Positive),
            new BloodStock(BloodType.B, RhFactor.Negative),
            new BloodStock(BloodType.AB, RhFactor.Positive),
            new BloodStock(BloodType.AB, RhFactor.Negative),
            new BloodStock(BloodType.O, RhFactor.Positive),
            new BloodStock(BloodType.O, RhFactor.Negative)
        );
    }
}