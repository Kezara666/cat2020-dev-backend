using CAT20.Core.Models.Mixin;
using Microsoft.EntityFrameworkCore;


namespace CAT20.Data
{
    public partial class MixinDbContext : DbContext
    {
        public MixinDbContext()
        {
        }
        public MixinDbContext(DbContextOptions<MixinDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<VoteAssignment> VoteAssignment { get; set; }
        public virtual DbSet<VoteAssignmentDetails> VoteAssignmentDetails { get; set; }
        public virtual DbSet<MixinCancelOrder> MixinCancelOrders { get; set; }
        public virtual DbSet<MixinOrder> MixinOrders { get; set; }
        public virtual DbSet<MixinOrderLine> MixinOrderLines { get; set; }
        public virtual DbSet<Banking> Bankings { get; set; }
        public virtual DbSet<MixinOrderLog> MixinOrderLogs { get; set; }
        public virtual DbSet<MixinOrderLineLog> MixinOrderLineLogs { get; set; }
        //public virtual DbSet<CustomVoteSubLevel1> CustomVoteSubLevel1s { get; set; }
        //public virtual DbSet<CustomVoteSubLevel2> CustomVoteSubLevel2s { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_sinhala_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<VoteAssignment>(entity =>
            {
                entity.ToTable("vote_assignment");

                entity.HasIndex(e => e.OfficeId, "mixin_vote_assignment_FK_1");

                entity.HasIndex(e => e.BankAccountId, "mixin_vote_assignment_FK_2");

                entity.HasIndex(e => new { e.VoteId, e.OfficeId, e.IsActive }, "mixin_vote_assignment_UN")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.BankAccountId).HasColumnName("bank_account_id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasColumnName("date_modified");

                entity.Property(e => e.OfficeId).HasColumnName("office_id");

                entity.Property(e => e.VoteId).HasColumnName("vote_id");

                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");

                entity.Ignore(e => e.office);
                entity.Ignore(e => e.voteDetail);
                entity.Ignore(e => e.accountDetail);
            });

            modelBuilder.Entity<VoteAssignmentDetails>(entity =>
            {
                entity.ToTable("vote_assignment_details");

                entity.HasIndex(e => new { e.VoteAssignmentId, e.CustomVoteName, e.IsActive }, "mixin_vote_assignment_details_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.IsActive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.CustomVoteName)
                    .IsRequired()
                    .HasColumnName("custom_vote_name");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasColumnName("date_modified");

                entity.Property(e => e.VoteAssignmentId).HasColumnName("mixin_vote_assignment_id");
                entity.Property(e => e.Depth).HasDefaultValue(0).HasColumnName("depth");
                entity.Property(e => e.Code).HasColumnName("code");
                entity.Property(e => e.ParentId).HasColumnName("parent_id");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                //entity.HasIndex(e => e.ParentId);
                entity.Property(e => e.IsSubLevel).HasDefaultValue(false).HasColumnName("is_sub_level");

                entity.HasOne(d => d.voteAssignment)
                    .WithMany(p => p.VoteAssignmentDetails)
                    .HasForeignKey(d => d.VoteAssignmentId)
                    .HasConstraintName("mixin_vote_assignment_details_FK");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
            });

            //modelBuilder.Entity<CustomVoteSubLevel1>(entity =>
            //{
            //    entity.ToTable("custom_vote_sublevel1");

            //    entity.Property(e => e.Id)
            //        .HasColumnName("cv_sbl1_id");

            //    entity.Property(e => e.Description)
            //        .IsRequired()
            //        .HasColumnName("cv_sbl1_description");

            //    entity.Property(e => e.CustomVoteId).HasColumnName("cv_sbl1_custom_vote_id").HasDefaultValue(1); 
            //    entity.Property(e => e.Status).HasColumnName("cv_sbl1_status").HasDefaultValue(1);
            //    entity.Property(e => e.CreatedBy).HasColumnName("cv_sbl1_created_by");
            //    entity.Property(e => e.UpdatedBy).HasColumnName("cv_sbl1_updated_by");
            //    entity.Property(e => e.CreatedAt).HasColumnName("cv_sbl1_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            //    entity.Property(e => e.UpdatedAt).HasColumnName("cv_sbl1_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();

            //    entity.HasOne(d => d.CustomVote)
            //        .WithMany(p => p.CustomVoteSubLevel1s)
            //        .HasForeignKey(d => d.CustomVoteId);
            //});

            //modelBuilder.Entity<CustomVoteSubLevel2>(entity =>
            //{
            //    entity.ToTable("custom_vote_sublevel2");

            //    entity.Property(e => e.Id)
            //        .HasColumnName("cv_sbl2_id");

            //    entity.Property(e => e.Description)
            //        .IsRequired()
            //        .HasColumnName("cv_sbl2_description");
            //    entity.Property(e => e.CustomVoteSubLevel1Id).HasColumnName("cv_sbl1_custom_vote_level1id").HasDefaultValue(1);
            //    entity.Property(e => e.Status).HasColumnName("cv_sbl2_status").HasDefaultValue(1); 
            //    entity.Property(e => e.CreatedBy).HasColumnName("cv_sbl2_created_by");
            //    entity.Property(e => e.UpdatedBy).HasColumnName("cv_sbl2_updated_by");
            //    entity.Property(e => e.CreatedAt).HasColumnName("cv_sbl2_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            //    entity.Property(e => e.UpdatedAt).HasColumnName("cv_sbl2_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();

            //    entity.HasOne(d => d.CustomVoteSubLevel1)
            //        .WithMany(p => p.CustomVoteSubLevel2s)
            //        .HasForeignKey(d => d.CustomVoteSubLevel1Id);
            //});

            modelBuilder.Entity<MixinCancelOrder>(entity =>
            {
                entity.ToTable("mixin_cancel_order");

                entity.HasIndex(e => e.MixinOrderId, "mixin_cancel_order_FK");

                entity.HasIndex(e => e.CreatedBy, "mixin_cancel_order_FK_1");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.SessionId).HasColumnName("ir_session_id");

                entity.Property(e => e.MixinOrderId).HasColumnName("mixin_order_id");

                entity.Property(e => e.Reason)
                    .HasColumnType("text")
                    .HasColumnName("reason");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                //entity.HasOne(d => d.MixinOrder)
                //    .WithOne(p => p.MixinCancelOrder)
                //    .HasForeignKey(d => d.MixinOrderId)
                //    .HasConstraintName("mixin_cancel_order_FK");

                entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
                entity.Property(e => e.ApprovalComment)
                    .HasColumnType("text")
                    .HasColumnName("approvalcomment");
            });

