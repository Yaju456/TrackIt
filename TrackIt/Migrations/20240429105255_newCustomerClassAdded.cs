using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class newCustomerClassAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_AspNetUsers_Client_id",
                table: "StockTable");

            migrationBuilder.DropIndex(
                name: "IX_StockTable_Client_id",
                table: "StockTable");

            migrationBuilder.DropColumn(
                name: "Client_id",
                table: "StockTable");

            migrationBuilder.AddColumn<int>(
                name: "Customer_id",
                table: "StockTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GauPalikaorMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerClass", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTable_Customer_id",
                table: "StockTable",
                column: "Customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_CustomerClass_Customer_id",
                table: "StockTable",
                column: "Customer_id",
                principalTable: "CustomerClass",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_CustomerClass_Customer_id",
                table: "StockTable");

            migrationBuilder.DropTable(
                name: "CustomerClass");

            migrationBuilder.DropIndex(
                name: "IX_StockTable_Customer_id",
                table: "StockTable");

            migrationBuilder.DropColumn(
                name: "Customer_id",
                table: "StockTable");

            migrationBuilder.AddColumn<string>(
                name: "Client_id",
                table: "StockTable",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTable_Client_id",
                table: "StockTable",
                column: "Client_id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_AspNetUsers_Client_id",
                table: "StockTable",
                column: "Client_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
