using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class mistakeinpreviousmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_OrderhasProducts_orderhasProduct_id",
                table: "StockTable");

            migrationBuilder.RenameColumn(
                name: "orderhasProduct_id",
                table: "StockTable",
                newName: "billhasProduct_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockTable_orderhasProduct_id",
                table: "StockTable",
                newName: "IX_StockTable_billhasProduct_id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_billhasProduct_billhasProduct_id",
                table: "StockTable",
                column: "billhasProduct_id",
                principalTable: "billhasProduct",
                onDelete: ReferentialAction.SetNull,
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_billhasProduct_billhasProduct_id",
                table: "StockTable");

            migrationBuilder.RenameColumn(
                name: "billhasProduct_id",
                table: "StockTable",
                newName: "orderhasProduct_id");

            migrationBuilder.RenameIndex(
                name: "IX_StockTable_billhasProduct_id",
                table: "StockTable",
                newName: "IX_StockTable_orderhasProduct_id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_OrderhasProducts_orderhasProduct_id",
                table: "StockTable",
                column: "orderhasProduct_id",
                principalTable: "OrderhasProducts",
                principalColumn: "Id");
        }
    }
}
