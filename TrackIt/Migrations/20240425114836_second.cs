using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sales_id",
                table: "StockTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockTable_Sales_id",
                table: "StockTable",
                column: "Sales_id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_SalesTable_Sales_id",
                table: "StockTable",
                column: "Sales_id",
                principalTable: "SalesTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_SalesTable_Sales_id",
                table: "StockTable");

            migrationBuilder.DropIndex(
                name: "IX_StockTable_Sales_id",
                table: "StockTable");

            migrationBuilder.DropColumn(
                name: "Sales_id",
                table: "StockTable");
        }
    }
}
