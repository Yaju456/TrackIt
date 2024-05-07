using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyinBucket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Order_id",
                table: "Bucket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Bucket_Product_id",
                table: "Bucket",
                column: "Product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bucket_ProductTable_Product_id",
                table: "Bucket",
                column: "Product_id",
                principalTable: "ProductTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bucket_ProductTable_Product_id",
                table: "Bucket");

            migrationBuilder.DropIndex(
                name: "IX_Bucket_Product_id",
                table: "Bucket");

            migrationBuilder.AlterColumn<int>(
                name: "Order_id",
                table: "Bucket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