            modelBuilder.Entity<MixinOrder>(entity =>
            {
                entity.ToTable("mixin_order");

                entity.HasIndex(e => e.GnDivisionId, "mixin_order_FK_1");

                entity.HasIndex(e => e.SessionId, "mixin_order_FK_2");

                entity.HasIndex(e => e.PartnerId, "mixin_order_FK_3");

                entity.HasIndex(e => e.PaymentMethodId, "mixin_order_FK_4");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CashierId).HasColumnName("cashier_id");

                entity.Property(e => e.AssessmentId).HasColumnName("assessment_id");
                entity.Property(e => e.WaterConnectionId).HasColumnName("water_connection_id");
                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.HasIndex(e => e.AssessmentId);
                entity.HasIndex(e => e.WaterConnectionId);
                entity.HasIndex(e => e.ShopId);

                entity.Property(e => e.ChequeBankName).HasMaxLength(255)
                .HasColumnName("cheque_bank_name");

                entity.Property(e => e.ChequeDate)
                    .HasColumnType("datetime")
                    .HasColumnName("cheque_date");

                entity.Property(e => e.ChequeNumber)
                    .HasMaxLength(255)
                    .HasColumnName("cheque_number");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.CustomerMobileNumber)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("customer_mobile_number");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("customer_name");

                entity.Property(e => e.CustomerNicNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("customer_nic_number");

                entity.Property(e => e.GnDivisionId).HasColumnName("gn_division_id");

                entity.Property(e => e.SessionId).HasColumnName("ir_session_id");
                entity.Property(e => e.AccountDetailId).HasColumnName("account_detail_id");
                entity.Property(e => e.PartnerId).HasDefaultValue(60).HasColumnName("res_partner_id");
                entity.Property(e => e.EmployeeId).HasDefaultValue(1).HasColumnName("hrm_employee_id");
                entity.Property(e => e.PaymentMethodId).HasColumnName("res_payment_method_id");
                entity.Property(e => e.BusinessId).HasColumnName("business_id");
                entity.Property(e => e.BusinessTaxId).HasColumnName("business_tax_id");
                entity.Property(e => e.AppCategoryId).HasColumnName("app_category_id");
                entity.Property(e => e.TaxTypeId).HasColumnName("tax_type_id");

                entity.Property(e => e.State)
                .HasColumnName("state")
                .HasColumnType("int").IsRequired()
                .HasDefaultValueSql("1");

                entity.HasIndex(e => e.State);

                entity.Property(e => e.TotalAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("total_amount");

                entity.Property(e => e.DiscountRate)
                    .HasPrecision(10, 2)
                    .HasColumnName("discount_rate");

                entity.Property(e => e.DiscountAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("discount_amount");

                entity.Property(e => e.TradeLicenseStatus)
               .HasColumnName("trade_license_status")
               .HasColumnType("int").IsRequired()
               .HasDefaultValueSql("0");


                entity.Ignore(t => t.GnDivisions);
                entity.Ignore(t => t.Office);
                entity.Ignore(t => t.UserDetail);
                entity.Ignore(t => t.Cashier);
                entity.Ignore(t => t.Partner);
                entity.Ignore(t => t.AccountDetail);

                entity.Property(e => e.PaymentDetailId).HasColumnName("online_payment_det_id");




                //just for reporting 

                entity.Property(e => e.AssmtBalByExcessDeduction).HasColumnName("assmt_bal_by_excess_deduction");

                //mandotory field 

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedAt)
                  .HasColumnType("datetime")
                  .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


                entity.Property(e => e.UpdatedAt)
                 .HasColumnType("datetime")
                 .HasColumnName("updated_at");

            });

