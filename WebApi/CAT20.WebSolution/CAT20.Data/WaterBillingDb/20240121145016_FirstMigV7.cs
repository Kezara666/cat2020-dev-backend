using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoOfConcels",
                table: "wb_balances",
                newName: "wb_bal_no_of_cancels");

            migrationBuilder.AlterColumn<int>(
                name: "wb_bal_no_of_payments",
                table: "wb_balances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "wb_bal_no_of_cancels",
                table: "wb_balances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "wb_bal_no_of_cancels",
                table: "wb_balances",
                newName: "NoOfConcels");

            migrationBuilder.AlterColumn<int>(
                name: "wb_bal_no_of_payments",
                table: "wb_balances",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfConcels",
                table: "wb_balances",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
