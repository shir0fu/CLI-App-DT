using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLIappDT.Migrations
{
    /// <inheritdoc />
    public partial class FixFindLocationWithMaxTipAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE FindLocationWithMaxTipAmount
                AS
                BEGIN
                    SELECT TOP 1 *
                    FROM Trips
                    WHERE PULocationId IN (
                        SELECT TOP 1 PULocationId
                        FROM Trips
                        GROUP BY PULocationId
                        ORDER BY AVG(TipAmount) DESC
                    );
                END;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
