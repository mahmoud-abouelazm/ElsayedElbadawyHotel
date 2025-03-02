using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElSayedHotel.Migrations
{
    /// <inheritdoc />
    public partial class updatafields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Country_CountryId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_CountryId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Room");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "District",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GovernorateId",
                table: "District",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Governorate",
                columns: table => new
                {
                    GovernorateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    GovernorateName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorate", x => x.GovernorateId);
                    table.ForeignKey(
                        name: "FK_Governorate_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_District_CountryId",
                table: "District",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_District_GovernorateId",
                table: "District",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Governorate_CountryId",
                table: "Governorate",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_District_Country_CountryId",
                table: "District",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_District_Governorate_GovernorateId",
                table: "District",
                column: "GovernorateId",
                principalTable: "Governorate",
                principalColumn: "GovernorateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_District_Country_CountryId",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_District_Governorate_GovernorateId",
                table: "District");

            migrationBuilder.DropTable(
                name: "Governorate");

            migrationBuilder.DropIndex(
                name: "IX_District_CountryId",
                table: "District");

            migrationBuilder.DropIndex(
                name: "IX_District_GovernorateId",
                table: "District");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "District");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                table: "District");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Room_CountryId",
                table: "Room",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Country_CountryId",
                table: "Room",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
