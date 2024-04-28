using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class making_foreignkeyNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_ClientTable_Client_id",
                table: "StockTable");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_SalesTable_Sales_id",
                table: "StockTable");

            migrationBuilder.AlterColumn<int>(
                name: "Sales_id",
                table: "StockTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Client_id",
                table: "StockTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_ClientTable_Client_id",
                table: "StockTable",
                column: "Client_id",
                principalTable: "ClientTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_SalesTable_Sales_id",
                table: "StockTable",
                column: "Sales_id",
                principalTable: "SalesTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_ClientTable_Client_id",
                table: "StockTable");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_SalesTable_Sales_id",
                table: "StockTable");

            migrationBuilder.AlterColumn<int>(
                name: "Sales_id",
                table: "StockTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Client_id",
                table: "StockTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_ClientTable_Client_id",
                table: "StockTable",
                column: "Client_id",
                principalTable: "ClientTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_SalesTable_Sales_id",
                table: "StockTable",
                column: "Sales_id",
                principalTable: "SalesTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
