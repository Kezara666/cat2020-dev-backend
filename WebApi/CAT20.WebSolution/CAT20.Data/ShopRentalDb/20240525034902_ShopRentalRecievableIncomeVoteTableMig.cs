using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ShopRentalDb
{
    public partial class ShopRentalRecievableIncomeVoteTableMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sr_bal_updated_at",
                table: "sr_shopRental_vote_assign",
                newName: "sr_vote_updated_at");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_ly_ty_service_charge_arreas",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_paid_ly_ty_service_charge_arreas",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.AddColumn<decimal>(
                name: "sr_bal_ty_ly_overpayment",
                table: "sr_shopRental_balance",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                defaultValueSql: "'0'");

            migrationBuilder.CreateTable(
                name: "sr_shopRental_receivable_income_vote_assign",
                columns: table => new
                {
                    sr_ri_vote_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sr_ri_vote_property_id = table.Column<int>(type: "int", nullable: false),
                    sr_ri_vote_shop_id = table.Column<int>(type: "int", nullable: false),
                    sr_ri_vote_property_rental_id = table.Column<int>(type: "int", nullable: false),
                    sr_ri_vote_last_year_arreas_amount_id = table.Column<int>(type: "int", nullable: false),
                    sr_ri_vote_this_year_arreas_amount_id = table.Column<int>(type: "int", nullable: false),
                    sr_ri_vote_last_year_fine_id = table.Column<int>(type: "int", nullable: false),
                    sr_ri_vote_this_year_fine_id = table.Column<int>(type: "int", nullable: false),
                    sr_ri_vote_service_charge_arreas_amount_id = table.Column<int>(type: "int", nullable: false),
                    sr_ri_vote_service_charge_id = table.Column<int>(type: "int", nullable: false),
                    sr_ri_vote_over_payment_id = table.Column<int>(type: "int", nullable: false),
                    sr_ri_vote_status = table.Column<int>(type: "int", nullable: true),
                    sr_ri_vote_office_id = table.Column<int>(type: "int", nullable: true),
                    sr_ri_vote_sabha_id = table.Column<int>(type: "int", nullable: true),
                    sr_ri_vote_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    sr_ri_vote_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sr_ri_vote_created_by = table.Column<int>(type: "int", nullable: true),
                    sr_ri_vote_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_shopRental_receivable_income_vote_assign", x => x.sr_ri_vote_id);
                    table.ForeignKey(
                        name: "sr_ri_vote_FK_property",
                        column: x => x.sr_ri_vote_property_id,
                        principalTable: "sr_property",
                        principalColumn: "sr_property_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sr_ri_vote_Fk_shop",
                        column: x => x.sr_ri_vote_shop_id,
                        principalTable: "sr_shop",
                        principalColumn: "sr_shop_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_sr_shopRental_receivable_income_vote_assign_sr_ri_vote_prope~",
                table: "sr_shopRental_receivable_income_vote_assign",
                column: "sr_ri_vote_property_id");

            migrationBuilder.CreateIndex(
                name: "IX_sr_shopRental_receivable_income_vote_assign_sr_ri_vote_shop_~",
                table: "sr_shopRental_receivable_income_vote_assign",
                column: "sr_ri_vote_shop_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_shopRental_receivable_income_vote_assign");

            migrationBuilder.DropColumn(
                name: "sr_bal_ly_ty_service_charge_arreas",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_paid_ly_ty_service_charge_arreas",
                table: "sr_shopRental_balance");

            migrationBuilder.DropColumn(
                name: "sr_bal_ty_ly_overpayment",
                table: "sr_shopRental_balance");

            migrationBuilder.RenameColumn(
                name: "sr_vote_updated_at",
                table: "sr_shopRental_vote_assign",
                newName: "sr_bal_updated_at");
        }
    }
}
