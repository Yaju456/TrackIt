using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Migrations
{
    /// <inheritdoc />
    public partial class Adressofcust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "CustomerTable");

            migrationBuilder.DropColumn(
                name: "GauPalikaorMunicipality",
                table: "CustomerTable");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "CustomerTable");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "CustomerTable",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocalBodyId",
                table: "CustomerTable",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "CustomerTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    NameNp = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    IMUCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    NameNp = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IMUCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalBody",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameNp = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    isMunicipality = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IMUCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalBody", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalBody_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTable_DistrictId",
                table: "CustomerTable",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTable_LocalBodyId",
                table: "CustomerTable",
                column: "LocalBodyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTable_ProvinceId",
                table: "CustomerTable",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_District_ProvinceId",
                table: "District",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalBody_DistrictId",
                table: "LocalBody",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTable_District_DistrictId",
                table: "CustomerTable",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTable_LocalBody_LocalBodyId",
                table: "CustomerTable",
                column: "LocalBodyId",
                principalTable: "LocalBody",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTable_Province_ProvinceId",
                table: "CustomerTable",
                column: "ProvinceId",
                principalTable: "Province",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTable_District_DistrictId",
                table: "CustomerTable");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTable_LocalBody_LocalBodyId",
                table: "CustomerTable");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTable_Province_ProvinceId",
                table: "CustomerTable");

            migrationBuilder.DropTable(
                name: "LocalBody");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTable_DistrictId",
                table: "CustomerTable");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTable_LocalBodyId",
                table: "CustomerTable");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTable_ProvinceId",
                table: "CustomerTable");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "CustomerTable");

            migrationBuilder.DropColumn(
                name: "LocalBodyId",
                table: "CustomerTable");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "CustomerTable");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "CustomerTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GauPalikaorMunicipality",
                table: "CustomerTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "CustomerTable",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
