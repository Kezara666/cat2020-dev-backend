using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "wb_alt_active_nature_id",
                table: "wb_connection_audit_log",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "wb_alt_active_sataus",
                table: "wb_connection_audit_log",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "wb_alt_billing_id",
                table: "wb_connection_audit_log",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "wb_alt_office_id",
                table: "wb_connection_audit_log",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "wb_alt_partner_id",
                table: "wb_connection_audit_log",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "wb_alt_subroad_id",
                table: "wb_connection_audit_log",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "wb_afc_req_connection_id",
                table: "wb_application_for_connections",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.AddColumn<bool>(
                name: "wb_afc_req_only_billing_change",
                table: "wb_application_for_connections",
                type: "tinyint(1)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "wb_alt_active_nature_id",
                table: "wb_connection_audit_log");

            migrationBuilder.DropColumn(
                name: "wb_alt_active_sataus",
                table: "wb_connection_audit_log");

            migrationBuilder.DropColumn(
                name: "wb_alt_billing_id",
                table: "wb_connection_audit_log");

            migrationBuilder.DropColumn(
                name: "wb_alt_office_id",
                table: "wb_connection_audit_log");

            migrationBuilder.DropColumn(
                name: "wb_alt_partner_id",
                table: "wb_connection_audit_log");

            migrationBuilder.DropColumn(
                name: "wb_alt_subroad_id",
                table: "wb_connection_audit_log");

            migrationBuilder.DropColumn(
                name: "wb_afc_req_only_billing_change",
                table: "wb_application_for_connections");

            migrationBuilder.AlterColumn<string>(
                name: "wb_afc_req_connection_id",
                table: "wb_application_for_connections",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                collation: "utf8mb4_sinhala_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
