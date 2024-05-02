using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class removed_sales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesTable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clinent_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    Client_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Sales_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesTable_AspNetUsers_Clinent_id",
                        column: x => x.Clinent_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesTable_ProductTable_Product_id",
                        column: x => x.Product_id,
                        principalTable: "ProductTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesTable_Clinent_id",
                table: "SalesTable",
                column: "Clinent_id");

            migrationBuilder.CreateIndex(
                name: "IX_SalesTable_Product_id",
                table: "SalesTable",
                column: "Product_id");
        }
    }
}
