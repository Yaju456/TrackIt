using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class removed_required : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockTable_serial_number",
                table: "StockTable");

            migrationBuilder.AlterColumn<string>(
                name: "serial_number",
                table: "StockTable",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_StockTable_serial_number",
                table: "StockTable",
                column: "serial_number",
                unique: true,
                filter: "[serial_number] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockTable_serial_number",
                table: "StockTable");

            migrationBuilder.AlterColumn<string>(
                name: "serial_number",
                table: "StockTable",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTable_serial_number",
                table: "StockTable",
                column: "serial_number",
                unique: true);
        }
    }
}
