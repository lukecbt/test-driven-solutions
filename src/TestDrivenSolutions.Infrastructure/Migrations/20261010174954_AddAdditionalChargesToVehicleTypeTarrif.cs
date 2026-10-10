using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestDrivenSolutions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdditionalChargesToVehicleTypeTarrif : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.CreateTable(
                name: "VehicleTypeTarrifs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CostPerMinute = table.Column<decimal>(type: "numeric", nullable: false),
                    AdditionalChargeMinutes = table.Column<int>(type: "integer", nullable: false),
                    AdditionalChargeAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypeTarrifs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VehicleTypeTarrifs",
                columns: new[] { "Id", "AdditionalChargeAmount", "AdditionalChargeMinutes", "CostPerMinute", "Description" },
                values: new object[,]
                {
                    { 1, 1m, 5, 0.10m, "Small car" },
                    { 2, 1m, 5, 0.20m, "Medium car" },
                    { 3, 1m, 5, 0.40m, "Large car" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleTypeTarrifs");

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CostPerMinute = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "Id", "CostPerMinute", "Description" },
                values: new object[,]
                {
                    { 1, 0.10m, "Small car" },
                    { 2, 0.20m, "Medium car" },
                    { 3, 0.40m, "Large car" }
                });
        }
    }
}
