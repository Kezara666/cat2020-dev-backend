using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_wb_water_connections_wb_wc_connection_id",
                table: "wb_water_connections",
                column: "wb_wc_connection_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_wb_water_connections_wb_meter_connection_info_wb_wc_connecti~",
                table: "wb_water_connections",
                column: "wb_wc_connection_id",
                principalTable: "wb_meter_connection_info",
                principalColumn: "wb_wp_mci_conn_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wb_water_connections_wb_meter_connection_info_wb_wc_connecti~",
                table: "wb_water_connections");

            migrationBuilder.DropIndex(
                name: "IX_wb_water_connections_wb_wc_connection_id",
                table: "wb_water_connections");
        }
    }
}
