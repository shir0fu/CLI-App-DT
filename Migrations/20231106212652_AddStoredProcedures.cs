using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLIappDT.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE FindTripsByLocation
                    @PULocationId INT
                AS
                BEGIN
                    SELECT *
                    FROM Trips
                    WHERE PULocationId = @PULocationId;
                END;
                ");
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE FindLongestTripsByTime
                AS
                BEGIN
                    SELECT TOP 100 *
                    FROM Trips
                    ORDER BY DATEDIFF(SECOND, PickUpDateTime, DropOffDateTime) DESC;
                END;
                ");
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE FindLongestTripsByDistance
                AS
                BEGIN
                    SELECT TOP 100 *
                    FROM Trips
                    ORDER BY TripDistance DESC;
                END;
                ");
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE FindLocationWithMaxTipAmount
                AS
                BEGIN
                    SELECT TOP 1 PULocationId, AVG(TipAmount) AS AverageTipAmount
                    FROM Trips
                    GROUP BY PULocationId
                    ORDER BY AverageTipAmount DESC;
                END;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS FindTripsByLocation;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS FindLongestTripsByTime;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS FindLongestTripsByDistance;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS FindLocationWithMaxTipAmount;");
        }
    }
}
