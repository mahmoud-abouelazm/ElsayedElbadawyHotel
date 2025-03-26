using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElSayedHotel.Migrations
{
    /// <inheritdoc />
    public partial class owneridaddedtothereservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Reservation",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Reservation");
        }
    }
}
