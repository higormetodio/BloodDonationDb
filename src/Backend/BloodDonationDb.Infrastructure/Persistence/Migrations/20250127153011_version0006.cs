using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonationDb.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class version0006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.AddColumn<int>(
                name: "BloodType",
                table: "Receivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RhFactor",
                table: "Receivers",
                type: "int",
                nullable: false,
                defaultValue: 0);         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
