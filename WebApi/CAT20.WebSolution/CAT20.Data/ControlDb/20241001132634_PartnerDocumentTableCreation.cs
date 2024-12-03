using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAT20.Data.ControlDb
{
    public partial class PartnerDocumentTableCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartnerDocument_res_partner_PartnerId",
                table: "PartnerDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartnerDocument",
                table: "PartnerDocument");

            migrationBuilder.RenameTable(
                name: "PartnerDocument",
                newName: "partner_documents");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "partner_documents",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "partner_documents",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "partner_documents",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "partner_documents",
                newName: "partner_id");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "partner_documents",
                newName: "file_name");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "partner_documents",
                newName: "document_type");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "partner_documents",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_PartnerDocument_PartnerId",
                table: "partner_documents",
                newName: "IX_partner_documents_partner_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "partner_documents",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "partner_documents",
                type: "datetime",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "partner_documents",
                type: "tinyint(1)",
                nullable: false,
                defaultValueSql: "'1'");

            migrationBuilder.AddPrimaryKey(
                name: "PK_partner_documents",
                table: "partner_documents",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_partner_documents_res_partner_partner_id",
                table: "partner_documents",
                column: "partner_id",
                principalTable: "res_partner",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partner_documents_res_partner_partner_id",
                table: "partner_documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_partner_documents",
                table: "partner_documents");

            migrationBuilder.DropColumn(
                name: "status",
                table: "partner_documents");

            migrationBuilder.RenameTable(
                name: "partner_documents",
                newName: "PartnerDocument");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "PartnerDocument",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PartnerDocument",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "PartnerDocument",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "partner_id",
                table: "PartnerDocument",
                newName: "PartnerId");

            migrationBuilder.RenameColumn(
                name: "file_name",
                table: "PartnerDocument",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "document_type",
                table: "PartnerDocument",
                newName: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "PartnerDocument",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_partner_documents_partner_id",
                table: "PartnerDocument",
                newName: "IX_PartnerDocument_PartnerId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "PartnerDocument",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "PartnerDocument",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartnerDocument",
                table: "PartnerDocument",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerDocument_res_partner_PartnerId",
                table: "PartnerDocument",
                column: "PartnerId",
                principalTable: "res_partner",
                principalColumn: "id");
        }
    }
}
