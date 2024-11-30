using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerKm",
                table: "TransportServices");

            migrationBuilder.AlterColumn<double>(
                name: "PricePerKg",
                table: "TransportServices",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "PricePerAmount",
                table: "TransportServices",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<double>(
                name: "TransportPrice",
                table: "TransportServices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransportPrice",
                table: "TransportServices");

            migrationBuilder.AlterColumn<float>(
                name: "PricePerKg",
                table: "TransportServices",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "PricePerAmount",
                table: "TransportServices",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<float>(
                name: "PricePerKm",
                table: "TransportServices",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
