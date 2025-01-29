using BloodDonationDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonationDb.Infrastructure.Persistence.Configuration;

public class ReceiverEntityConfiguration : IEntityTypeConfiguration<Receiver>
{
    public void Configure(EntityTypeBuilder<Receiver> builder)
    {
        builder.ToTable("Receivers");

        builder.HasKey(receiver => receiver.Id);

        builder.Property(receiver => receiver.CreateOn)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(receiver => receiver.Name)
            .IsRequired()
            .HasColumnType("nvarchar(255)");

        builder.Property(receiver => receiver.Email)
            .IsRequired()
            .HasColumnType("nvarchar(255)");

        builder.Property(receiver => receiver.BloodType)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(receiver => receiver.RhFactor)
            .IsRequired()
            .HasColumnType("int");

        builder.OwnsOne(receiver => receiver.Address, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("Street")
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            address.Property(a => a.Number)
                .HasColumnName("Number")
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            address.Property(a => a.City)
                .HasColumnName("City")
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            address.Property(a => a.State)
                .HasColumnName("State")
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            address.Property(a => a.ZipCode)
                .HasColumnName("ZipCode")
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            address.Property(a => a.Country)
                .HasColumnName("Country")
                .IsRequired()
                .HasColumnType("nvarchar(255)");
        });

        builder.Property(receiver => receiver.Active)
            .IsRequired()
            .HasColumnType("bit");

        builder.HasMany(receiver => receiver.Donations)
            .WithOne(donation => donation.Receiver)
            .HasForeignKey(donation => donation.ReceiverId)
            .HasConstraintName("FK_Receiver_Donation_Id")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(donor => donor.Email)
            .HasDatabaseName("IX_Receiver_Email")
            .IsUnique();
    }
}