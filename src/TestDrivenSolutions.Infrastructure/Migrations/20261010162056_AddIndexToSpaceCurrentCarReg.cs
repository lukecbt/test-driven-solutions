using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDrivenSolutions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToSpaceCurrentCarReg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Spaces_CurrentCarReg",
                table: "Spaces",
                column: "CurrentCarReg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Spaces_CurrentCarReg",
                table: "Spaces");
        }
    }
}
