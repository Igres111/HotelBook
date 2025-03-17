using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RefactorBookingDbv20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "Bookings",
                newName: "DestinationCountry");

            migrationBuilder.AddColumn<string>(
                name: "DestinationCity",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationCity",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "DestinationCountry",
                table: "Bookings",
                newName: "Destination");
        }
    }
}
