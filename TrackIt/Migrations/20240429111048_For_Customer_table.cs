using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class For_Customer_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_CustomerClass_Customer_id",
                table: "StockTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerClass",
                table: "CustomerClass");

            migrationBuilder.RenameTable(
                name: "CustomerClass",
                newName: "CustomerTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerTable",
                table: "CustomerTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_CustomerTable_Customer_id",
                table: "StockTable",
                column: "Customer_id",
                principalTable: "CustomerTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_CustomerTable_Customer_id",
                table: "StockTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerTable",
                table: "CustomerTable");

            migrationBuilder.RenameTable(
                name: "CustomerTable",
                newName: "CustomerClass");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerClass",
                table: "CustomerClass",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_CustomerClass_Customer_id",
                table: "StockTable",
                column: "Customer_id",
                principalTable: "CustomerClass",
                principalColumn: "Id");
        }
    }
}
