using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateRouteStop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_Drivers_DriverId",
                table: "RouteStops");

            migrationBuilder.DropIndex(
                name: "IX_RouteStops_DriverId",
                table: "RouteStops");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "RouteStops");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "RouteStops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_DriverId",
                table: "RouteStops",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_Drivers_DriverId",
                table: "RouteStops",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
