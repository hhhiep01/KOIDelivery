using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixTransportService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TransportServices_TransportServiceId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TransportServiceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TransportServiceId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TransportServiceId",
                table: "Orders",
                column: "TransportServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_TransportServices_TransportServiceId",
                table: "Orders",
                column: "TransportServiceId",
                principalTable: "TransportServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TransportServices_TransportServiceId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TransportServiceId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "TransportServiceId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TransportServiceId",
                table: "Users",
                column: "TransportServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TransportServices_TransportServiceId",
                table: "Users",
                column: "TransportServiceId",
                principalTable: "TransportServices",
                principalColumn: "Id");
        }
    }
}
