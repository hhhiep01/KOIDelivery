using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Routes_RouteId1",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_RouteId1",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "RouteId1",
                table: "Drivers");

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

            migrationBuilder.CreateIndex(
                name: "IX_Routes_DriverId",
                table: "Routes",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Drivers_DriverId",
                table: "Routes",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_Drivers_DriverId",
                table: "RouteStops",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Drivers_DriverId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_Drivers_DriverId",
                table: "RouteStops");

            migrationBuilder.DropIndex(
                name: "IX_RouteStops_DriverId",
                table: "RouteStops");

            migrationBuilder.DropIndex(
                name: "IX_Routes_DriverId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "RouteStops");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Drivers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RouteId1",
                table: "Drivers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_RouteId1",
                table: "Drivers",
                column: "RouteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Routes_RouteId1",
                table: "Drivers",
                column: "RouteId1",
                principalTable: "Routes",
                principalColumn: "Id");
        }
    }
}
