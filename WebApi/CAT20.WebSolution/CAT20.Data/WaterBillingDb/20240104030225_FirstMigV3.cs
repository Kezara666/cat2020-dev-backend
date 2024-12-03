using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV3 : Migration
    {
        //protected override void Up(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.RenameColumn(
        //        name: "ApprovedAt",
        //        table: "wb_application_for_connections",
        //        newName: "wb_afc_approved_at");
        //}

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.RenameColumn(
        //        name: "wb_afc_approved_at",
        //        table: "wb_application_for_connections",
        //        newName: "ApprovedAt");
        //}

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<DateTime>(
            //    name: "wb_afc_approved_at",
            //    table: "wb_application_for_connections",
            //    nullable: true); // Adjust the data type and nullability as needed
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "wb_afc_approved_at",
            //    table: "wb_application_for_connections");
        }
    }
}
