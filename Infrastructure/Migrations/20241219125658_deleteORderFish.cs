using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deleteORderFish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FishHealths_OrderFishs_OrderFishId",
                table: "FishHealths");

            migrationBuilder.DropForeignKey(
                name: "FK_FishQualifications_OrderFishs_OrderFishId",
                table: "FishQualifications");

            migrationBuilder.DropTable(
                name: "OrderFishs");

            migrationBuilder.DropIndex(
                name: "IX_FishQualifications_OrderFishId",
                table: "FishQualifications");

            migrationBuilder.DropIndex(
                name: "IX_FishHealths_OrderFishId",
                table: "FishHealths");

            migrationBuilder.DropColumn(
                name: "OrderFishId",
                table: "FishQualifications");

            migrationBuilder.DropColumn(
                name: "OrderFishId",
                table: "FishHealths");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderFishId",
                table: "FishQualifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderFishId",
                table: "FishHealths",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderFishs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FishImgURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAccountId = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderFishs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderFishs_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderFishs_Users_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FishQualifications_OrderFishId",
                table: "FishQualifications",
                column: "OrderFishId");

            migrationBuilder.CreateIndex(
                name: "IX_FishHealths_OrderFishId",
                table: "FishHealths",
                column: "OrderFishId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderFishs_OrderId",
                table: "OrderFishs",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderFishs_UserAccountId",
                table: "OrderFishs",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_FishHealths_OrderFishs_OrderFishId",
                table: "FishHealths",
                column: "OrderFishId",
                principalTable: "OrderFishs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FishQualifications_OrderFishs_OrderFishId",
                table: "FishQualifications",
                column: "OrderFishId",
                principalTable: "OrderFishs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
