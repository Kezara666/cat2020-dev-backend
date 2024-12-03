using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.OnlinePaymentDb
{
    public partial class OnlineBookingAddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookingStatus",
                table: "online_Bookings",
                newName: "online_booking_status");

            migrationBuilder.AddColumn<int>(
                name: "online_booking_sabha_id",
                table: "online_Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "online_booking_sabha_id",
                table: "online_Bookings");

            migrationBuilder.RenameColumn(
                name: "online_booking_status",
                table: "online_Bookings",
                newName: "BookingStatus");
        }
    }
}
