using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class update_in_Items : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Modal",
                table: "ProductTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modal",
                table: "ProductTable");
        }
    }
}
