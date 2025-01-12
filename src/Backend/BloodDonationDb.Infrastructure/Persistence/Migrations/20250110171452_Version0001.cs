using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationDb.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Version0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodsStock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    RhFactor = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MinimumQuantityReached = table.Column<bool>(type: "bit", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodsStock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    IsDonor = table.Column<bool>(type: "bit", nullable: false),
                    LastDonation = table.Column<DateTime>(type: "datetime", nullable: false),
                    NextDonation = table.Column<DateTime>(type: "datetime", nullable: false),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    RhFactor = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonationDonors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodStockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    When = table.Column<DateTime>(type: "datetime", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationDonors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodStock_DonationDonor",
                        column: x => x.BloodStockId,
                        principalTable: "BloodsStock",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donor_Donation_Id",
                        column: x => x.DonorId,
                        principalTable: "Donors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DonationReceivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BloodStockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    When = table.Column<DateTime>(type: "datetime", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationReceivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodStock_DonationReceiver",
                        column: x => x.BloodStockId,
                        principalTable: "BloodsStock",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Receiver_Donation_Id",
                        column: x => x.ReceiverId,
                        principalTable: "Receivers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationDonors_BloodStockId",
                table: "DonationDonors",
                column: "BloodStockId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationDonors_DonorId",
                table: "DonationDonors",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationReceivers_BloodStockId",
                table: "DonationReceivers",
                column: "BloodStockId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationReceivers_ReceiverId",
                table: "DonationReceivers",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_Email",
                table: "Donors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_Email",
                table: "Receivers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonationDonors");

            migrationBuilder.DropTable(
                name: "DonationReceivers");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "BloodsStock");

            migrationBuilder.DropTable(
                name: "Receivers");
        }
    }
}
