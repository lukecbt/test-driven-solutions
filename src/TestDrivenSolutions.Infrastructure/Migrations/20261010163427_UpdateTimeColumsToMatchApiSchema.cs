using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDrivenSolutions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTimeColumsToMatchApiSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParkingStartedTime",
                table: "Spaces",
                newName: "TimeIn");

            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "Cars",
                newName: "TimeOut");

            migrationBuilder.RenameColumn(
                name: "ArrivalTime",
                table: "Cars",
                newName: "TimeIn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeIn",
                table: "Spaces",
                newName: "ParkingStartedTime");

            migrationBuilder.RenameColumn(
                name: "TimeOut",
                table: "Cars",
                newName: "DepartureTime");

            migrationBuilder.RenameColumn(
                name: "TimeIn",
                table: "Cars",
                newName: "ArrivalTime");
        }
    }
}
