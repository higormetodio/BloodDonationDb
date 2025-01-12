using BloodDonationDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonationDb.Infrastructure.Persistence.Configuration;

public class DonationReceiverEntityConfiguration : IEntityTypeConfiguration<DonationReceiver>
{
    public void Configure(EntityTypeBuilder<DonationReceiver> builder)
    {
        builder.ToTable("DonationReceivers");

        builder.HasKey(donation => donation.Id);

        builder.Property(donation => donation.CreateOn)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(donation => donation.ReceiverId)
            .IsRequired();

        builder.Property(donation => donation.When)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(donation => donation.Quantity)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(donation => donation.BloodStockId)
            .IsRequired();
    }
}