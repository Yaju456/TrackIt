using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class orderhasproductkeyaddedtostocktable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "orderhasProduct_id",
                table: "StockTable",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "total",
                table: "Bill",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "payment",
                table: "Bill",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTable_orderhasProduct_id",
                table: "StockTable",
                column: "orderhasProduct_id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_OrderhasProducts_orderhasProduct_id",
                table: "StockTable",
                column: "orderhasProduct_id",
                principalTable: "OrderhasProducts",
                onDelete: ReferentialAction.NoAction,
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_OrderhasProducts_orderhasProduct_id",
                table: "StockTable");

            migrationBuilder.DropIndex(
                name: "IX_StockTable_orderhasProduct_id",
                table: "StockTable");

            migrationBuilder.DropColumn(
                name: "orderhasProduct_id",
                table: "StockTable");

            migrationBuilder.AlterColumn<int>(
                name: "total",
                table: "Bill",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "payment",
                table: "Bill",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
