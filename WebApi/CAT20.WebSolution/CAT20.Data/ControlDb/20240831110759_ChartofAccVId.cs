﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class ChartofAccVId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cd_s_chart_of_acc_version_id",
                table: "cd_sabha",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cd_s_chart_of_acc_version_id",
                table: "cd_sabha");
        }
    }
}