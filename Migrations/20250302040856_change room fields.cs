using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElSayedHotel.Migrations
{
    /// <inheritdoc />
    public partial class changeroomfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Room",
                newName: "ownerRoomName");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Room",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.DistrictId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Room_CountryId",
                table: "Room",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_DistrictId",
                table: "Room",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Country_CountryId",
                table: "Room",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_District_DistrictId",
                table: "Room",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "DistrictId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Country_CountryId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_District_DistrictId",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropIndex(
                name: "IX_Room_CountryId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_DistrictId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "ownerRoomName",
                table: "Room",
                newName: "Name");
        }
    }
}
