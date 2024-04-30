using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class for_customers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesTable_ClientTable_Clinent_id",
                table: "SalesTable");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_ClientTable_Client_id",
                table: "StockTable");

            migrationBuilder.DropTable(
                name: "ClientTable");

            migrationBuilder.AlterColumn<string>(
                name: "Client_id",
                table: "StockTable",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clinent_id",
                table: "SalesTable",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Client_id",
                table: "SalesTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTable_AspNetUsers_Clinent_id",
                table: "SalesTable",
                column: "Clinent_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_AspNetUsers_Client_id",
                table: "StockTable",
                column: "Client_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesTable_AspNetUsers_Clinent_id",
                table: "SalesTable");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTable_AspNetUsers_Client_id",
                table: "StockTable");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Client_id",
                table: "StockTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Clinent_id",
                table: "SalesTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Client_id",
                table: "SalesTable",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ClientTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTable", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesTable_ClientTable_Clinent_id",
                table: "SalesTable",
                column: "Clinent_id",
                principalTable: "ClientTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTable_ClientTable_Client_id",
                table: "StockTable",
                column: "Client_id",
                principalTable: "ClientTable",
                principalColumn: "Id");
        }
    }
}
