using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDrivenSolutions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCarNamingToVehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentCarType",
                table: "Spaces",
                newName: "CurrentVehicleType");

            migrationBuilder.RenameColumn(
                name: "CurrentCarReg",
                table: "Spaces",
                newName: "CurrentVehicleReg");

            migrationBuilder.RenameIndex(
                name: "IX_Spaces_CurrentCarReg",
                table: "Spaces",
                newName: "IX_Spaces_CurrentVehicleReg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentVehicleType",
                table: "Spaces",
                newName: "CurrentCarType");

            migrationBuilder.RenameColumn(
                name: "CurrentVehicleReg",
                table: "Spaces",
                newName: "CurrentCarReg");

            migrationBuilder.RenameIndex(
                name: "IX_Spaces_CurrentVehicleReg",
                table: "Spaces",
                newName: "IX_Spaces_CurrentCarReg");
        }
    }
}
