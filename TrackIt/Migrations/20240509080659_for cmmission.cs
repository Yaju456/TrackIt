using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class forcmmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Payment",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "commission",
                table: "Payment",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "commissionper",
                table: "Payment",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "commission",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "commissionper",
                table: "Payment");
        }
    }
}
