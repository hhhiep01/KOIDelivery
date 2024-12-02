using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeDataTransportService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromProvince",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ToProvince",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "FromProvince",
                table: "TransportServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToProvince",
                table: "TransportServices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromProvince",
                table: "TransportServices");

            migrationBuilder.DropColumn(
                name: "ToProvince",
                table: "TransportServices");

            migrationBuilder.AddColumn<string>(
                name: "FromProvince",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToProvince",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
