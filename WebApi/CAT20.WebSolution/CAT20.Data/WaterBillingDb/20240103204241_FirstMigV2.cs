using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "wb_bal_mci_conn_no",
                table: "wb_balances",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_sinhala_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "wb_bal_mci_conn_no",
                table: "wb_balances");
        }
    }
}