            modelBuilder.Entity<MixinOrderLine>(entity =>
            {
                entity.ToTable("mixin_order_line");

                entity.HasIndex(e => e.MixinVoteAssignmentDetailId, "mixin_order_line_FK_4");

                //entity.HasIndex(e => e.MixinOrderId, "mixin_order_line_FK_3");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2)
                    .HasColumnName("amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CustomVoteName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("custom_vote_name");

                entity.Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.MixinOrderId).HasColumnName("mixin_order_id");
                entity.Property(e => e.VoteOrBal).HasColumnName("vote_or_bal");

                entity.Property(e => e.MixinVoteAssignmentDetailId).HasColumnName("mixin_vote_assignment_detail_id");

                entity.Property(e => e.PaymentNbtAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("payment_nbt_amount");

                entity.Property(e => e.PaymentVatAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("payment_vat_amount");

                entity.Property(e => e.PaymentNbtId).HasColumnName("res_payment_nbt_id");

                entity.Property(e => e.PaymentVatId).HasColumnName("res_payment_vat_id");
                entity.Property(e => e.VotePaymentTypeId).HasColumnName("vote_ptype_id");

                entity.Property(e => e.StampAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("stamp_amount");

                entity.Property(e => e.TotalAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("total_amount");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.MixinOrder)
                   .WithMany(p => p.MixinOrderLine)
                   .HasForeignKey(d => d.MixinOrderId)
                   .HasConstraintName("mixin_order_line_FK");

                entity.Property(e => e.VoteDetailId).HasColumnName("vote_detail_id");
                entity.Property(e => e.ClassificationId).HasColumnName("classification_id");

                entity.Ignore(t => t.PaymentVat);
                entity.Ignore(t => t.PaymentNbt);
                entity.Ignore(t => t.VoteAssignmentDetails);
                entity.Ignore(t => t.MixinOrder);


                //just for reporting 

                entity.Property(e => e.AssmtGrossAmount).HasColumnName("assmt_gross_amount");
                entity.Property(e => e.AssmtDiscountAmount).HasColumnName("assmt_discount_amount");
                entity.Property(e => e.AssmtDiscountRate).HasColumnName("assmt_discount_rate");



            });

            modelBuilder.Entity<MixinOrderLog>(entity =>
            {
                entity.ToTable("mixin_order_log");

                entity.HasIndex(e => e.GnDivisionId, "mixin_order_log_FK_1");

                entity.HasIndex(e => e.SessionId, "mixin_order_log_FK_2");

                entity.HasIndex(e => e.PartnerId, "mixin_order_log_FK_3");

                entity.HasIndex(e => e.PaymentMethodId, "mixin_order_log_FK_4");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CashierId).HasColumnName("cashier_id");

                entity.Property(e => e.AssessmentId).HasColumnName("assessment_id");
                entity.Property(e => e.WaterConnectionId).HasColumnName("water_connection_id");
                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.HasIndex(e => e.AssessmentId);
                entity.HasIndex(e => e.WaterConnectionId);
                entity.HasIndex(e => e.ShopId);

                entity.Property(e => e.ChequeBankName).HasMaxLength(255)
                .HasColumnName("cheque_bank_name");

                entity.Property(e => e.ChequeDate)
                    .HasColumnType("datetime")
                    .HasColumnName("cheque_date");

                entity.Property(e => e.ChequeNumber)
                    .HasMaxLength(255)
                    .HasColumnName("cheque_number");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("code");



                entity.Property(e => e.CustomerMobileNumber)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("customer_mobile_number");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("customer_name");

                entity.Property(e => e.CustomerNicNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("customer_nic_number");

                entity.Property(e => e.GnDivisionId).HasColumnName("gn_division_id");

                entity.Property(e => e.SessionId).HasColumnName("ir_session_id");
                entity.Property(e => e.AccountDetailId).HasColumnName("account_detail_id");


                entity.Property(e => e.PartnerId).HasDefaultValue(60).HasColumnName("res_partner_id");
                entity.Property(e => e.EmployeeId).HasDefaultValue(1).HasColumnName("hrm_employee_id");

                entity.Property(e => e.PaymentMethodId).HasColumnName("res_payment_method_id");

                entity.Property(e => e.BusinessId).HasColumnName("business_id");
                entity.Property(e => e.BusinessTaxId).HasColumnName("business_tax_id");
                entity.Property(e => e.AppCategoryId).HasColumnName("app_category_id");
                entity.Property(e => e.TaxTypeId).HasColumnName("tax_type_id");

                entity.Property(e => e.State)
                .HasColumnName("state")
                .HasColumnType("int").IsRequired()
                .HasDefaultValueSql("1");

                entity.HasIndex(e => e.State);

                entity.Property(e => e.TotalAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("total_amount");

                entity.Property(e => e.DiscountRate)
                    .HasPrecision(10, 2)
                    .HasColumnName("discount_rate");

                entity.Property(e => e.DiscountAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("discount_amount");

                entity.Property(e => e.TradeLicenseStatus)
               .HasColumnName("trade_license_status")
               .HasColumnType("int").IsRequired()
               .HasDefaultValueSql("0");


                entity.Ignore(t => t.GnDivisions);
                entity.Ignore(t => t.Office);
                entity.Ignore(t => t.UserDetail);
                entity.Ignore(t => t.Cashier);
                entity.Ignore(t => t.Partner);
                entity.Ignore(t => t.AccountDetail);

                entity.Property(e => e.PaymentDetailId).HasColumnName("online_payment_det_id");




                //just for reporting 

                entity.Property(e => e.AssmtBalByExcessDeduction).HasColumnName("assmt_bal_by_excess_deduction");

                //mandotory field 

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedAt)
                  .HasColumnType("datetime")
                  .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


                entity.Property(e => e.UpdatedAt)
                 .HasColumnType("datetime")
                 .HasColumnName("updated_at");

            });

            modelBuilder.Entity<MixinOrderLineLog>(entity =>
            {
                entity.ToTable("mixin_order_line_log");

                entity.HasIndex(e => e.MixinVoteAssignmentDetailId, "mixin_order_line_log_FK_4");

                //entity.HasIndex(e => e.MixinOrderId, "mixin_order_line_FK_3");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2)
                    .HasColumnName("amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CustomVoteName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("custom_vote_name");

                entity.Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.MixinOrderId).HasColumnName("mixin_order_id");
                entity.Property(e => e.VoteOrBal).HasColumnName("vote_or_bal");

                entity.Property(e => e.MixinVoteAssignmentDetailId).HasColumnName("mixin_vote_assignment_detail_id");

                entity.Property(e => e.PaymentNbtAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("payment_nbt_amount");

                entity.Property(e => e.PaymentVatAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("payment_vat_amount");

                entity.Property(e => e.PaymentNbtId).HasColumnName("res_payment_nbt_id");

                entity.Property(e => e.PaymentVatId).HasColumnName("res_payment_vat_id");
                entity.Property(e => e.VotePaymentTypeId).HasColumnName("vote_ptype_id");

                entity.Property(e => e.StampAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("stamp_amount");

                entity.Property(e => e.TotalAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("total_amount");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.MixinOrderLog)
                   .WithMany(p => p.MixinOrderLineLog)
                   .HasForeignKey(d => d.MixinOrderId)
                   .HasConstraintName("mixin_order_line_log_FK");

                entity.Property(e => e.VoteDetailId).HasColumnName("vote_detail_id");
                entity.Property(e => e.ClassificationId).HasColumnName("classification_id");

                entity.Ignore(t => t.PaymentVat);
                entity.Ignore(t => t.PaymentNbt);
                entity.Ignore(t => t.VoteAssignmentDetails);
                entity.Ignore(t => t.MixinOrderLog);


                //just for reporting 

                entity.Property(e => e.AssmtGrossAmount).HasColumnName("assmt_gross_amount");
                entity.Property(e => e.AssmtDiscountAmount).HasColumnName("assmt_discount_amount");
                entity.Property(e => e.AssmtDiscountRate).HasColumnName("assmt_discount_rate");



            });

            modelBuilder.Entity<Banking>(entity =>
            {
                entity.ToTable("banking");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.BankedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("banked_date");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.HasIndex(e => new { e.OrderId }, "banking_order_id_UN")
                    .IsUnique();
                //entity.Ignore(t => t.MixinOrder);
            });






            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}