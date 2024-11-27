using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Drivers_DriverId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DriverId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DriverId",
                table: "Users",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Drivers_DriverId",
                table: "Users",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
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

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
