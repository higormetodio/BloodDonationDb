using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonationDb.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class version0004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_RefreshToken",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BloodsStock",
                columns: new[] { "Id", "BloodType", "CreateOn", "MinimumQuantityReached", "Quantity", "RhFactor" },
                values: new object[,]
                {
                    { new Guid("15eef3f4-a8b8-4635-9deb-16d43f8b9674"), 1, new DateTime(2025, 1, 15, 18, 52, 19, 160, DateTimeKind.Utc).AddTicks(8870), true, 0, 2 },
                    { new Guid("461e27ba-50fa-4b57-bd76-bfe2cf34f568"), 1, new DateTime(2025, 1, 15, 18, 52, 19, 160, DateTimeKind.Utc).AddTicks(7853), true, 0, 1 },
                    { new Guid("5bbbdb40-5e18-4723-8cae-c4954a50aa2d"), 3, new DateTime(2025, 1, 15, 18, 52, 19, 160, DateTimeKind.Utc).AddTicks(8890), true, 0, 1 },
                    { new Guid("c125e184-ace3-4bd4-a006-e9808de06ae5"), 4, new DateTime(2025, 1, 15, 18, 52, 19, 160, DateTimeKind.Utc).AddTicks(8889), true, 0, 2 },
                    { new Guid("ca3c1ca6-c570-4d4d-a332-3158fee61460"), 2, new DateTime(2025, 1, 15, 18, 52, 19, 160, DateTimeKind.Utc).AddTicks(8874), true, 0, 2 },
                    { new Guid("ca833add-4bea-4aa6-9ce2-ddaded39c26a"), 3, new DateTime(2025, 1, 15, 18, 52, 19, 160, DateTimeKind.Utc).AddTicks(8891), true, 0, 2 },
                    { new Guid("d191dcd2-4628-48dd-8621-88c660fbb7c5"), 4, new DateTime(2025, 1, 15, 18, 52, 19, 160, DateTimeKind.Utc).AddTicks(8875), true, 0, 1 },
                    { new Guid("fa7a398d-1501-47d5-94a3-c7b248023e53"), 2, new DateTime(2025, 1, 15, 18, 52, 19, 160, DateTimeKind.Utc).AddTicks(8873), true, 0, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("15eef3f4-a8b8-4635-9deb-16d43f8b9674"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("461e27ba-50fa-4b57-bd76-bfe2cf34f568"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("5bbbdb40-5e18-4723-8cae-c4954a50aa2d"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("c125e184-ace3-4bd4-a006-e9808de06ae5"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("ca3c1ca6-c570-4d4d-a332-3158fee61460"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("ca833add-4bea-4aa6-9ce2-ddaded39c26a"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("d191dcd2-4628-48dd-8621-88c660fbb7c5"));

            migrationBuilder.DeleteData(
                table: "BloodsStock",
                keyColumn: "Id",
                keyValue: new Guid("fa7a398d-1501-47d5-94a3-c7b248023e53"));

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
    }
}
