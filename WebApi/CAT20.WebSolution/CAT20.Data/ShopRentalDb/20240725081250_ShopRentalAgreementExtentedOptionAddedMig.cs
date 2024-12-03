using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalAgreementExtentedOptionAddedMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "sr_agcr_requestedstatus",
                table: "sr_agreement_change_request",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "sr_agcr_agreement_close_date",
                table: "sr_agreement_change_request",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateOnly>(
                name: "sr_agcr_agreement_extended_end_date",
                table: "sr_agreement_change_request",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sr_agcr_requestType",
                table: "sr_agreement_change_request",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sr_agcr_agreement_extended_end_date",
                table: "sr_agreement_change_request");

            migrationBuilder.DropColumn(
                name: "sr_agcr_requestType",
                table: "sr_agreement_change_request");

            migrationBuilder.AlterColumn<int>(
                name: "sr_agcr_requestedstatus",
                table: "sr_agreement_change_request",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "sr_agcr_agreement_close_date",
                table: "sr_agreement_change_request",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
