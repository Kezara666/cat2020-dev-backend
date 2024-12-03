using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopTableAgreementStartDateEndDateDataTypeModifiedMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "sr_shop_agreement_start_date",
                table: "sr_shop",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "sr_shop_agreement_end_date",
                table: "sr_shop",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "sr_shop_agreement_close_date",
                table: "sr_shop",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "sr_agcr_agreement_close_date",
                table: "sr_agreement_change_request",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "sr_shop_agreement_start_date",
                table: "sr_shop",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "sr_shop_agreement_end_date",
                table: "sr_shop",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "sr_shop_agreement_close_date",
                table: "sr_shop",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "sr_agcr_agreement_close_date",
                table: "sr_agreement_change_request",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
