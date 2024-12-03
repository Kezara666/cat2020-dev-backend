using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.OnlinePaymentDb
{
    public partial class AddStartEndDatesToEachBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "online_booking_cremation_date",
                table: "online_Bookings",
                newName: "online_booking_creation_date");

            migrationBuilder.CreateTable(
                name: "booking_date",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    booking_dates_property_id = table.Column<int>(type: "int", nullable: false),
                    booking_dates_sub_property_id = table.Column<int>(type: "int", nullable: false),
                    booking_time_slot_ids = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_dates_booking_status = table.Column<int>(type: "int", nullable: false),
                    booking_dates_start_date = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_dates_end_date = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_dates_created_by = table.Column<int>(type: "int", nullable: true),
                    booking_dates_updated_by = table.Column<int>(type: "int", nullable: true),
                    booking_dates_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    booking_dates_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    booking_dates_online_booking_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_booking_date_booking_main_property_type_booking_dates_proper~",
                        column: x => x.booking_dates_property_id,
                        principalTable: "booking_main_property_type",
                        principalColumn: "booking_property_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_date_booking_sub_property_type_booking_dates_sub_pro~",
                        column: x => x.booking_dates_sub_property_id,
                        principalTable: "booking_sub_property_type",
                        principalColumn: "booking_sub_property_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_date_online_Bookings_booking_dates_online_booking_id",
                        column: x => x.booking_dates_online_booking_id,
                        principalTable: "online_Bookings",
                        principalColumn: "online_booking_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_booking_date_booking_dates_online_booking_id",
                table: "booking_date",
                column: "booking_dates_online_booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_date_booking_dates_property_id",
                table: "booking_date",
                column: "booking_dates_property_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_date_booking_dates_sub_property_id",
                table: "booking_date",
                column: "booking_dates_sub_property_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_date");

            migrationBuilder.RenameColumn(
                name: "online_booking_creation_date",
                table: "online_Bookings",
                newName: "online_booking_cremation_date");
        }
    }
}
