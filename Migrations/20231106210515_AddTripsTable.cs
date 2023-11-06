using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLIappDT.Migrations
{
    /// <inheritdoc />
    public partial class AddTripsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickUpDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DropOffDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassangerCount = table.Column<int>(type: "int", nullable: false),
                    TripDistance = table.Column<double>(type: "float", nullable: false),
                    StoreAndFwdFlag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PULocationID = table.Column<int>(type: "int", nullable: false),
                    DOLocationID = table.Column<int>(type: "int", nullable: false),
                    FareAmount = table.Column<double>(type: "float", nullable: false),
                    TripAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Trips");
        }
    }
}
