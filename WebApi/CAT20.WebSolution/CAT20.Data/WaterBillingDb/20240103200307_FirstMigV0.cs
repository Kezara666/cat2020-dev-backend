using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.WaterBillingDb
{
    public partial class FirstMigV0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");
            /*
            migrationBuilder.CreateTable(
                name: "wb_meter_reader_assigns",
                columns: table => new
                {
                    wb_wp_mras_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_mras_reader_id = table.Column<int>(type: "int", nullable: false),
                    wb_mras_subroad_id = table.Column<int>(type: "int", nullable: false),
                    wb_wp_mras_status = table.Column<int>(type: "int", nullable: true),
                    wb_wp_mras_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_wp_mras_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_wp_mras_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_wp_mras_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_meter_reader_assigns", x => x.wb_wp_mras_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_number_sequence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_office_id = table.Column<int>(type: "int", nullable: true),
                    wb_core_no = table.Column<int>(type: "int", nullable: true),
                    wb_application_no = table.Column<int>(type: "int", nullable: true),
                    wb_wp_ns_status = table.Column<int>(type: "int", nullable: true),
                    wb_wp_ns_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_wp_ns_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_wp_ns_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_wp_ns_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_number_sequence", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_payment_category",
                columns: table => new
                {
                    wb_pay_cat_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_pay_cat_desc = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_pay_cat_sabhaId = table.Column<int>(type: "int", nullable: false),
                    wb_pay_cat_status = table.Column<int>(type: "int", nullable: true),
                    wb_pay_cat_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_pay_cat_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_pay_cat_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_pay_cat_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_payment_category", x => x.wb_pay_cat_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_water_projects",
                columns: table => new
                {
                    wb_wp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_wp_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_wp_office_id = table.Column<int>(type: "int", nullable: false),
                    wb_wp_status = table.Column<int>(type: "int", nullable: true),
                    wb_wp_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_wp_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_wp_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_wp_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_water_projects", x => x.wb_wp_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_wp_mainroads",
                columns: table => new
                {
                    wb_wp_mr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_wp_mr_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_wp_mr_sabha_id = table.Column<int>(type: "int", nullable: false),
                    wb_wp_mr_status = table.Column<int>(type: "int", nullable: true),
                    wb_wp_mr_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_wp_mr_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_wp_mr_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_wp_mr_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_wp_mainroads", x => x.wb_wp_mr_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_wp_natures",
                columns: table => new
                {
                    wb_wp_nature_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_wp_nature_type = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_wp_nature_sabha_id = table.Column<int>(type: "int", nullable: false),
                    wb_wp_nature_status = table.Column<int>(type: "int", nullable: true),
                    wb_wp_nature_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_wp_nature_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_wp_nature_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_wp_nature_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_wp_natures", x => x.wb_wp_nature_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_vote_assigns",
                columns: table => new
                {
                    wb_vas_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_vas_water_project_id = table.Column<int>(type: "int", nullable: false),
                    wb_vas_payment_category_Id = table.Column<int>(type: "int", nullable: false),
                    wb_vas_vote = table.Column<int>(type: "int", nullable: false),
                    wb_wp_vas_status = table.Column<int>(type: "int", nullable: true),
                    wb_wp_vas_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_wp_vas_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_wp_vas_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_wp_vas_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_vote_assigns", x => x.wb_vas_id);
                    table.ForeignKey(
                        name: "fk_wp_vt_pcat",
                        column: x => x.wb_vas_payment_category_Id,
                        principalTable: "wb_payment_category",
                        principalColumn: "wb_pay_cat_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_waterproject_gndivisions",
                columns: table => new
                {
                    wb_waterprojet_id = table.Column<int>(type: "int", nullable: false),
                    wb_wp_ex_gnd_id = table.Column<int>(type: "int", nullable: false),
                    wb_wp_gnd_id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    wb_wp_gnd_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_wp_gnd_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_wp_gnd_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_wp_gnd_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_waterproject_gndivisions", x => new { x.wb_waterprojet_id, x.wb_wp_ex_gnd_id });
                    table.ForeignKey(
                        name: "fk_wp_gnd_wp_project_id",
                        column: x => x.wb_waterprojet_id,
                        principalTable: "wb_water_projects",
                        principalColumn: "wb_wp_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_wp_subroads",
                columns: table => new
                {
                    wb_wp_sr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_wp_sr_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_wp_sr_main_id = table.Column<int>(type: "int", nullable: false),
                    wb_wp_sr_water_project_id = table.Column<int>(type: "int", nullable: false),
                    wb_wp_sr_status = table.Column<int>(type: "int", nullable: true),
                    wb_wp_sr_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_wp_sr_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_wp_sr_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_wp_sr_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_wp_subroads", x => x.wb_wp_sr_id);
                    table.ForeignKey(
                        name: "fk_wp_subroad_wp_mainRoad",
                        column: x => x.wb_wp_sr_main_id,
                        principalTable: "wb_wp_mainroads",
                        principalColumn: "wb_wp_mr_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_wp_subroad_wp_project_id",
                        column: x => x.wb_wp_sr_water_project_id,
                        principalTable: "wb_water_projects",
                        principalColumn: "wb_wp_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "water_projects_natures_assign",
                columns: table => new
                {
                    NaturesId = table.Column<int>(type: "int", nullable: false),
                    WaterProjectsId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_water_projects_natures_assign", x => new { x.NaturesId, x.WaterProjectsId });
                    table.ForeignKey(
                        name: "FK_water_projects_natures_assign_wb_water_projects_WaterProject~",
                        column: x => x.WaterProjectsId,
                        principalTable: "wb_water_projects",
                        principalColumn: "wb_wp_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_water_projects_natures_assign_wb_wp_natures_NaturesId",
                        column: x => x.NaturesId,
                        principalTable: "wb_wp_natures",
                        principalColumn: "wb_wp_nature_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_non_meter_fix_charges",
                columns: table => new
                {
                    wb_nmfc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_nmfc_wp_id = table.Column<int>(type: "int", nullable: false),
                    wb_nmfc_nature_id = table.Column<int>(type: "int", nullable: false),
                    wb_nmfc_fixed_charge = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    wb_nmfc_status = table.Column<int>(type: "int", nullable: true),
                    wb_nmfc_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_nmfc_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_nmfc_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_nmfc_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_non_meter_fix_charges", x => x.wb_nmfc_id);
                    table.ForeignKey(
                        name: "fk_wp_nmfc_wp_nature",
                        column: x => x.wb_nmfc_nature_id,
                        principalTable: "wb_wp_natures",
                        principalColumn: "wb_wp_nature_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_water_tariffs",
                columns: table => new
                {
                    wb_wt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_wt_wp_id = table.Column<int>(type: "int", nullable: false),
                    wb_wt_nature_id = table.Column<int>(type: "int", nullable: true),
                    wb_wt_range_start = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    wb_wt_range_end = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    wb_wt_unit_price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    wb_wt_fixed_charge = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    wb_wt_status = table.Column<int>(type: "int", nullable: true),
                    wb_wt_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_wt_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_wt_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_wt_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_water_tariffs", x => x.wb_wt_id);
                    table.ForeignKey(
                        name: "fk_wp_tariff_wp_nature",
                        column: x => x.wb_wt_nature_id,
                        principalTable: "wb_wp_natures",
                        principalColumn: "wb_wp_nature_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_application_for_connections",
                columns: table => new
                {
                    wb_afc_application_no = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_afc_partner_id = table.Column<int>(type: "int", nullable: false),
                    wb_afc_billing_id = table.Column<int>(type: "int", nullable: false),
                    wb_afc_req_nature_id = table.Column<int>(type: "int", nullable: false),
                    wb_afc_req_connection_id = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_afc_is_approved = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    wb_afc_rejt_cmt = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_afc_approved_by = table.Column<int>(type: "int", nullable: true),
                    wb_afc_subroad_id = table.Column<int>(type: "int", nullable: false),
                    wb_afc_conn_type = table.Column<int>(type: "int", nullable: true),
                    wb_afc_status = table.Column<int>(type: "int", nullable: true),
                    wb_afc_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_afc_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    wb_afc_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_afc_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_application_for_connections", x => x.wb_afc_application_no);
                    table.ForeignKey(
                        name: "fk_wb_afc_req_nature_id",
                        column: x => x.wb_afc_req_nature_id,
                        principalTable: "wb_wp_natures",
                        principalColumn: "wb_wp_nature_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_wp_afc_subroad_id",
                        column: x => x.wb_afc_subroad_id,
                        principalTable: "wb_wp_subroads",
                        principalColumn: "wb_wp_sr_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_meter_connection_info",
                columns: table => new
                {
                    wb_wp_mci_conn_id = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_mci_conn_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_mci_meter_no = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_mci_subroad_id = table.Column<int>(type: "int", nullable: false),
                    wb_mci_is_assign = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    wb_mci_order_no = table.Column<int>(type: "int", nullable: false),
                    wb_wp_mci_status = table.Column<int>(type: "int", nullable: true),
                    wb_wp_mci_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_wp_mci_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_wp_mci_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_wp_mci_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_meter_connection_info", x => x.wb_wp_mci_conn_id);
                    table.ForeignKey(
                        name: "fk_wp_meter_connectInfo_wp_subRoad",
                        column: x => x.wb_mci_subroad_id,
                        principalTable: "wb_wp_subroads",
                        principalColumn: "wb_wp_sr_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_water_connections",
                columns: table => new
                {
                    wb_wc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_wc_connection_id = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_wc_partner_id = table.Column<int>(type: "int", nullable: false),
                    wb_wc_billing_id = table.Column<int>(type: "int", nullable: false),
                    wb_wc_subroad_id = table.Column<int>(type: "int", nullable: false),
                    wb_wc_office_id = table.Column<int>(type: "int", nullable: false),
                    wb_wc_install_date = table.Column<DateOnly>(type: "date", nullable: false),
                    wb_wc_active_sataus = table.Column<int>(type: "int", nullable: false),
                    wb_wc_active_nature_id = table.Column<int>(type: "int", nullable: false),
                    wb_wc_status_change_request = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    wb_wc_nature_change_request = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    wb_wc_status = table.Column<int>(type: "int", nullable: true),
                    wb_wc_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_wc_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_wc_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_wc_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_water_connections", x => x.wb_wc_id);
                    table.ForeignKey(
                        name: "fk_wb_wc_active_nature_id",
                        column: x => x.wb_wc_active_nature_id,
                        principalTable: "wb_wp_natures",
                        principalColumn: "wb_wp_nature_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_wp_wc_subroad_id",
                        column: x => x.wb_wc_subroad_id,
                        principalTable: "wb_wp_subroads",
                        principalColumn: "wb_wp_sr_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_application_for_connections_documents",
                columns: table => new
                {
                    wb_afc_doc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_afc_doc_type = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_doc_uri = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_afc_doc_application_no = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_afc_doc_status = table.Column<int>(type: "int", nullable: true),
                    wb_afc_doc_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_afc_doc_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_afc_doc_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_afc_doc_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_application_for_connections_documents", x => x.wb_afc_doc_id);
                    table.ForeignKey(
                        name: "fk_wp_doc_afwc_id",
                        column: x => x.wb_afc_doc_application_no,
                        principalTable: "wb_application_for_connections",
                        principalColumn: "wb_afc_application_no",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");
            */
            migrationBuilder.CreateTable(
                name: "wb_balances",
                columns: table => new
                {
                    wb_bal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_bal_wc_primary_id = table.Column<int>(type: "int", nullable: false),
                    wb_bal_bar_code = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_invoice_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_year = table.Column<int>(type: "int", nullable: false),
                    wb_bal_month = table.Column<int>(type: "int", nullable: false),
                    wb_bal_from_date = table.Column<DateOnly>(type: "date", nullable: false),
                    wb_bal_to_date = table.Column<DateOnly>(type: "date", nullable: true),
                    wb_bal_bill_process_date = table.Column<DateOnly>(type: "date", nullable: false),
                    wb_bal_meter_no = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_prev_meter_reading = table.Column<int>(type: "int", nullable: false),
                    wb_bal_this_month_meter_reading = table.Column<int>(type: "int", nullable: true),
                    wb_bal_reading_date = table.Column<DateOnly>(type: "date", nullable: true),
                    wb_bal_print_arrears = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    wb_bal_water_charge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_fix_charge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_vat_rate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_vat_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_this_month_charges = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_total_due = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_metercondition = table.Column<int>(name: "wb_bal_meter-condition", type: "int", nullable: true),
                    wb_bal_additional_type = table.Column<int>(type: "int", nullable: true),
                    wb_bal_additional_charge = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_ontime_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_late_paid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_payments = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    wb_bal_is_completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    wb_bal_is_filled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    wb_bal_is_processed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    wb_bal_no_of_payments = table.Column<int>(type: "int", nullable: false),
                    wb_bal_cal_string = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_print_last_bill_year_month = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_print_billing_details = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_bal_print_balance_b_f = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    wb_bal_print_last_month_payments = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    wb_bal_num_print = table.Column<int>(type: "int", nullable: true),
                    wb_bal_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_bal_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_bal_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_bal_updated_by = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<DateTime>(type: "timestamp(6)", rowVersion: true, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_balances", x => x.wb_bal_id);
                    table.ForeignKey(
                        name: "fk_bal_wc_id",
                        column: x => x.wb_bal_wc_primary_id,
                        principalTable: "wb_water_connections",
                        principalColumn: "wb_wc_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");
            /*
                        migrationBuilder.CreateTable(
                            name: "wb_connection_audit_log",
                            columns: table => new
                            {
                                wb_atl_id = table.Column<int>(type: "int", nullable: false)
                                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                                wb_atl_wc_id = table.Column<int>(type: "int", nullable: false),
                                wb_atl_action = table.Column<int>(type: "int", nullable: false),
                                wb_atl_time_stamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                                wb_action_by = table.Column<int>(type: "int", nullable: true),
                                wb_entity_type = table.Column<int>(type: "int", nullable: false)
                            },
                            constraints: table =>
                            {
                                table.PrimaryKey("PK_wb_connection_audit_log", x => x.wb_atl_id);
                                table.ForeignKey(
                                    name: "fk_atl_wc_id",
                                    column: x => x.wb_atl_wc_id,
                                    principalTable: "wb_water_connections",
                                    principalColumn: "wb_wc_id",
                                    onDelete: ReferentialAction.Cascade);
                            })
                            .Annotation("MySql:CharSet", "utf8mb4")
                            .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

                        */
            migrationBuilder.CreateTable(
                name: "wb_open_balance_info",
                columns: table => new
                {
                    wb_opn_bal_info_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_opn_bal_info_wcon_id = table.Column<int>(type: "int", nullable: false),
                    wb_opn_bal_month = table.Column<int>(type: "int", nullable: false),
                    wb_opn_bal_year = table.Column<int>(type: "int", nullable: false),
                    wb_opn_bal_info_ly_arrears = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    wb_opn_bal_monthly = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    wb_opn_bal_info_lm_reading = table.Column<int>(type: "int", nullable: false),
                    wb_opn_bal_bal_id_for_last_year_arrears = table.Column<int>(type: "int", nullable: true),
                    wb_opn_bal_bal_id_for_current_year_bal = table.Column<int>(type: "int", nullable: false),
                    wb_opn_bal_info_status = table.Column<int>(type: "int", nullable: true),
                    wb_opn_bal_info_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_opn_bal_info_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_opn_bal_info_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_opn_bal_info_updated_by = table.Column<int>(type: "int", nullable: true),
                    wb_opn_bal_is_processed = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_open_balance_info", x => x.wb_opn_bal_info_id);
                    table.ForeignKey(
                        name: "FK_wb_open_balance_info_wb_water_connections_wb_opn_bal_info_wc~",
                        column: x => x.wb_opn_bal_info_wcon_id,
                        principalTable: "wb_water_connections",
                        principalColumn: "wb_wc_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            /*
            migrationBuilder.CreateTable(
                name: "wb_wb_documents",
                columns: table => new
                {
                    wb_doc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_doc_type = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_doc_uri = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_doc_connection_id = table.Column<int>(type: "int", nullable: true),
                    wb_doc_status = table.Column<int>(type: "int", nullable: true),
                    wb_doc_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_doc_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_doc_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_doc_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_wb_documents", x => x.wb_doc_id);
                    table.ForeignKey(
                        name: "fk_wp_doc_conn_id",
                        column: x => x.wb_doc_connection_id,
                        principalTable: "wb_water_connections",
                        principalColumn: "wb_wc_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_wc_connection_status_log",
                columns: table => new
                {
                    wb_wc_slog_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_wc_slog_conn_satus = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    wb_wc_slog_comment = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_slog_action = table.Column<int>(type: "int", nullable: true),
                    wb_slog_action_by = table.Column<int>(type: "int", nullable: true),
                    wb_slog_action_note = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_slog_activated_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    wb_wc_con_id = table.Column<int>(type: "int", nullable: false),
                    wb_slog_status = table.Column<int>(type: "int", nullable: true),
                    wb_slog_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_slog_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_slog_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_slog_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_wc_connection_status_log", x => x.wb_wc_slog_id);
                    table.ForeignKey(
                        name: "fk_wp_wc_slog_id",
                        column: x => x.wb_wc_con_id,
                        principalTable: "wb_water_connections",
                        principalColumn: "wb_wc_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            migrationBuilder.CreateTable(
                name: "wb_wc_nature_log",
                columns: table => new
                {
                    wb_wc_nlog_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_nlog_nature_id = table.Column<int>(type: "int", nullable: false),
                    wb_wc_nlog_comment = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_nlog_action = table.Column<int>(type: "int", nullable: true),
                    wb_nlog_action_by = table.Column<int>(type: "int", nullable: true),
                    wb_slog_action_note = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_nlog_activated_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    wb_wc_con_id = table.Column<int>(type: "int", nullable: false),
                    wb_nlog_status = table.Column<int>(type: "int", nullable: true),
                    wb_nlog_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    wb_nlog_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_nlog_created_by = table.Column<int>(type: "int", nullable: true),
                    wb_nlog_updated_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_wc_nature_log", x => x.wb_wc_nlog_id);
                    table.ForeignKey(
                        name: "FK_wb_wc_nature_log_wb_wp_natures_wb_nlog_nature_id",
                        column: x => x.wb_nlog_nature_id,
                        principalTable: "wb_wp_natures",
                        principalColumn: "wb_wp_nature_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_wp_wc_nlog_id",
                        column: x => x.wb_wc_con_id,
                        principalTable: "wb_water_connections",
                        principalColumn: "wb_wc_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");
            */
            migrationBuilder.CreateTable(
                name: "wb_open_balance_approval_status",
                columns: table => new
                {
                    wb_obli_aprl_sts_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    wb_obli_aprl_status = table.Column<int>(type: "int", nullable: false),
                    OpnBalInfoId = table.Column<int>(type: "int", nullable: true),
                    wb_obli_aprl_sts_comment = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_sinhala_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    wb_opn_bal_info_updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    wb_opn_bal_info_created_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wb_open_balance_approval_status", x => x.wb_obli_aprl_sts_id);
                    table.ForeignKey(
                        name: "fk_opn_bal_info_aprl_sts_id",
                        column: x => x.OpnBalInfoId,
                        principalTable: "wb_open_balance_info",
                        principalColumn: "wb_opn_bal_info_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_sinhala_ci");

            //migrationBuilder.CreateIndex(
            //    name: "IX_water_projects_natures_assign_WaterProjectsId",
            //    table: "water_projects_natures_assign",
            //    column: "WaterProjectsId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_application_for_connections_wb_afc_req_nature_id",
            //    table: "wb_application_for_connections",
            //    column: "wb_afc_req_nature_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_application_for_connections_wb_afc_subroad_id",
            //    table: "wb_application_for_connections",
            //    column: "wb_afc_subroad_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_application_for_connections_documents_wb_afc_doc_applicat~",
            //    table: "wb_application_for_connections_documents",
            //    column: "wb_afc_doc_application_no");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_application_for_connections_documents_wb_afc_doc_id",
            //    table: "wb_application_for_connections_documents",
            //    column: "wb_afc_doc_id",
            //    unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_wb_balances_wb_bal_wc_primary_id_wb_bal_year_wb_bal_month",
                table: "wb_balances",
                columns: new[] { "wb_bal_wc_primary_id", "wb_bal_year", "wb_bal_month" },
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_connection_audit_log_wb_atl_wc_id",
            //    table: "wb_connection_audit_log",
            //    column: "wb_atl_wc_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_meter_connection_info_wb_mci_subroad_id",
            //    table: "wb_meter_connection_info",
            //    column: "wb_mci_subroad_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_meter_connection_info_wb_wp_mci_conn_id",
            //    table: "wb_meter_connection_info",
            //    column: "wb_wp_mci_conn_id",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_non_meter_fix_charges_wb_nmfc_nature_id_wb_nmfc_wp_id",
            //    table: "wb_non_meter_fix_charges",
            //    columns: new[] { "wb_nmfc_nature_id", "wb_nmfc_wp_id" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_number_sequence_wb_office_id",
            //    table: "wb_number_sequence",
            //    column: "wb_office_id",
            //    unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_wb_open_balance_approval_status_OpnBalInfoId",
                table: "wb_open_balance_approval_status",
                column: "OpnBalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_wb_open_balance_info_wb_opn_bal_info_wcon_id",
                table: "wb_open_balance_info",
                column: "wb_opn_bal_info_wcon_id",
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_vote_assigns_wb_vas_payment_category_Id",
            //    table: "wb_vote_assigns",
            //    column: "wb_vas_payment_category_Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_vote_assigns_wb_vas_water_project_id_wb_vas_payment_categ~",
            //    table: "wb_vote_assigns",
            //    columns: new[] { "wb_vas_water_project_id", "wb_vas_payment_category_Id" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_water_connections_wb_wc_active_nature_id",
            //    table: "wb_water_connections",
            //    column: "wb_wc_active_nature_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_water_connections_wb_wc_office_id",
            //    table: "wb_water_connections",
            //    column: "wb_wc_office_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_water_connections_wb_wc_subroad_id",
            //    table: "wb_water_connections",
            //    column: "wb_wc_subroad_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_water_projects_wb_wp_office_id_wb_wp_name",
            //    table: "wb_water_projects",
            //    columns: new[] { "wb_wp_office_id", "wb_wp_name" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_water_tariffs_wb_wt_nature_id",
            //    table: "wb_water_tariffs",
            //    column: "wb_wt_nature_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_wb_documents_wb_doc_connection_id",
            //    table: "wb_wb_documents",
            //    column: "wb_doc_connection_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_wc_connection_status_log_wb_wc_con_id",
            //    table: "wb_wc_connection_status_log",
            //    column: "wb_wc_con_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_wc_nature_log_wb_nlog_nature_id",
            //    table: "wb_wc_nature_log",
            //    column: "wb_nlog_nature_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_wc_nature_log_wb_wc_con_id",
            //    table: "wb_wc_nature_log",
            //    column: "wb_wc_con_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_wp_mainroads_wb_wp_mr_name_wb_wp_mr_sabha_id",
            //    table: "wb_wp_mainroads",
            //    columns: new[] { "wb_wp_mr_name", "wb_wp_mr_sabha_id" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_wp_natures_wb_wp_nature_type_wb_wp_nature_sabha_id",
            //    table: "wb_wp_natures",
            //    columns: new[] { "wb_wp_nature_type", "wb_wp_nature_sabha_id" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_wp_subroads_wb_wp_sr_main_id",
            //    table: "wb_wp_subroads",
            //    column: "wb_wp_sr_main_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_wp_subroads_wb_wp_sr_name_wb_wp_sr_main_id",
            //    table: "wb_wp_subroads",
            //    columns: new[] { "wb_wp_sr_name", "wb_wp_sr_main_id" },
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_wb_wp_subroads_wb_wp_sr_water_project_id",
            //    table: "wb_wp_subroads",
            //    column: "wb_wp_sr_water_project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "water_projects_natures_assign");

            //migrationBuilder.DropTable(
            //    name: "wb_application_for_connections_documents");

            //migrationBuilder.DropTable(
            //    name: "wb_balances");

            //migrationBuilder.DropTable(
            //    name: "wb_connection_audit_log");

            //migrationBuilder.DropTable(
            //    name: "wb_meter_connection_info");

            //migrationBuilder.DropTable(
            //    name: "wb_meter_reader_assigns");

            //migrationBuilder.DropTable(
            //    name: "wb_non_meter_fix_charges");

            //migrationBuilder.DropTable(
            //    name: "wb_number_sequence");

            //migrationBuilder.DropTable(
            //    name: "wb_open_balance_approval_status");

            //migrationBuilder.DropTable(
            //    name: "wb_vote_assigns");

            //migrationBuilder.DropTable(
            //    name: "wb_water_tariffs");

            //migrationBuilder.DropTable(
            //    name: "wb_waterproject_gndivisions");

            //migrationBuilder.DropTable(
            //    name: "wb_wb_documents");

            //migrationBuilder.DropTable(
            //    name: "wb_wc_connection_status_log");

            //migrationBuilder.DropTable(
            //    name: "wb_wc_nature_log");

            //migrationBuilder.DropTable(
            //    name: "wb_application_for_connections");

            //migrationBuilder.DropTable(
            //    name: "wb_open_balance_info");

            //migrationBuilder.DropTable(
            //    name: "wb_payment_category");

            //migrationBuilder.DropTable(
            //    name: "wb_water_connections");

            //migrationBuilder.DropTable(
            //    name: "wb_wp_natures");

            //migrationBuilder.DropTable(
            //    name: "wb_wp_subroads");

            //migrationBuilder.DropTable(
            //    name: "wb_wp_mainroads");

            //migrationBuilder.DropTable(
            //    name: "wb_water_projects");
        }
    }
}
