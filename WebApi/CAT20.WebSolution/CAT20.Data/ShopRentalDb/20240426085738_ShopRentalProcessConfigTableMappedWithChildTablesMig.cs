using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalProcessConfigTableMappedWithChildTablesMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sr_process_configuration_sr_processConfig_sabha_id_sr_proces~",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_cal_type",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_rental_payment_date_type",
                table: "sr_process_configuration");

            migrationBuilder.RenameColumn(
                name: "sr_processConfig_fine_rate_type",
                table: "sr_process_configuration",
                newName: "sr_processConfig_rental_payment_date_type_id");

            migrationBuilder.AddColumn<int>(
                name: "sr_processConfig_fine_cal_type_id",
                table: "sr_process_configuration",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_processConfig_fine_charging_method_id",
                table: "sr_process_configuration",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sr_processConfig_fine_rate_type_id",
                table: "sr_process_configuration",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "sr_processConfig_group_name",
                table: "sr_process_configuration",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_sinhala_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sr_fine_cal_type",
                columns: table => new
                {
                    sr_fine_cal_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_fine_cal_type_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_fine_cal_type_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_fine_cal_type", x => x.sr_fine_cal_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_fine_charging_method",
                columns: table => new
                {
                    sr_fine_charging_method_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_fine_charging_method_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_fine_charging_method_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_fine_charging_method", x => x.sr_fine_charging_method_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_fine_rate_type",
                columns: table => new
                {
                    sr_fine_rate_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_fine_rate_type_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_fine_rate_type_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_fine_rate_type", x => x.sr_fine_rate_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "sr_rental_payment_data_type",
                columns: table => new
                {
                    sr_rental_payment_data_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_rental_payment_data_type_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sr_rental_payment_data_type_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_rental_payment_data_type", x => x.sr_rental_payment_data_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_sr_process_configuration_sr_processConfig_fine_cal_type_id",
                table: "sr_process_configuration",
                column: "sr_processConfig_fine_cal_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_process_configuration_sr_processConfig_fine_charging_meth~",
                table: "sr_process_configuration",
                column: "sr_processConfig_fine_charging_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_process_configuration_sr_processConfig_fine_rate_type_id",
                table: "sr_process_configuration",
                column: "sr_processConfig_fine_rate_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_process_configuration_sr_processConfig_rental_payment_dat~",
                table: "sr_process_configuration",
                column: "sr_processConfig_rental_payment_date_type_id");

            migrationBuilder.AddForeignKey(
                name: "sr_processConfig_ibfk_1",
                table: "sr_process_configuration",
                column: "sr_processConfig_fine_rate_type_id",
                principalTable: "sr_fine_rate_type",
                principalColumn: "sr_fine_rate_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "sr_processConfig_ibfk_2",
                table: "sr_process_configuration",
                column: "sr_processConfig_fine_cal_type_id",
                principalTable: "sr_fine_cal_type",
                principalColumn: "sr_fine_cal_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "sr_processConfig_ibfk_3",
                table: "sr_process_configuration",
                column: "sr_processConfig_rental_payment_date_type_id",
                principalTable: "sr_rental_payment_data_type",
                principalColumn: "sr_rental_payment_data_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "sr_processConfig_ibfk_4",
                table: "sr_process_configuration",
                column: "sr_processConfig_fine_charging_method_id",
                principalTable: "sr_fine_charging_method",
                principalColumn: "sr_fine_charging_method_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "sr_processConfig_ibfk_1",
                table: "sr_process_configuration");

            migrationBuilder.DropForeignKey(
                name: "sr_processConfig_ibfk_2",
                table: "sr_process_configuration");

            migrationBuilder.DropForeignKey(
                name: "sr_processConfig_ibfk_3",
                table: "sr_process_configuration");

            migrationBuilder.DropForeignKey(
                name: "sr_processConfig_ibfk_4",
                table: "sr_process_configuration");

            migrationBuilder.DropTable(
                name: "sr_fine_cal_type");

            migrationBuilder.DropTable(
                name: "sr_fine_charging_method");

            migrationBuilder.DropTable(
                name: "sr_fine_rate_type");

            migrationBuilder.DropTable(
                name: "sr_rental_payment_data_type");

            migrationBuilder.DropIndex(
                name: "IX_sr_process_configuration_sr_processConfig_fine_cal_type_id",
                table: "sr_process_configuration");

            migrationBuilder.DropIndex(
                name: "IX_sr_process_configuration_sr_processConfig_fine_charging_meth~",
                table: "sr_process_configuration");

            migrationBuilder.DropIndex(
                name: "IX_sr_process_configuration_sr_processConfig_fine_rate_type_id",
                table: "sr_process_configuration");

            migrationBuilder.DropIndex(
                name: "IX_sr_process_configuration_sr_processConfig_rental_payment_dat~",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_cal_type_id",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_charging_method_id",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_fine_rate_type_id",
                table: "sr_process_configuration");

            migrationBuilder.DropColumn(
                name: "sr_processConfig_group_name",
                table: "sr_process_configuration");

            migrationBuilder.RenameColumn(
                name: "sr_processConfig_rental_payment_date_type_id",
                table: "sr_process_configuration",
                newName: "sr_processConfig_fine_rate_type");

            migrationBuilder.AddColumn<bool>(
                name: "sr_processConfig_fine_cal_type",
                table: "sr_process_configuration",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "sr_processConfig_rental_payment_date_type",
                table: "sr_process_configuration",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_sr_process_configuration_sr_processConfig_sabha_id_sr_proces~",
                table: "sr_process_configuration",
                columns: new[] { "sr_processConfig_sabha_id", "sr_processConfig_fine_rate_type", "sr_processConfig_fine_daily_rate", "sr_processConfig_fine_monthly_rate", "sr_processConfig_fine_1st_month_rate", "sr_processConfig_fine_2nd_month_rate", "sr_processConfig_fine_3rd_month_rate", "sr_processConfig_fine_cal_type", "sr_processConfig_rental_payment_date_type" },
                unique: true);
        }
    }
}
