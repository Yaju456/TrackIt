using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sales_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Client_id = table.Column<int>(type: "int", nullable: false),
                    Clinent_id = table.Column<int>(type: "int", nullable: true),
                    Product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesTable_ClientTable_Clinent_id",
                        column: x => x.Clinent_id,
                        principalTable: "ClientTable",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesTable_ProductTable_Product_id",
                        column: x => x.Product_id,
                        principalTable: "ProductTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Arival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    vendor_id = table.Column<int>(type: "int", nullable: false),
                    Product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTable_ProductTable_Product_id",
                        column: x => x.Product_id,
                        principalTable: "ProductTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTable_VendorTable_vendor_id",
                        column: x => x.vendor_id,
                        principalTable: "VendorTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serial_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Order_id = table.Column<int>(type: "int", nullable: false),
                    Client_id = table.Column<int>(type: "int", nullable: false),
                    InStock = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTable", x => x.Id);
                    table.UniqueConstraint("AK_StockTable_serial_number", x => x.serial_number);
                    table.ForeignKey(
                        name: "FK_StockTable_ClientTable_Client_id",
                        column: x => x.Client_id,
                        principalTable: "ClientTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTable_OrderTable_Order_id",
                        column: x => x.Order_id,
                        principalTable: "OrderTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTable_ProductTable_Product_id",
                        column: x => x.Product_id,
                        principalTable: "ProductTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderTable_Product_id",
                table: "OrderTable",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTable_vendor_id",
                table: "OrderTable",
                column: "vendor_id");

            migrationBuilder.CreateIndex(
                name: "IX_SalesTable_Clinent_id",
                table: "SalesTable",
                column: "Clinent_id");

            migrationBuilder.CreateIndex(
                name: "IX_SalesTable_Product_id",
                table: "SalesTable",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_StockTable_Client_id",
                table: "StockTable",
                column: "Client_id");

            migrationBuilder.CreateIndex(
                name: "IX_StockTable_Order_id",
                table: "StockTable",
                column: "Order_id");

            migrationBuilder.CreateIndex(
                name: "IX_StockTable_Product_id",
                table: "StockTable",
                column: "Product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesTable");

            migrationBuilder.DropTable(
                name: "StockTable");

            migrationBuilder.DropTable(
                name: "ClientTable");

            migrationBuilder.DropTable(
                name: "OrderTable");

            migrationBuilder.DropTable(
                name: "ProductTable");

            migrationBuilder.DropTable(
                name: "VendorTable");
        }
    }
}
