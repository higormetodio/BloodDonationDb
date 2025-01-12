using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonationDb.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Version0002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BloodsStock",
                columns: new[] { "Id", "BloodType", "CreateOn", "MinimumQuantityReached", "Quantity", "RhFactor" },
                values: new object[,]
                {
                    { new Guid("1e19bc40-b748-4d22-a32d-68787f6fc2f9"), 3, new DateTime(2025, 1, 10, 17, 30, 24, 836, DateTimeKind.Utc).AddTicks(45), true, 0, 2 },
                    { new Guid("33fb19a8-b5e2-4318-91d7-487886f9c979"), 1, new DateTime(2025, 1, 10, 17, 30, 24, 835, DateTimeKind.Utc).AddTicks(8900), true, 0, 1 },
                    { new Guid("5b6ba21e-b750-4f45-96e3-02493050c389"), 4, new DateTime(2025, 1, 10, 17, 30, 24, 836, DateTimeKind.Utc).AddTicks(42), true, 0, 1 },
                    { new Guid("5ca06d01-80e4-4047-931b-f41cdc915baa"), 2, new DateTime(2025, 1, 10, 17, 30, 24, 836, DateTimeKind.Utc).AddTicks(41), true, 0, 2 },
                    { new Guid("a6039afe-20dd-4f44-a212-460637c80d9b"), 4, new DateTime(2025, 1, 10, 17, 30, 24, 836, DateTimeKind.Utc).AddTicks(43), true, 0, 2 },
                    { new Guid("c5221658-f79a-4c3f-b1c8-0728aacd41f6"), 2, new DateTime(2025, 1, 10, 17, 30, 24, 836, DateTimeKind.Utc).AddTicks(29), true, 0, 1 },
                    { new Guid("da9e738e-9865-4a6d-ac58-23a3e65aac73"), 3, new DateTime(2025, 1, 10, 17, 30, 24, 836, DateTimeKind.Utc).AddTicks(44), true, 0, 1 },
                    { new Guid("de07a1a7-ff76-4fe5-bf07-e993c9472edc"), 1, new DateTime(2025, 1, 10, 17, 30, 24, 836, DateTimeKind.Utc).AddTicks(25), true, 0, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("1e19bc40-b748-4d22-a32d-68787f6fc2f9"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("33fb19a8-b5e2-4318-91d7-487886f9c979"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("5b6ba21e-b750-4f45-96e3-02493050c389"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("5ca06d01-80e4-4047-931b-f41cdc915baa"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("a6039afe-20dd-4f44-a212-460637c80d9b"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("c5221658-f79a-4c3f-b1c8-0728aacd41f6"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("da9e738e-9865-4a6d-ac58-23a3e65aac73"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("de07a1a7-ff76-4fe5-bf07-e993c9472edc"));
        }
    }
}
