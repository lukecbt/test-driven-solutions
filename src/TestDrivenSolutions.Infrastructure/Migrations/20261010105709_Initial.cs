using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestDrivenSolutions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reg = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Charge = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CostPerMinute = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsOccupied = table.Column<bool>(type: "boolean", nullable: false),
                    CurrentCarReg = table.Column<string>(type: "text", nullable: true),
                    CurrentCarType = table.Column<int>(type: "integer", nullable: true),
                    ParkingStartedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaces", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CarTypes",
                columns: new[] { "Id", "CostPerMinute", "Description" },
                values: new object[,]
                {
                    { 1, 0.10m, "Small car" },
                    { 2, 0.20m, "Medium car" },
                    { 3, 0.40m, "Large car" }
                });

            migrationBuilder.InsertData(
                table: "Spaces",
                columns: new[] { "Id", "CurrentCarReg", "CurrentCarType", "IsOccupied", "ParkingStartedTime" },
                values: new object[,]
                {
                    { 1, null, null, false, null },
                    { 2, null, null, false, null },
                    { 3, null, null, false, null },
                    { 4, null, null, false, null },
                    { 5, null, null, false, null },
                    { 6, null, null, false, null },
                    { 7, null, null, false, null },
                    { 8, null, null, false, null },
                    { 9, null, null, false, null },
                    { 10, null, null, false, null },
                    { 11, null, null, false, null },
                    { 12, null, null, false, null },
                    { 13, null, null, false, null },
                    { 14, null, null, false, null },
                    { 15, null, null, false, null },
                    { 16, null, null, false, null },
                    { 17, null, null, false, null },
                    { 18, null, null, false, null },
                    { 19, null, null, false, null },
                    { 20, null, null, false, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "Spaces");
        }
    }
}
