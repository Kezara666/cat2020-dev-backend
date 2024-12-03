using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.AssessmentTaxDb
{
    public partial class FirstMigV26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "assmt_property_type_next_year_quarter_rate",
                table: "assmt_property_types",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_property_type_next_year_warrant_rate",
                table: "assmt_property_types",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "assmt_next_year_description_id",
                table: "assmt_assessments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assmt_next_year_property_type_id",
                table: "assmt_assessments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "assmt_parent_assessment_id",
                table: "assmt_assessments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assmt_next_year_allocation_amount",
                table: "assmt_allocations",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "assmt_next_year_allocation_description",
                table: "assmt_allocations",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_sinhala_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "assmt_amalgamation_assessment",
                columns: table => new
                {
                    assmt_amalgamation_assmt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_id = table.Column<int>(type: "int", nullable: true),
                    k_form_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_amalgamation_assessment", x => x.assmt_amalgamation_assmt_id);
                    table.ForeignKey(
                        name: "fk_amalgamation_assmt_id",
                        column: x => x.assmt_id,
                        principalTable: "assmt_assessments",
                        principalColumn: "assmt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_amalgamation_subdivision",
                columns: table => new
                {
                    assmt_amalgamation_assmt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sabha_id = table.Column<int>(type: "int", nullable: false),
                    parent_assmt_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_amalgamation_subdivision", x => x.assmt_amalgamation_assmt_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_next_year_q1",
                columns: table => new
                {
                    assmt_nq1_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_nq1_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_nq1_assmt_balance_id = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_next_year_q1", x => x.assmt_nq1_id);
                    table.ForeignKey(
                        name: "FK_assmt_next_year_q1_assmt_balances_assmt_nq1_assmt_balance_id",
                        column: x => x.assmt_nq1_assmt_balance_id,
                        principalTable: "assmt_balances",
                        principalColumn: "assmt_bal_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_next_year_q2",
                columns: table => new
                {
                    assmt_nq2_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_nq2_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_nq2_assmt_balance_id = table.Column<int>(type: "int", nullable: false),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_next_year_q2", x => x.assmt_nq2_id);
                    table.ForeignKey(
                        name: "FK_assmt_next_year_q2_assmt_balances_assmt_nq2_assmt_balance_id",
                        column: x => x.assmt_nq2_assmt_balance_id,
                        principalTable: "assmt_balances",
                        principalColumn: "assmt_bal_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_next_year_q3",
                columns: table => new
                {
                    assmt_nq3_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_nq3_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_nq3_assmt_balance_id = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_next_year_q3", x => x.assmt_nq3_id);
                    table.ForeignKey(
                        name: "FK_assmt_next_year_q3_assmt_balances_assmt_nq3_assmt_balance_id",
                        column: x => x.assmt_nq3_assmt_balance_id,
                        principalTable: "assmt_balances",
                        principalColumn: "assmt_bal_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "assmt_next_year_q4",
                columns: table => new
                {
                    assmt_nq4_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    assmt_nq4_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    assmt_nq4_assmt_balance_id = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assmt_next_year_q4", x => x.assmt_nq4_id);
                    table.ForeignKey(
                        name: "FK_assmt_next_year_q4_assmt_balances_assmt_nq4_assmt_balance_id",
                        column: x => x.assmt_nq4_assmt_balance_id,
                        principalTable: "assmt_balances",
                        principalColumn: "assmt_bal_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "property_types_logs",
                columns: table => new
                {
                    _property_type_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PropertyTypeId = table.Column<int>(type: "int(11)", nullable: false),
                    property_type_name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    property_type_no = table.Column<int>(type: "int(11)", nullable: false),
                    property_type_quarter_rate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    assmt_property_type_warrant_rate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    property_type_change_filed = table.Column<int>(type: "int(11)", nullable: true),
                    date_from = table.Column<DateOnly>(type: "date", nullable: true),
                    date_to = table.Column<DateOnly>(type: "date", nullable: true),
                    assmt_property_type_created_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    assmt_property_type_created_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_property_types_logs", x => x._property_type_id);
                    table.ForeignKey(
                        name: "fk_property_type_logs",
                        column: x => x.PropertyTypeId,
                        principalTable: "assmt_property_types",
                        principalColumn: "assmt_property_type_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "amalgamation",
                columns: table => new
                {
                    amalgamation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    amalgamation_subdivision_id = table.Column<int>(type: "int", nullable: true),
                    k_form_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amalgamation", x => x.amalgamation_id);
                    table.ForeignKey(
                        name: "fk_amalgamation_subdivision_id",
                        column: x => x.amalgamation_subdivision_id,
                        principalTable: "assmt_amalgamation_subdivision",
                        principalColumn: "assmt_amalgamation_assmt_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "amalgamation_subdivision_actions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    amalgamation_subdivision_id = table.Column<int>(type: "int", nullable: true),
                    action_state = table.Column<int>(type: "int", nullable: false),
                    action_by = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    action_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    system_action_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amalgamation_subdivision_actions", x => x.id);
                    table.ForeignKey(
                        name: "FK_amalgamation_subdivision_actions_assmt_amalgamation_subdivis~",
                        column: x => x.amalgamation_subdivision_id,
                        principalTable: "assmt_amalgamation_subdivision",
                        principalColumn: "assmt_amalgamation_assmt_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "amalgamation_subdivision_documents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    amalgamation_subdivision_id = table.Column<int>(type: "int", nullable: true),
                    doc_type = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uri = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    row_status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amalgamation_subdivision_documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_amalgamation_subdivision_documents_assmt_amalgamation_subdiv~",
                        column: x => x.amalgamation_subdivision_id,
                        principalTable: "assmt_amalgamation_subdivision",
                        principalColumn: "assmt_amalgamation_assmt_id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateIndex(
                name: "IX_amalgamation_amalgamation_subdivision_id",
                table: "amalgamation",
                column: "amalgamation_subdivision_id");

            migrationBuilder.CreateIndex(
                name: "IX_amalgamation_subdivision_actions_amalgamation_subdivision_id",
                table: "amalgamation_subdivision_actions",
                column: "amalgamation_subdivision_id");

            migrationBuilder.CreateIndex(
                name: "IX_amalgamation_subdivision_documents_amalgamation_subdivision_~",
                table: "amalgamation_subdivision_documents",
                column: "amalgamation_subdivision_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_amalgamation_assessment_assmt_id",
                table: "assmt_amalgamation_assessment",
                column: "assmt_id");

            migrationBuilder.CreateIndex(
                name: "IX_assmt_next_year_q1_assmt_nq1_assmt_balance_id",
                table: "assmt_next_year_q1",
                column: "assmt_nq1_assmt_balance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_next_year_q2_assmt_nq2_assmt_balance_id",
                table: "assmt_next_year_q2",
                column: "assmt_nq2_assmt_balance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_next_year_q3_assmt_nq3_assmt_balance_id",
                table: "assmt_next_year_q3",
                column: "assmt_nq3_assmt_balance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assmt_next_year_q4_assmt_nq4_assmt_balance_id",
                table: "assmt_next_year_q4",
                column: "assmt_nq4_assmt_balance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_property_types_logs_PropertyTypeId",
                table: "property_types_logs",
                column: "PropertyTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "amalgamation");

            migrationBuilder.DropTable(
                name: "amalgamation_subdivision_actions");

            migrationBuilder.DropTable(
                name: "amalgamation_subdivision_documents");

            migrationBuilder.DropTable(
                name: "assmt_amalgamation_assessment");

            migrationBuilder.DropTable(
                name: "assmt_next_year_q1");

            migrationBuilder.DropTable(
                name: "assmt_next_year_q2");

            migrationBuilder.DropTable(
                name: "assmt_next_year_q3");

            migrationBuilder.DropTable(
                name: "assmt_next_year_q4");

            migrationBuilder.DropTable(
                name: "property_types_logs");

            migrationBuilder.DropTable(
                name: "assmt_amalgamation_subdivision");

            migrationBuilder.DropColumn(
                name: "assmt_property_type_next_year_quarter_rate",
                table: "assmt_property_types");

            migrationBuilder.DropColumn(
                name: "assmt_property_type_next_year_warrant_rate",
                table: "assmt_property_types");

            migrationBuilder.DropColumn(
                name: "assmt_next_year_description_id",
                table: "assmt_assessments");

            migrationBuilder.DropColumn(
                name: "assmt_next_year_property_type_id",
                table: "assmt_assessments");

            migrationBuilder.DropColumn(
                name: "assmt_parent_assessment_id",
                table: "assmt_assessments");

            migrationBuilder.DropColumn(
                name: "assmt_next_year_allocation_amount",
                table: "assmt_allocations");

            migrationBuilder.DropColumn(
                name: "assmt_next_year_allocation_description",
                table: "assmt_allocations");
        }
    }
}
