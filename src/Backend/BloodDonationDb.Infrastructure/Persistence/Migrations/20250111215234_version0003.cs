using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonationDb.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class version0003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BloodsStock",
                columns: new[] { "Id", "BloodType", "CreateOn", "MinimumQuantityReached", "Quantity", "RhFactor" },
                values: new object[,]
                {
                    { new Guid("380bad32-4f0a-477d-bc32-7c09ec276053"), 3, new DateTime(2025, 1, 11, 21, 52, 34, 420, DateTimeKind.Utc).AddTicks(4430), true, 0, 2 },
                    { new Guid("4846e79f-d2e3-4efb-a0a1-d07fbe6d0df7"), 3, new DateTime(2025, 1, 11, 21, 52, 34, 420, DateTimeKind.Utc).AddTicks(4415), true, 0, 1 },
                    { new Guid("5e46bee4-6f33-4e58-8f21-911d6274ed77"), 1, new DateTime(2025, 1, 11, 21, 52, 34, 420, DateTimeKind.Utc).AddTicks(4408), true, 0, 2 },
                    { new Guid("865d95f8-5b7a-4358-989f-75000703656c"), 2, new DateTime(2025, 1, 11, 21, 52, 34, 420, DateTimeKind.Utc).AddTicks(4411), true, 0, 1 },
                    { new Guid("8eab636c-addb-49d4-a294-c8385c3fba83"), 4, new DateTime(2025, 1, 11, 21, 52, 34, 420, DateTimeKind.Utc).AddTicks(4414), true, 0, 2 },
                    { new Guid("9bf14aac-e979-4cf7-9596-a55ef42b793e"), 2, new DateTime(2025, 1, 11, 21, 52, 34, 420, DateTimeKind.Utc).AddTicks(4412), true, 0, 2 },
                    { new Guid("c2529921-618b-44c5-b6d1-bf39add7fe3a"), 4, new DateTime(2025, 1, 11, 21, 52, 34, 420, DateTimeKind.Utc).AddTicks(4413), true, 0, 1 },
                    { new Guid("f1e2e070-f8bd-4c42-8b81-86cb86d2b304"), 1, new DateTime(2025, 1, 11, 21, 52, 34, 420, DateTimeKind.Utc).AddTicks(2990), true, 0, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("380bad32-4f0a-477d-bc32-7c09ec276053"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("4846e79f-d2e3-4efb-a0a1-d07fbe6d0df7"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("5e46bee4-6f33-4e58-8f21-911d6274ed77"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("865d95f8-5b7a-4358-989f-75000703656c"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("8eab636c-addb-49d4-a294-c8385c3fba83"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("9bf14aac-e979-4cf7-9596-a55ef42b793e"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("c2529921-618b-44c5-b6d1-bf39add7fe3a"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("f1e2e070-f8bd-4c42-8b81-86cb86d2b304"));

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
    }
}
