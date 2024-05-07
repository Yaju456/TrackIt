using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class made_bill_id_nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_billhasProduct_Bill_Bill_id",
                table: "billhasProduct");

            migrationBuilder.AlterColumn<int>(
                name: "Bill_id",
                table: "billhasProduct",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_billhasProduct_Bill_Bill_id",
                table: "billhasProduct",
                column: "Bill_id",
                principalTable: "Bill",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_billhasProduct_Bill_Bill_id",
                table: "billhasProduct");

            migrationBuilder.AlterColumn<int>(
                name: "Bill_id",
                table: "billhasProduct",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_billhasProduct_Bill_Bill_id",
                table: "billhasProduct",
                column: "Bill_id",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
