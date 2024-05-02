using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class some_chages_in_Ordertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "OrderTable");

            migrationBuilder.AddColumn<int>(
                name: "In_Stock",
                table: "OrderTable",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "In_Stock",
                table: "OrderTable");

            migrationBuilder.AddColumn<int>(
                name: "Sales_id",
                table: "StockTable",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "OrderTable",
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
                principalColumn: "Id");
        }
    }
}
