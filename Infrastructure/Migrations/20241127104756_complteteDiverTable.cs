using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class complteteDiverTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DriverId",
                table: "Users",
                column: "DriverId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Drivers_DriverId",
                table: "Users",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Drivers_DriverId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DriverId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Users");
        }
    }
}
