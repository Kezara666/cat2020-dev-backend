using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.OnlinePaymentDb
{
    public partial class onlineBookingChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "online_booking_booking_time_slot_id",
                table: "online_Bookings");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "online_Bookings",
                newName: "online_booking_total_amount");

            migrationBuilder.RenameColumn(
                name: "online_booking_start_date",
                table: "online_Bookings",
                newName: "online_booking_cremation_date");

            migrationBuilder.RenameColumn(
                name: "online_booking_end_date",
                table: "online_Bookings",
                newName: "online_booking_booking_time_slot_ids");

            migrationBuilder.RenameColumn(
                name: "sabha_id",
                table: "booking_time_slot",
                newName: "booking_time_slot_sabha_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "online_booking_total_amount",
                table: "online_Bookings",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "online_booking_cremation_date",
                table: "online_Bookings",
                newName: "online_booking_start_date");

            migrationBuilder.RenameColumn(
                name: "online_booking_booking_time_slot_ids",
                table: "online_Bookings",
                newName: "online_booking_end_date");

            migrationBuilder.RenameColumn(
                name: "booking_time_slot_sabha_id",
                table: "booking_time_slot",
                newName: "sabha_id");

            migrationBuilder.AddColumn<int>(
                name: "online_booking_booking_time_slot_id",
                table: "online_Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
