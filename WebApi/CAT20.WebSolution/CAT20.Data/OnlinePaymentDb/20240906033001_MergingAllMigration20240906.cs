using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.OnlinePaymentDb
{
    public partial class MergingAllMigration20240906 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "booking_main_property_type",
                columns: table => new
                {
                    booking_property_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    booking_main_property_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_main_property_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_main_property_status = table.Column<int>(type: "int", nullable: true),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    booking_main_property_created_by = table.Column<int>(type: "int", nullable: true),
                    booking_main_property_updated_by = table.Column<int>(type: "int", nullable: true),
                    booking_main_property_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    booking_main_property_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_main_property_type", x => x.booking_property_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "logIn_details",
                columns: table => new
                {
                    logInID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mobile_no = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ip_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    device = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    operating_system = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    os_version = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    browser = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    browser_version = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    device_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    orientation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    log_in_time = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("log_in_id", x => x.logInID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "online_Bookings",
                columns: table => new
                {
                    online_booking_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    online_booking_property_id = table.Column<int>(type: "int", nullable: false),
                    online_booking_sub_property_id = table.Column<int>(type: "int", nullable: false),
                    online_booking_customer_id = table.Column<int>(type: "int", nullable: false),
                    online_booking_start_date = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    online_booking_end_date = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    online_booking_booking_time_slot_id = table.Column<int>(type: "int", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    online_booking_payament_status = table.Column<int>(type: "int", nullable: false),
                    online_booking_transaction_id = table.Column<int>(type: "int", nullable: false),
                    online_booking_approved_by = table.Column<int>(type: "int", nullable: false),
                    online_booking_approved_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    online_booking_rejected_reason = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    online_booking_cansellation_reason = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    online_booking_booking_notes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_time_slot_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    booking_time_slot_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_online_Bookings", x => x.online_booking_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "payment_detail_backup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    payment_detail_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "payment_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    transaction_id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    session_id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    result_indicator = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    error = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    input_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    service_percentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    service_charges = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    unique_id = table.Column<int>(type: "int", nullable: true),
                    account_no = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    partner_id = table.Column<int>(type: "int", nullable: true),
                    partner_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    partner_nic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    partner_mobile_no = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    partner_email_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    office_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    office_session_id = table.Column<int>(type: "int", nullable: true),
                    check = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    cashier_id = table.Column<int>(type: "int", nullable: true, defaultValue: -1),
                    cashier_updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_details", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "payment_gateways",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    province_id = table.Column<int>(type: "int", nullable: true),
                    bank_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    merchant_id = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    api_key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    report_api_key = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    service_percentage = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    ProfileID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecretKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "booking_sub_property_type",
                columns: table => new
                {
                    booking_sub_property_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    booking_sub_property_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_sub_property_code = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_sub_property_status = table.Column<int>(type: "int", nullable: false),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    booking_main_property_id = table.Column<int>(type: "int", nullable: false),
                    booking_sub_property_address = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_sub_property_telephone_number = table.Column<int>(type: "int", nullable: false),
                    booking_sub_property_latitude = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_sub_property_longitude = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_sub_property_created_by = table.Column<int>(type: "int", nullable: true),
                    booking_sub_property_updated_by = table.Column<int>(type: "int", nullable: true),
                    booking_sub_property_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    booking_sub_property_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_sub_property_type", x => x.booking_sub_property_id);
                    table.ForeignKey(
                        name: "fk_booking_sub_property_booking_main_property_id",
                        column: x => x.booking_main_property_id,
                        principalTable: "booking_main_property_type",
                        principalColumn: "booking_property_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "dispute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    payment_detail_id = table.Column<int>(type: "int", nullable: true),
                    reason = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    message = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dispute_Payment_Detail_Id",
                        column: x => x.payment_detail_id,
                        principalTable: "payment_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "other_description",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherDescription_Payment_Detail_Id",
                        column: x => x.Id,
                        principalTable: "payment_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "booking_charging_scheme",
                columns: table => new
                {
                    booking_cs_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    booking_cs_subproperty_id = table.Column<int>(type: "int", nullable: false),
                    booking_cs_charging_type = table.Column<int>(type: "int", nullable: false),
                    booking_cs_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    booking_cs_status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    booking_cs_sabha_id = table.Column<int>(type: "int", nullable: true),
                    booking_cs_created_by = table.Column<int>(type: "int", nullable: true),
                    booking_cs_updated_by = table.Column<int>(type: "int", nullable: true),
                    booking_cs_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    booking_cs_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_charging_scheme", x => x.booking_cs_id);
                    table.ForeignKey(
                        name: "FK_booking_charging_scheme_booking_sub_property_type_booking_cs~",
                        column: x => x.booking_cs_subproperty_id,
                        principalTable: "booking_sub_property_type",
                        principalColumn: "booking_sub_property_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "booking_time_slot",
                columns: table => new
                {
                    booking_time_slot_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    booking_time_slot_subproperty_id = table.Column<int>(type: "int", nullable: false),
                    booking_time_slot_description = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_time_slot_from_time = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_time_slot_to_time = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    booking_time_slot_status = table.Column<int>(type: "int", nullable: false),
                    booking_time_slot_oreder_level = table.Column<int>(type: "int", nullable: false),
                    sabha_id = table.Column<int>(type: "int", nullable: true),
                    booking_time_slot_created_by = table.Column<int>(type: "int", nullable: true),
                    booking_time_slot_updated_by = table.Column<int>(type: "int", nullable: true),
                    booking_time_slot_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    booking_time_slot_updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_time_slot", x => x.booking_time_slot_id);
                    table.ForeignKey(
                        name: "fk_booking_sub_property_booking_time_slot_id",
                        column: x => x.booking_time_slot_subproperty_id,
                        principalTable: "booking_sub_property_type",
                        principalColumn: "booking_sub_property_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_booking_charging_scheme_booking_cs_subproperty_id",
                table: "booking_charging_scheme",
                column: "booking_cs_subproperty_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_sub_property_type_booking_main_property_id",
                table: "booking_sub_property_type",
                column: "booking_main_property_id");

            migrationBuilder.CreateIndex(
                name: "IX_booking_time_slot_booking_time_slot_subproperty_id",
                table: "booking_time_slot",
                column: "booking_time_slot_subproperty_id");

            migrationBuilder.CreateIndex(
                name: "IX_dispute_payment_detail_id",
                table: "dispute",
                column: "payment_detail_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_charging_scheme");

            migrationBuilder.DropTable(
                name: "booking_time_slot");

            migrationBuilder.DropTable(
                name: "dispute");

            migrationBuilder.DropTable(
                name: "logIn_details");

            migrationBuilder.DropTable(
                name: "online_Bookings");

            migrationBuilder.DropTable(
                name: "other_description");

            migrationBuilder.DropTable(
                name: "payment_detail_backup");

            migrationBuilder.DropTable(
                name: "payment_gateways");

            migrationBuilder.DropTable(
                name: "booking_sub_property_type");

            migrationBuilder.DropTable(
                name: "payment_details");

            migrationBuilder.DropTable(
                name: "booking_main_property_type");
        }
    }
}
