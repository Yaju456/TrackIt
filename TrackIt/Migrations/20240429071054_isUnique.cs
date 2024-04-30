using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class isUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_StockTable_serial_number",
                table: "StockTable");

            migrationBuilder.CreateIndex(
                name: "IX_StockTable_serial_number",
                table: "StockTable",
                column: "serial_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockTable_serial_number",
                table: "StockTable");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StockTable_serial_number",
                table: "StockTable",
                column: "serial_number");
        }
    }
}
