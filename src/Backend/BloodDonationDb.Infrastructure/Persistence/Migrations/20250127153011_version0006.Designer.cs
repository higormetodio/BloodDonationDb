﻿// <auto-generated />
using System;
using BloodDonationDb.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BloodDonationDb.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(BloodDonationDbContext))]
    [Migration("20250127153011_version0006")]
    partial class version0006
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.BloodStock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BloodType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("MinimumQuantityReached")
                        .HasColumnType("bit");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RhFactor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BloodsStock", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("e180fcf7-2de0-4082-9cd0-db19191fc1f3"),
                            BloodType = 1,
                            CreateOn = new DateTime(2025, 1, 27, 15, 30, 10, 573, DateTimeKind.Utc).AddTicks(385),
                            MinimumQuantityReached = true,
                            Quantity = 0,
                            RhFactor = 1
                        },
                        new
                        {
                            Id = new Guid("b60716e4-c526-483c-90d5-53434674c101"),
                            BloodType = 1,
                            CreateOn = new DateTime(2025, 1, 27, 15, 30, 10, 573, DateTimeKind.Utc).AddTicks(1913),
                            MinimumQuantityReached = true,
                            Quantity = 0,
                            RhFactor = 2
                        },
                        new
                        {
                            Id = new Guid("075c4252-0b30-4fac-af86-7c2374ab4980"),
                            BloodType = 2,
                            CreateOn = new DateTime(2025, 1, 27, 15, 30, 10, 573, DateTimeKind.Utc).AddTicks(1918),
                            MinimumQuantityReached = true,
                            Quantity = 0,
                            RhFactor = 1
                        },
                        new
                        {
                            Id = new Guid("ff98a980-2548-4fb8-bd39-4eeb2b56424d"),
                            BloodType = 2,
                            CreateOn = new DateTime(2025, 1, 27, 15, 30, 10, 573, DateTimeKind.Utc).AddTicks(1919),
                            MinimumQuantityReached = true,
                            Quantity = 0,
                            RhFactor = 2
                        },
                        new
                        {
                            Id = new Guid("47da8d25-9202-488e-bf9d-f20fe94c7dcb"),
                            BloodType = 4,
                            CreateOn = new DateTime(2025, 1, 27, 15, 30, 10, 573, DateTimeKind.Utc).AddTicks(1920),
                            MinimumQuantityReached = true,
                            Quantity = 0,
                            RhFactor = 1
                        },
                        new
                        {
                            Id = new Guid("01341491-3750-452d-bb92-fa200af90308"),
                            BloodType = 4,
                            CreateOn = new DateTime(2025, 1, 27, 15, 30, 10, 573, DateTimeKind.Utc).AddTicks(1921),
                            MinimumQuantityReached = true,
                            Quantity = 0,
                            RhFactor = 2
                        },
                        new
                        {
                            Id = new Guid("25963d57-21c7-4fe4-929a-4c1fc6e932e1"),
                            BloodType = 3,
                            CreateOn = new DateTime(2025, 1, 27, 15, 30, 10, 573, DateTimeKind.Utc).AddTicks(1922),
                            MinimumQuantityReached = true,
                            Quantity = 0,
                            RhFactor = 1
                        },
                        new
                        {
                            Id = new Guid("6a799968-9e15-4ebe-8163-426ecc1581e0"),
                            BloodType = 3,
                            CreateOn = new DateTime(2025, 1, 27, 15, 30, 10, 573, DateTimeKind.Utc).AddTicks(1923),
                            MinimumQuantityReached = true,
                            Quantity = 0,
                            RhFactor = 2
                        });
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.DonationDonor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BloodStockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime");

                    b.Property<Guid>("DonorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("When")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("BloodStockId");

                    b.HasIndex("DonorId");

                    b.ToTable("DonationDonors", (string)null);
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.DonationReceiver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BloodStockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("When")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("BloodStockId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("DonationReceivers", (string)null);
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.Donor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("smalldatetime");

                    b.Property<int>("BloodType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDonor")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastDonation")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("NextDonation")
                        .HasColumnType("datetime");

                    b.Property<int>("RhFactor")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("IX_Donors_Email");

                    b.ToTable("Donors", (string)null);
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.Receiver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("BloodType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RhFactor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("IX_Receiver_Email");

                    b.ToTable("Receivers", (string)null);
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens", (string)null);
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.DonationDonor", b =>
                {
                    b.HasOne("BloodDonationDb.Domain.Entities.BloodStock", "BloodStock")
                        .WithMany("DonationDonors")
                        .HasForeignKey("BloodStockId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_BloodStock_DonationDonor");

                    b.HasOne("BloodDonationDb.Domain.Entities.Donor", "Donor")
                        .WithMany("Donations")
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Donor_Donation_Id");

                    b.Navigation("BloodStock");

                    b.Navigation("Donor");
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.DonationReceiver", b =>
                {
                    b.HasOne("BloodDonationDb.Domain.Entities.BloodStock", "BloodStock")
                        .WithMany("DonationReceivers")
                        .HasForeignKey("BloodStockId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_BloodStock_DonationReceiver");

                    b.HasOne("BloodDonationDb.Domain.Entities.Receiver", "Receiver")
                        .WithMany("Donations")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Receiver_Donation_Id");

                    b.Navigation("BloodStock");

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.Donor", b =>
                {
                    b.OwnsOne("BloodDonationDb.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("DonorId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("Country");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("Number");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("State");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("Street");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("ZipCode");

                            b1.HasKey("DonorId");

                            b1.ToTable("Donors");

                            b1.WithOwner()
                                .HasForeignKey("DonorId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.Receiver", b =>
                {
                    b.OwnsOne("BloodDonationDb.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("ReceiverId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("Country");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("Number");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("State");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("Street");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(255)")
                                .HasColumnName("ZipCode");

                            b1.HasKey("ReceiverId");

                            b1.ToTable("Receivers");

                            b1.WithOwner()
                                .HasForeignKey("ReceiverId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("BloodDonationDb.Domain.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("BloodDonationDb.Domain.Entities.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_RefreshToken");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.BloodStock", b =>
                {
                    b.Navigation("DonationDonors");

                    b.Navigation("DonationReceivers");
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.Donor", b =>
                {
                    b.Navigation("Donations");
                });

            modelBuilder.Entity("BloodDonationDb.Domain.Entities.Receiver", b =>
                {
                    b.Navigation("Donations");
                });
#pragma warning restore 612, 618
        }
    }
}
