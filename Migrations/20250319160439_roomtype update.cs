using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElSayedHotel.Migrations
{
    /// <inheritdoc />
    public partial class roomtypeupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomType_roomType",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "roomType",
                table: "Room",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomType_roomType",
                table: "Room",
                column: "roomType",
                principalTable: "RoomType",
                principalColumn: "RoomType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomType_roomType",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "roomType",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomType_roomType",
                table: "Room",
                column: "roomType",
                principalTable: "RoomType",
                principalColumn: "RoomType",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
