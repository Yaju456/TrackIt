using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class orderhasproducttableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTable_ProductTable_Product_id",
                table: "OrderTable");

            migrationBuilder.DropIndex(
                name: "IX_OrderTable_Product_id",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "In_Stock",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "Product_id",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderTable");

            migrationBuilder.CreateTable(
                name: "OrderhasProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_id = table.Column<int>(type: "int", nullable: false),
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderhasProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderhasProducts_OrderTable_Order_id",
                        column: x => x.Order_id,
                        principalTable: "OrderTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderhasProducts_ProductTable_Product_id",
                        column: x => x.Product_id,
                        principalTable: "ProductTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderhasProducts_Order_id",
                table: "OrderhasProducts",
                column: "Order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderhasProducts_Product_id",
                table: "OrderhasProducts",
                column: "Product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderhasProducts");

            migrationBuilder.AddColumn<int>(
                name: "In_Stock",
                table: "OrderTable",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Product_id",
                table: "OrderTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderTable_Product_id",
                table: "OrderTable",
                column: "Product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_ProductTable_Product_id",
                table: "OrderTable",
                column: "Product_id",
                principalTable: "ProductTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
