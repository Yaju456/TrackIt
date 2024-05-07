using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class PhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PhoneNumber",
                table: "CustomerTable",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "PhoneNumberCheck",
                table: "CustomerTable",
                sql: "PhoneNumber between 9800000000 and 9899999999");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "PhoneNumberCheck",
                table: "CustomerTable");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CustomerTable");
        }
    }
}
