using BloodDonationDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodDonationDb.Infrastructure.Persistence.Configuration;

public class DonorEntityConfiguration : IEntityTypeConfiguration<Donor>
{
    public void Configure(EntityTypeBuilder<Donor> builder)
    {
        builder.ToTable("Donors");

        builder.HasKey(donor => donor.Id);

        builder.Property(donor => donor.CreateOn)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(donor => donor.Name)
            .IsRequired()
            .HasColumnType("nvarchar(255)");

        builder.Property(donor => donor.Email)
            .IsRequired()
            .HasColumnType("nvarchar(255)");

        builder.Property(donor => donor.BirthDate)
            .IsRequired()
            .HasColumnType("smalldatetime");

        builder.Property(donor => donor.Gender)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(donor => donor.Weight)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(donor => donor.IsDonor)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(donor => donor.LastDonation)
            .HasColumnType("datetime");

        builder.Property(donor => donor.NextDonation)
            .HasColumnType("datetime");

        builder.Property(donor => donor.BloodType)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(donor => donor.RhFactor)
            .IsRequired()
            .HasColumnType("int");

        builder.OwnsOne(donor => donor.Address, address =>
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

        builder.Property(entity => entity.Active)
            .IsRequired()
            .HasColumnType("bit");

        builder.HasMany(donor => donor.Donations)
            .WithOne(donation => donation.Donor)
            .HasForeignKey(donation => donation.DonorId)
            .HasConstraintName("FK_Donor_Donation_Id")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(donor => donor.Email)
            .HasDatabaseName("IX_Donors_Email")
            .IsUnique();




    }
}