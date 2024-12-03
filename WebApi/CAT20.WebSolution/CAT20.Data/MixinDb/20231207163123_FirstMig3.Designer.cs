﻿// <auto-generated />
using System;
using CAT20.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CAT20.Data.MixinDb
{
    [DbContext(typeof(MixinDbContext))]
    [Migration("20231207163123_FirstMig3")]
    partial class FirstMig3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_sinhala_ci")
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("CAT20.Core.Models.Mixin.Banking", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("BankedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("banked_date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int")
                        .HasColumnName("office_id");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "OrderId" }, "banking_order_id_UN")
                        .IsUnique();

                    b.ToTable("banking", (string)null);
                });

            modelBuilder.Entity("CAT20.Core.Models.Mixin.MixinCancelOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("ApprovalComment")
                        .HasColumnType("text")
                        .HasColumnName("approvalcomment");

                    b.Property<int?>("ApprovedBy")
                        .HasColumnType("int")
                        .HasColumnName("approved_by");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<int>("MixinOrderId")
                        .HasColumnType("int")
                        .HasColumnName("mixin_order_id");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("reason");

                    b.Property<int>("SessionId")
                        .HasColumnType("int")
                        .HasColumnName("ir_session_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("MixinOrderId")
                        .IsUnique();

                    b.HasIndex(new[] { "MixinOrderId" }, "mixin_cancel_order_FK");

                    b.HasIndex(new[] { "CreatedBy" }, "mixin_cancel_order_FK_1");

                    b.ToTable("mixin_cancel_order", (string)null);
                });

            modelBuilder.Entity("CAT20.Core.Models.Mixin.MixinOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("AccountDetailId")
                        .HasColumnType("int")
                        .HasColumnName("account_detail_id");

                    b.Property<int?>("AppCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("app_category_id");

                    b.Property<int?>("AssessmentId")
                        .HasColumnType("int")
                        .HasColumnName("assessment_id");

                    b.Property<int?>("BusinessId")
                        .HasColumnType("int")
                        .HasColumnName("business_id");

                    b.Property<int?>("BusinessTaxId")
                        .HasColumnType("int")
                        .HasColumnName("business_tax_id");

                    b.Property<int?>("CashierId")
                        .HasColumnType("int")
                        .HasColumnName("cashier_id");

                    b.Property<string>("ChequeBankName")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("cheque_bank_name");

                    b.Property<DateTime?>("ChequeDate")
                        .HasColumnType("datetime")
                        .HasColumnName("cheque_date");

                    b.Property<string>("ChequeNumber")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("cheque_number");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int")
                        .HasColumnName("created_by");

                    b.Property<string>("CustomerMobileNumber")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("customer_mobile_number");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("customer_name");

                    b.Property<string>("CustomerNicNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("customer_nic_number");

                    b.Property<decimal?>("DiscountAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("discount_amount");

                    b.Property<decimal?>("DiscountRate")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("discount_rate");

                    b.Property<int>("GnDivisionId")
                        .HasColumnType("int")
                        .HasColumnName("gn_division_id");

                    b.Property<int?>("OfficeId")
                        .HasColumnType("int");

                    b.Property<int?>("PartnerId")
                        .HasColumnType("int")
                        .HasColumnName("res_partner_id");

                    b.Property<int>("PaymentMethodId")
                        .HasColumnType("int")
                        .HasColumnName("res_payment_method_id");

                    b.Property<int>("SessionId")
                        .HasColumnType("int")
                        .HasColumnName("ir_session_id");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int")
                        .HasColumnName("shop_id");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("state")
                        .HasDefaultValueSql("1");

                    b.Property<int?>("TaxTypeId")
                        .HasColumnType("int")
                        .HasColumnName("tax_type_id");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("total_amount");

                    b.Property<int>("TradeLicenseStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("trade_license_status")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at");

                    b.Property<int?>("WaterConnectionId")
                        .HasColumnType("int")
                        .HasColumnName("water_connection_id");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentId");

                    b.HasIndex("ShopId");

                    b.HasIndex("State");

                    b.HasIndex("WaterConnectionId");

                    b.HasIndex(new[] { "GnDivisionId" }, "mixin_order_FK_1");

                    b.HasIndex(new[] { "SessionId" }, "mixin_order_FK_2");

                    b.HasIndex(new[] { "PartnerId" }, "mixin_order_FK_3");

                    b.HasIndex(new[] { "PaymentMethodId" }, "mixin_order_FK_4");

                    b.ToTable("mixin_order", (string)null);
                });

            modelBuilder.Entity("CAT20.Core.Models.Mixin.MixinOrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<decimal?>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("CustomVoteName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("custom_vote_name");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<int>("MixinOrderId")
                        .HasColumnType("int")
                        .HasColumnName("mixin_order_id");

                    b.Property<int>("MixinVoteAssignmentDetailId")
                        .HasColumnType("int")
                        .HasColumnName("mixin_vote_assignment_detail_id");

                    b.Property<decimal?>("PaymentNbtAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("payment_nbt_amount");

                    b.Property<int?>("PaymentNbtId")
                        .HasColumnType("int")
                        .HasColumnName("res_payment_nbt_id");

                    b.Property<decimal?>("PaymentVatAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("payment_vat_amount");

                    b.Property<int?>("PaymentVatId")
                        .HasColumnType("int")
                        .HasColumnName("res_payment_vat_id");

                    b.Property<decimal?>("StampAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("stamp_amount");

                    b.Property<decimal?>("TotalAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("total_amount");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("updated_at");

                    b.Property<int>("VoteOrBal")
                        .HasColumnType("int")
                        .HasColumnName("vote_or_bal");

                    b.HasKey("Id");

                    b.HasIndex("MixinOrderId");

                    b.HasIndex(new[] { "MixinVoteAssignmentDetailId" }, "mixin_order_line_FK_4");

                    b.ToTable("mixin_order_line", (string)null);
                });

            modelBuilder.Entity("CAT20.Core.Models.Mixin.VoteAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("BankAccountId")
                        .HasColumnType("int")
                        .HasColumnName("bank_account_id");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime")
                        .HasColumnName("date_created");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime")
                        .HasColumnName("date_modified");

                    b.Property<sbyte>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("isactive")
                        .HasDefaultValueSql("'1'");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int")
                        .HasColumnName("office_id");

                    b.Property<int>("SabhaId")
                        .HasColumnType("int")
                        .HasColumnName("sabha_id");

                    b.Property<int>("VoteId")
                        .HasColumnType("int")
                        .HasColumnName("vote_id");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "OfficeId" }, "mixin_vote_assignment_FK_1");

                    b.HasIndex(new[] { "BankAccountId" }, "mixin_vote_assignment_FK_2");

                    b.HasIndex(new[] { "VoteId", "OfficeId", "IsActive" }, "mixin_vote_assignment_UN")
                        .IsUnique();

                    b.ToTable("vote_assignment", (string)null);
                });

            modelBuilder.Entity("CAT20.Core.Models.Mixin.VoteAssignmentDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("CustomVoteName")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("custom_vote_name");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime")
                        .HasColumnName("date_created");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime")
                        .HasColumnName("date_modified");

                    b.Property<sbyte>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("isactive")
                        .HasDefaultValueSql("'1'");

                    b.Property<int>("VoteAssignmentId")
                        .HasColumnType("int")
                        .HasColumnName("mixin_vote_assignment_id");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "VoteAssignmentId", "CustomVoteName", "IsActive" }, "mixin_vote_assignment_details_UN")
                        .IsUnique();

                    b.ToTable("vote_assignment_details", (string)null);
                });

            modelBuilder.Entity("CAT20.Core.Models.Mixin.MixinCancelOrder", b =>
                {
                    b.HasOne("CAT20.Core.Models.Mixin.MixinOrder", "MixinOrder")
                        .WithOne("MixinCancelOrder")
                        .HasForeignKey("CAT20.Core.Models.Mixin.MixinCancelOrder", "MixinOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MixinOrder");
                });

            modelBuilder.Entity("CAT20.Core.Models.Mixin.MixinOrderLine", b =>
                {
                    b.HasOne("CAT20.Core.Models.Mixin.MixinOrder", null)
                        .WithMany("MixinOrderLine")
                        .HasForeignKey("MixinOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CAT20.Core.Models.Mixin.VoteAssignmentDetails", b =>
                {
                    b.HasOne("CAT20.Core.Models.Mixin.VoteAssignment", "voteAssignment")
                        .WithMany("VoteAssignmentDetails")
                        .HasForeignKey("VoteAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("mixin_vote_assignment_details_FK");

                    b.Navigation("voteAssignment");
                });

            modelBuilder.Entity("CAT20.Core.Models.Mixin.MixinOrder", b =>
                {
                    b.Navigation("MixinCancelOrder")
                        .IsRequired();

                    b.Navigation("MixinOrderLine");
                });

            modelBuilder.Entity("CAT20.Core.Models.Mixin.VoteAssignment", b =>
                {
                    b.Navigation("VoteAssignmentDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
