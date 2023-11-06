using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLIappDT.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TripAmount",
                table: "Trips",
                newName: "TipAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipAmount",
                table: "Trips",
                newName: "TripAmount");
        }
    }
}
