using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.AssessmentTax.AssessmentBalanceHistory.AssessmentBalanceHistory;
using CAT20.Core.Models.AssessmentTax.Quarter;
using CAT20.Core.Models.FinalAccount;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data
{
    public partial class AssessmentTaxDbContext : DbContext
    {
        public AssessmentTaxDbContext(DbContextOptions<AssessmentTaxDbContext> options) : base(options)
        {
        }


        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<Description> Descriptions { get; set; }
        public virtual DbSet<AssessmentPropertyType> AssessmentPropertyTypes { get; set; }
        public virtual DbSet<PropertyTypesLogs> PropertyTypesLogs { get; set; }
        public virtual DbSet<Assessment> Assessments { get; set; }
        public virtual DbSet<Allocation> Allocations { get; set; }
        public virtual DbSet<NewAllocationRequest> NewAllocationRequests { get; set; }
        public virtual DbSet<AllocationLog> AllocationLogs { get; set; }
        public virtual DbSet<AssessmentTempPartner> AssessmentTempPartners { get; set; }
        public virtual DbSet<AssessmentTempSubPartner> AssessmentTempSubPartners { get; set; }
        public virtual DbSet<VotePaymentType> VotePaymentTypes { get; set; }
        public virtual DbSet<AssessmentVoteAssign> AssessmentVoteAssigns { get; set; }
        public virtual DbSet<AssessmentBalance> AssessmentBalances { get; set; }
        public virtual DbSet<Q1> Q1S { get; set; }
        public virtual DbSet<Q2> Q2S { get; set; }
        public virtual DbSet<Q3> Q3S { get; set; }
        public virtual DbSet<Q4> Q4S { get; set; }


        public virtual DbSet<AssessmentBalancesHistory> AssessmentBalanceHistories { get; set; }
        public virtual DbSet<QH1> QH1S { get; set; }
        public virtual DbSet<QH2> QH2S { get; set; }
        public virtual DbSet<QH3> QH3S { get; set; }
        public virtual DbSet<QH4> QH4S { get; set; }
        public virtual DbSet<AssessmentTransaction> AssessmentTransactions { get; set; }
        public virtual DbSet<AssessmentRates> Rates { get; set; }

        public virtual DbSet<AssessmentProcess> AssessmentProcesses { get; set; }
        public virtual DbSet<AssessmentDescriptionLog> AssessmentDescriptionLogs { get; set; }
        public virtual DbSet<AssessmentPropertyTypeLog> AssessmentPropertyTypeLogs { get; set; }
        public virtual DbSet<AssessmentJournal> AssessmentJournals { get; set; }
        public virtual DbSet<AssessmentAssetsChange> AssessmentAssetsChanges { get; set; }
        public virtual DbSet<AssessmentQuarterReport> AssessmentQuarterReport { get; set; }
        public virtual DbSet<AssessmentQuarterReportLog> AssessmentQuarterReportLogs { get; set; }

        public virtual DbSet<AssessmentBillAdjustment> AssessmentBillAdjustments { get; set; }


        public virtual DbSet<NQ1> NQ1S { get; set; }
        public virtual DbSet<NQ2> NQ2S { get; set; }
        public virtual DbSet<NQ3> NQ3S { get; set; }
        public virtual DbSet<NQ4> NQ4S { get; set; }

        public virtual DbSet<Amalgamation> Amalgamations { get; set; }
        public virtual DbSet<SubDivision> SubDivisions { get; set; }
        public virtual DbSet<AmalgamationAssessment> AmalgamationAssessments { get; set; }

        public virtual DbSet<AmalgamationSubDivision> AmalgamationSubDivision { get; set; }
        public virtual DbSet<AmalgamationSubDivisionDocuments> AmalgamationSubDivisionDocuments { get; set; }
        public virtual DbSet<AmalgamationSubDivisionActions> AmalgamationSubDivisionActions { get; set; }
        public virtual DbSet<AssessmentATD> AssessmentATDs { get; set; }
        public virtual DbSet<AssessmentATDOwnerslog> AssessmentATDOwnerslogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_sinhala_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.ToTable("assmt_wards");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_ward_id");
                entity.Property(e => e.WardName).HasColumnName("assmt_ward_name");
                entity.Property(e => e.WardNo).HasColumnName("assmt_ward_no");
                entity.Property(e => e.WardCode).HasColumnName("assmt_ward_code");
                entity.Property(e => e.OfficeId).HasColumnName("assmt_ward_office_id");
                entity.Property(e => e.SabhaId).HasColumnName("assmt_ward_sabha_id");

                entity.Property(e => e.Status).HasColumnName("assmt_ward_status");
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_ward_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_ward_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_ward_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assmt_ward_updated_at");

                entity.HasIndex(e => new { e.WardNo, e.SabhaId }).IsUnique(true);
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.ToTable("assmt_streets");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_street_id");
                entity.Property(e => e.StreetName).HasColumnName("assmt_street_name");
                entity.Property(e => e.StreetNo).HasColumnName("assmt_street_no");
                entity.Property(e => e.StreetCode).HasColumnName("assmt_street_code");
                entity.Property(e => e.WardId).HasColumnName("assmt_street_ward_id");



                // mandatory fields for entity
                entity.Property(e => e.Status).HasColumnName("assmt_street_status");
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_street_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_street_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_streett_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).ValueGeneratedOnAddOrUpdate().HasColumnType("datetime").HasColumnName("assm_street_updated_at");


                //constraint
                entity.HasIndex(e => new { e.StreetNo, e.WardId }).IsUnique(true);

                entity.HasOne(st => st.Ward)
                        .WithMany(wd => wd.Streets)
                        .HasForeignKey(st => st.WardId)
                        .HasConstraintName("fk_assmt_street_assm_ward");
            });

            modelBuilder.Entity<Description>(entity =>
            {
                entity.ToTable("assmt_descriptions");


                entity.Property(e => e.Id).HasColumnName("assm_des_id");
                entity.Property(e => e.DescriptionNo).HasColumnName("assm_des_no");
                entity.Property(e => e.DescriptionText).HasColumnName("assm_des_text");
                entity.Property(e => e.SabhaId).HasColumnName("assmt_des_sabha_id");

                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnType("int(1)").HasColumnName("assm_des_status");
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_des_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_des_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_des_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assm_des_updated_at");

                //constraint

                entity.HasIndex(e => new { e.DescriptionNo, e.SabhaId }).IsUnique(true);
            });

            modelBuilder.Entity<AssessmentPropertyType>(entity =>
            {
                entity.ToTable("assmt_property_types");

                entity.Property(e => e.Id).HasColumnName("assmt_property_type_id");

                entity.Property(e => e.Id).HasColumnType("int(11)").HasColumnName("assmt_property_type_id");

                entity.Property(e => e.PropertyTypeName).HasColumnName("assmt_property_type_name");
                entity.Property(e => e.PropertyTypeNo).HasColumnName("assmt_property_type_no").HasColumnType("int(11)");

                entity.Property(e => e.Status).HasColumnType("int(11)").HasColumnName("assmt_property_type_status");
                entity.Property(e => e.QuarterRate).HasColumnName("assmt_property_type_quarter_rate");
                entity.Property(e => e.WarrantRate).HasColumnName("assmt_property_type_warrant_rate");
                entity.Property(e => e.SabhaId).HasColumnName("assmt_property_type_sabha_id").HasColumnType("int(11)");

                entity.Property(e => e.NextYearQuarterRate).HasColumnName("assmt_property_type_next_year_quarter_rate");
                entity.Property(e => e.NextYearWarrantRate).HasColumnName("assmt_property_type_next_year_warrant_rate");

                // mandatory fields for entity

                entity.Property(e => e.CreatedBy).HasColumnName("assmt_property_type_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_property_type_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_property_type_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assm_property_type_updated_at");

                // constraint

                entity.HasIndex(e => new { e.PropertyTypeNo, e.SabhaId }).IsUnique(true);
            });

            modelBuilder.Entity<PropertyTypesLogs>(entity =>
            {
                entity.ToTable("property_types_logs");

                entity.Property(e => e.Id).HasColumnName("property_type_log_id");

                entity.Property(e => e.Id).HasColumnType("int(11)").HasColumnName("_property_type_id");

                entity.Property(e => e.PropertyTypeName).HasColumnName("property_type_name");
                entity.Property(e => e.PropertyTypeNo).HasColumnName("property_type_no").HasColumnType("int(11)");

                entity.Property(e => e.ChangeFiled).HasColumnType("int(11)").HasColumnName("property_type_change_filed");
                entity.Property(e => e.QuarterRate).HasColumnName("property_type_quarter_rate");
                entity.Property(e => e.WarrantRate).HasColumnName("assmt_property_type_warrant_rate");

                entity.Property(e => e.DateFrom).HasColumnName("date_from");
                entity.Property(e => e.DateTo).HasColumnName("date_to");

                // mandatory fields for entity

                entity.Property(e => e.CreatedBy).HasColumnName("assmt_property_type_created_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_property_type_created_at");

                // constraint

                entity.HasOne(d => d.AssessmentPropertyType)
                   .WithMany(p => p.PropertyTypesLogs)
                   .HasForeignKey(d => d.PropertyTypeId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_property_type_logs");

            });



            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.ToTable("assmt_assessments");

                entity.HasCharSet("utf8mb4")
                   .UseCollation("utf8mb4_sinhala_ci");

                //entity.HasIndex(e => e.PartnerId, "assmt_partner_id");
                //entity.HasIndex(e => e.SubPartnerId, "assmt_sub_partner_id");

                //entity.HasIndex(e => e.StreetId, "assmt_street_id");
                //entity.HasIndex(e => e.AssessmentPropertyTypeId, "assmt_AssessmentPropertyType_id");

                //entity.HasIndex(e => e.DescriptionId, "assmt_description_text_id");




                entity.Property(e => e.Id).HasColumnName("assmt_id");
                entity.Property(e => e.PartnerId).HasColumnName("assmt_partner_id");
                entity.Property(e => e.SubPartnerId).HasColumnName("assmt_sub_partner_id");
                entity.Property(e => e.StreetId).HasColumnName("assmt_street_id");
                entity.Property(e => e.PropertyTypeId).HasColumnName("assmt_property_type_id");
                entity.Property(e => e.DescriptionId).HasColumnName("assmt_description_id");

                entity.Property(e => e.OrderNo).HasColumnName("assmt_order");
                entity.Property(e => e.AssessmentNo).HasColumnName("assmt_no");
                entity.Property(e => e.AssessmentStatus).HasColumnName("assmt_status");
                entity.Property(e => e.Syn).HasColumnName("assmt_syn");
                entity.Property(e => e.Comment).HasColumnName("assmt_comment");
                entity.Property(e => e.Obsolete).HasColumnName("assmt_obsolete");

                entity.Property(e => e.OfficeId).HasColumnName("assmt_office_id");
                entity.Property(e => e.SabhaId).HasColumnName("assmt_sabha_id");
                entity.Property(e => e.IsWarrant).HasColumnName("assmt_is_warrant");

                //entity.Property(e => e.TempPartnerId).HasColumnName("assmt_temp_partner_id");
                //entity.Property(e => e.TempSubPartnerId).HasColumnName("assmt_temp_sub_partner_id");

                entity.Property(e => e.IsPartnerUpdated).HasColumnName("assmt_is_partner_updated");
                entity.Property(e => e.IsSubPartnerUpdated).HasColumnName("assmt_is_sub_partner_updated");

                entity.Property(e => e.DescriptionChangeRequest).HasColumnName("assmt_has_description_change_request");
                entity.Property(e => e.PropertyTypeChangeRequest).HasColumnName("assmt_has_property_type_change_request");
                entity.Property(e => e.AllocationChangeRequest).HasColumnName("assmt_has_allocation_change_request");
                entity.Property(e => e.DeleteRequest).HasColumnName("assmt_has_delete_request");
                entity.Property(e => e.HasJournalRequest).HasDefaultValue(false).HasColumnName("assmt_has_journal_request");
                entity.Property(e => e.HasAssetsChangeRequest).HasDefaultValue(false).HasColumnName("assmt_has_assets_cng_request");
                entity.Property(e => e.HasBillAdjustmentRequest).HasDefaultValue(false).HasColumnName("assmt_has_bil_adj_request");

                entity.Property(e => e.PropertyAddress).HasColumnName("assmt_property_address");
                entity.Property(e => e.NextYearPropertyTypeId).HasColumnName("assmt_next_year_property_type_id");
                entity.Property(e => e.NextYearDescriptionId).HasColumnName("assmt_next_year_description_id");
                entity.Property(e => e.ParentAssessmentId).HasColumnName("assmt_parent_assessment_id");

                entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("assmt_assmt_created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("assmt_assmt_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("assmt_assmt_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_assmt_updated_by");


                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();


                //many to one relationship

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.Assessments)
                    .HasForeignKey(d => d.StreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_assmt_street_id");

                entity.HasOne(d => d.Description)
                    .WithMany(p => p.Assessments)
                    .HasForeignKey(d => d.DescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_assmt_description_id");

                entity.HasOne(d => d.AssessmentPropertyType)
                    .WithMany(p => p.Assessments)
                    .HasForeignKey(d => d.PropertyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_assmt_property_type");



                //one to one relations

                entity.HasOne(asmt => asmt.Allocation)
                    .WithOne(al => al.Assessment)
                    .HasForeignKey<Allocation>(al => al.AssessmentId);

                entity.HasOne(asmt => asmt.NewAllocationRequest)
                   .WithOne(nal => nal.Assessment)
                   .HasForeignKey<NewAllocationRequest>(nal => nal.AssessmentId);

                entity.HasOne(asmt => asmt.AssessmentTempPartner)
                  .WithOne(tp => tp.Assessment)
                  .HasForeignKey<AssessmentTempPartner>(tp => tp.AssessmentId);

                //entity.HasOne(asmt => asmt.AssessmentTempSubPartner)
                // .WithOne(tsp => tsp.Assessment)
                // .HasForeignKey<AssessmentTempSubPartner>(tsp => tsp.AssessmentId);

            entity.HasMany(asmt => asmt.AssessmentTempSubPartner)
            .WithOne(tsp => tsp.Assessment)
            .HasForeignKey(tsp=>tsp.AssessmentId);

                entity.HasOne(asmt => asmt.AssessmentBalance)
                    .WithOne(bal => bal.Assessment)
                    .HasForeignKey<AssessmentBalance>(bal => bal.AssessmentId)
                    .IsRequired(false);

                //entity.HasOne(bh => bh.AssessmentBalancesHistory)
                //    .WithMany(p => p.QuarterOpeningBalance)
                //    .HasForeignKey(d => d.AssessmentId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("fk_assmt_QopeningBal_Assessment1");


                entity.HasIndex(e => new { e.AssessmentNo, e.StreetId }).IsUnique(true);

                entity.Ignore(e => e.Partner);
                entity.Ignore(e => e.SubPartner);
            });


            modelBuilder.Entity<AmalgamationAssessment>(entity =>
            {
                entity.ToTable("assmt_amalgamation_assessment");

                entity.HasCharSet("utf8mb4")
                  .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("assmt_amalgamation_assmt_id");
                entity.Property(e => e.KFormId).HasColumnName("k_form_id");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_id");


            });

            modelBuilder.Entity<Allocation>(entity =>
            {
                entity.ToTable("assmt_allocations");

                entity.HasCharSet("utf8mb4")
                  .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("assmt_allocation_id");
                entity.Property(e => e.AllocationAmount).HasColumnName("assmt_allocation_amount");
                entity.Property(e => e.ChangedDate).HasColumnName("assmt_allocation_changed_date");
                entity.Property(e => e.AllocationDescription).HasColumnName("assmt_allocation_description");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_allocation_assmt_id");

                entity.Property(e => e.NextYearAllocationAmount).HasColumnName("assmt_next_year_allocation_amount");
                entity.Property(e => e.NextYearAllocationDescription).HasColumnName("assmt_next_year_allocation_description");

                // mandatory fields for entity
                entity.Property(e => e.Status).HasColumnType("int(11)").HasColumnName("assmt_allocation_status");
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_allocation_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_allocation_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_allocation_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assmt_allocation_updated_at");

            });


            modelBuilder.Entity<NewAllocationRequest>(entity =>
            {
                entity.ToTable("assmt_new_allocation_requests");

                entity.HasCharSet("utf8mb4")
                  .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("assmt_new_allocation_req_id");
                entity.Property(e => e.AllocationAmount).HasColumnName("assmt_new_allocation_req_amount");
                entity.Property(e => e.AllocationDescription).HasColumnName("assmt_new_allocation_req_description");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_new_allocation_req_assmt_id");
                entity.Property(e => e.ActivationYear).HasColumnName("assmt_new_allocation_req_act_year");
                entity.Property(e => e.ActivationQuarter).HasColumnName("assmt_new_allocation_req_act_quarter");

                // mandatory fields for entity
                entity.Property(e => e.Status).HasColumnType("int(11)").HasColumnName("assmt_new_allocation_req_status");
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_new_allocation_req_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_new_allocation_req_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_new_allocation_req_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assmt_new_allocation_req_updated_at");

            });


            modelBuilder.Entity<AssessmentTempPartner>(entity =>
            {
                entity.ToTable("assm_temp_partners");

                entity.HasCharSet("utf8mb4")
                 .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("assmt_tmp_ptnr_id");
                entity.Property(e => e.Name).HasColumnName("assmt_tmp_ptnr_name");
                entity.Property(e => e.NICNumber).HasColumnName("assmt_tmp_ptnr_nic");
                entity.Property(e => e.MobileNumber).HasColumnName("assmt_tmp_ptnr_mobile_no");
                entity.Property(e => e.Email).HasColumnName("assmt_tmp_ptnr_email");


                entity.Property(e => e.Street1).HasColumnName("assmt_tmp_ptnr_street1");
                entity.Property(e => e.Street2).HasColumnName("assmt_tmp_ptnr_street2");

                entity.Property(e => e.AssessmentId).HasColumnName("assmt_tmp_ptnr_assmt_id");


                // mandatory fields for entity
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_tmp_ptnr_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_tmp_ptnr_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_tmp_ptnr_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assmt_tmp_ptnr_updated_at");


            });


            modelBuilder.Entity<AssessmentTempSubPartner>(entity =>
            {
                entity.ToTable("assm_temp_sub_partner");
                entity.HasCharSet("utf8mb4")
            .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_tmp_sub_ptnr_id");
                entity.Property(e => e.Name).HasColumnName("assmt_tmp_sub_ptnr_name");
                entity.Property(e => e.NICNumber).HasColumnName("assmt_tmp_sub_ptnr_nic");
                entity.Property(e => e.MobileNumber).HasColumnName("assmt_tmp_sub_ptnr_mobile_no");


                entity.Property(e => e.Street1).HasColumnName("assmt_tmp_sub_ptnr_street1");
                entity.Property(e => e.Street2).HasColumnName("assmt_tmp_ptnr_street2");

                entity.Property(e => e.AssessmentId).HasColumnName("assmt_tmp_sub_ptnr_assmt_id");
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_tmp_sub_ptnr_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_tmp_sub_ptnr_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_tmp_sub_ptnr_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assmt_tmp_sub_ptnr_updated_at");


            });



            modelBuilder.Entity<AssessmentBalance>(entity =>
            {
                entity.ToTable("assmt_balances");

                entity.Property(e => e.Id).HasColumnName("assmt_bal_id");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_bal_assmt_id");
                entity.Property(e => e.Year).HasColumnName("assmt_bal_year");
                entity.Property(e => e.StartDate).HasColumnName("assmt_bal_start_date");

                entity.Property(e => e.ExcessPayment).HasColumnName("assmt_bal_excess_payment");

                entity.Property(e => e.LYArrears).HasColumnName("assmt_bal_ly_arrears");
                entity.Property(e => e.LYWarrant).HasColumnName("assmt_bal_ly_warrant");

                entity.Property(e => e.TYArrears).HasColumnName("assmt_bal_ty_arrears");
                entity.Property(e => e.TYWarrant).HasColumnName("assmt_bal_ty_warrant");

                entity.Property(e => e.AnnualAmount).HasColumnName("assmt_bal_annual_amount");
                entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_bal_by_excess_deduction");
                entity.Property(e => e.Paid).HasColumnName("assmt_bal_paid");
                entity.Property(e => e.NumberOfPayments).HasDefaultValue(0).HasColumnName("assmt_bal_number_of_payments");
                entity.Property(e => e.NumberOfCancels).HasDefaultValue(0).HasColumnName("assmt_bal_number_of_cancels");

                entity.Property(e => e.OverPayment).HasColumnName("assmt_bal_over_payment");

                entity.Property(e => e.DiscountRate).HasColumnName("assmt_bal_discount_rate");
                entity.Property(e => e.Discount).HasColumnName("assmt_bal_discount");
                entity.Property(e => e.CurrentQuarter).HasColumnName("assmt_bal_current_quarter");
                entity.Property(e => e.IsCompleted).HasColumnName("assmt_bal_is_completed");
                entity.Property(e => e.HasTransaction).HasColumnName("assmt_bal_has_transaction");
                entity.Property(e => e.ReportBalance).HasDefaultValue(0m).HasColumnName("assmt_bal_rpt_balance");


                entity.Property(e => e.LYArrearsAdjustment).HasDefaultValue(0m).HasColumnName("assmt_bal_lya_adjusment");
                entity.Property(e => e.LYWarrantAdjustment).HasDefaultValue(0m).HasColumnName("assmt_bal_lyw_adjusment");
                entity.Property(e => e.TYArrearsAdjustment).HasDefaultValue(0m).HasColumnName("assmt_bal_tya_adjusment");
                entity.Property(e => e.TYWarrantAdjustment).HasDefaultValue(0m).HasColumnName("assmt_bal_tyw_adjusment");
                entity.Property(e => e.OverPayAdjustment).HasDefaultValue(0m).HasColumnName("assmt_bal_overpay_adjusment");


                entity.HasOne(bal => bal.Q1)
                   .WithOne(q => q.AssessmentBalance)
                   .HasForeignKey<Q1>(bal => bal.BalanceId);


                entity.HasOne(bal => bal.Q2)
                  .WithOne(q => q.AssessmentBalance)
                  .HasForeignKey<Q2>(bal => bal.BalanceId);

                entity.HasOne(bal => bal.Q3)
                  .WithOne(q => q.AssessmentBalance)
                  .HasForeignKey<Q3>(bal => bal.BalanceId);

                entity.HasOne(bal => bal.Q4)
                  .WithOne(q => q.AssessmentBalance)
                  .HasForeignKey<Q4>(bal => bal.BalanceId);



                entity.HasOne(bal => bal.NQ1)
                  .WithOne(q => q.AssessmentBalance)
                  .HasForeignKey<NQ1>(bal => bal.BalanceId);


                entity.HasOne(bal => bal.NQ2)
                  .WithOne(q => q.AssessmentBalance)
                  .HasForeignKey<NQ2>(bal => bal.BalanceId);

                entity.HasOne(bal => bal.NQ3)
                  .WithOne(q => q.AssessmentBalance)
                  .HasForeignKey<NQ3>(bal => bal.BalanceId);

                entity.HasOne(bal => bal.NQ4)
                  .WithOne(q => q.AssessmentBalance)
                  .HasForeignKey<NQ4>(bal => bal.BalanceId);

                //entity.HasIndex(e => new { e.AssessmentId, e.Year,}).IsUnique(true);

                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("assmt_bal_status");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("assmt_bal_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("assmt_bal_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("assmt_bal_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_bal_updated_by");


                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();


            });



            modelBuilder.Entity<Q1>(entity =>
            {
                entity.ToTable("assmt_q1");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_q1_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_q1_amount");

                entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_q1_by_excess_deduction");
                entity.Property(e => e.Paid).HasColumnName("assmt_q1_paid");
                entity.Property(e => e.Discount).HasColumnName("assmt_q1_discount");
                entity.Property(e => e.Warrant).HasColumnName("assmt_q1_warrant");
                entity.Property(e => e.WarrantMethod).HasColumnName("assmt_q1_warrant_method");

                entity.Property(e => e.StartDate).HasColumnName("assmt_q1_start_date");
                entity.Property(e => e.EndDate).HasColumnName("assmt_q1_end_date");

                entity.Property(e => e.IsCompleted).HasColumnName("assmt_q1_is_completed");
                entity.Property(e => e.IsOver).HasColumnName("assmt_q1_is_over");
                entity.Property(e => e.BalanceId).HasColumnName("assmt_q1_assmt_balance_id");

                entity.Property(e => e.WarrantBy).HasColumnName("assmt_q1_warrant_by");
                entity.Property(e => e.DiscountRate).HasColumnName("assmt_q1_discount_rate");
                entity.Property(e => e.WarrantRate).HasColumnName("assmt_q1_warrant_rate");
                
                entity.Property(e => e.Adjustment).HasDefaultValue(0m).HasColumnName("assmt_q1_adjusment");
                entity.Property(e => e.QReportAdjustment).HasDefaultValue(0m).HasColumnName("assmt_q1_report_adjusment");



                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();


            });

            modelBuilder.Entity<Q2>(entity =>
            {
                entity.ToTable("assmt_q2");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_q2_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_q2_amount");

                entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_q2_by_excess_deduction");
                entity.Property(e => e.Paid).HasColumnName("assmt_q2_paid");
                entity.Property(e => e.Discount).HasColumnName("assmt_q2_discount");
                entity.Property(e => e.Warrant).HasColumnName("assmt_q2_warrant");
                entity.Property(e => e.WarrantMethod).HasColumnName("assmt_q2_warrant_method");

                entity.Property(e => e.StartDate).HasColumnName("assmt_q2_start_date");
                entity.Property(e => e.EndDate).HasColumnName("assmt_q2_end_date");

                entity.Property(e => e.IsCompleted).HasColumnName("assmt_q2_is_completed");
                entity.Property(e => e.IsOver).HasColumnName("assmt_q2_is_over");
                entity.Property(e => e.BalanceId).HasColumnName("assmt_q2_assmt_balance_id");

                entity.Property(e => e.WarrantBy).HasColumnName("assmt_q2_warrant_by");
                entity.Property(e => e.DiscountRate).HasColumnName("assmt_q2_discount_rate");
                entity.Property(e => e.WarrantRate).HasColumnName("assmt_q2_warrant_rate");

                entity.Property(e => e.Adjustment).HasDefaultValue(0m).HasColumnName("assmt_q2_adjusment");
                entity.Property(e => e.QReportAdjustment).HasDefaultValue(0m).HasColumnName("assmt_q2_report_adjusment");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });

            modelBuilder.Entity<Q3>(entity =>
            {
                entity.ToTable("assmt_q3");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_q3_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_q3_amount");

                entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_q3_by_excess_deduction");
                entity.Property(e => e.Paid).HasColumnName("assmt_q3_paid");
                entity.Property(e => e.Discount).HasColumnName("assmt_q3_discount");
                entity.Property(e => e.Warrant).HasColumnName("assmt_q3_warrant");
                entity.Property(e => e.WarrantMethod).HasColumnName("assmt_q3_warrant_method");

                entity.Property(e => e.StartDate).HasColumnName("assmt_q3_start_date");
                entity.Property(e => e.EndDate).HasColumnName("assmt_q3_end_date");

                entity.Property(e => e.IsCompleted).HasColumnName("assmt_q3_is_completed");
                entity.Property(e => e.IsOver).HasColumnName("assmt_q3_is_over");
                entity.Property(e => e.BalanceId).HasColumnName("assmt_q3_assmt_balance_id");

                entity.Property(e => e.WarrantBy).HasColumnName("assmt_q3_warrant_by");
                entity.Property(e => e.DiscountRate).HasColumnName("assmt_q3_discount_rate");
                entity.Property(e => e.WarrantRate).HasColumnName("assmt_q3_warrant_rate");

                entity.Property(e => e.Adjustment).HasDefaultValue(0m).HasColumnName("assmt_q3_adjusment");
                entity.Property(e => e.QReportAdjustment).HasDefaultValue(0m).HasColumnName("assmt_q3_report_adjusment");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });

            modelBuilder.Entity<Q4>(entity =>
            {
                entity.ToTable("assmt_q4");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_q4_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_q4_amount");

                entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_q4_by_excess_deduction");
                entity.Property(e => e.Paid).HasColumnName("assmt_q4_paid");
                entity.Property(e => e.Discount).HasColumnName("assmt_q4_discount");
                entity.Property(e => e.Warrant).HasColumnName("assmt_q4_warrant");
                entity.Property(e => e.WarrantMethod).HasColumnName("assmt_q4_warrant_method");

                entity.Property(e => e.StartDate).HasColumnName("assmt_q4_start_date");
                entity.Property(e => e.EndDate).HasColumnName("assmt_q4_end_date");

                entity.Property(e => e.IsCompleted).HasColumnName("assmt_q4_is_completed");
                entity.Property(e => e.IsOver).HasColumnName("assmt_q4_is_over");
                entity.Property(e => e.BalanceId).HasColumnName("assmt_q4_assmt_balance_id");

                entity.Property(e => e.WarrantBy).HasColumnName("assmt_q4_warrant_by");
                entity.Property(e => e.DiscountRate).HasColumnName("assmt_q4_discount_rate");
                entity.Property(e => e.WarrantRate).HasColumnName("assmt_q4_warrant_rate");

                entity.Property(e => e.Adjustment).HasDefaultValue(0m).HasColumnName("assmt_q4_adjusment");
                entity.Property(e => e.QReportAdjustment).HasDefaultValue(0m).HasColumnName("assmt_q4_report_adjusment");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });

            modelBuilder.Entity<NQ1>(entity =>
            {
                entity.ToTable("assmt_next_year_q1");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_nq1_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_nq1_amount");

                //entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_q1_by_excess_deduction");
                //entity.Property(e => e.Paid).HasColumnName("assmt_q1_paid");
                //entity.Property(e => e.Discount).HasColumnName("assmt_q1_discount");
                //entity.Property(e => e.Warrant).HasColumnName("assmt_q1_warrant");
                //entity.Property(e => e.WarrantMethod).HasColumnName("assmt_q1_warrant_method");

                //entity.Property(e => e.StartDate).HasColumnName("assmt_q1_start_date");
                //entity.Property(e => e.EndDate).HasColumnName("assmt_q1_end_date");

                //entity.Property(e => e.IsCompleted).HasColumnName("assmt_q1_is_completed");
                //entity.Property(e => e.IsOver).HasColumnName("assmt_q1_is_over");
                entity.Property(e => e.BalanceId).HasColumnName("assmt_nq1_assmt_balance_id");

                //entity.Property(e => e.WarrantBy).HasColumnName("assmt_q1_warrant_by");
                //entity.Property(e => e.DiscountRate).HasColumnName("assmt_q1_discount_rate");
                //entity.Property(e => e.WarrantRate).HasColumnName("assmt_q1_warrant_rate");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();


            });

            modelBuilder.Entity<NQ2>(entity =>
            {
                entity.ToTable("assmt_next_year_q2");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_nq2_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_nq2_amount");

                //entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_q2_by_excess_deduction");
                //entity.Property(e => e.Paid).HasColumnName("assmt_q2_paid");
                //entity.Property(e => e.Discount).HasColumnName("assmt_q2_discount");
                //entity.Property(e => e.Warrant).HasColumnName("assmt_q2_warrant");
                //entity.Property(e => e.WarrantMethod).HasColumnName("assmt_q2_warrant_method");

                //entity.Property(e => e.StartDate).HasColumnName("assmt_q2_start_date");
                //entity.Property(e => e.EndDate).HasColumnName("assmt_q2_end_date");

                //entity.Property(e => e.IsCompleted).HasColumnName("assmt_q2_is_completed");
                //entity.Property(e => e.IsOver).HasColumnName("assmt_q2_is_over");
                entity.Property(e => e.BalanceId).HasColumnName("assmt_nq2_assmt_balance_id");

                //entity.Property(e => e.WarrantBy).HasColumnName("assmt_q2_warrant_by");
                //entity.Property(e => e.DiscountRate).HasColumnName("assmt_q2_discount_rate");
                //entity.Property(e => e.WarrantRate).HasColumnName("assmt_q2_warrant_rate");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });

            modelBuilder.Entity<NQ3>(entity =>
            {
                entity.ToTable("assmt_next_year_q3");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_nq3_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_nq3_amount");

                //entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_q3_by_excess_deduction");
                //entity.Property(e => e.Paid).HasColumnName("assmt_q3_paid");
                //entity.Property(e => e.Discount).HasColumnName("assmt_q3_discount");
                //entity.Property(e => e.Warrant).HasColumnName("assmt_q3_warrant");
                //entity.Property(e => e.WarrantMethod).HasColumnName("assmt_q3_warrant_method");

                //entity.Property(e => e.StartDate).HasColumnName("assmt_q3_start_date");
                //entity.Property(e => e.EndDate).HasColumnName("assmt_q3_end_date");

                //entity.Property(e => e.IsCompleted).HasColumnName("assmt_q3_is_completed");
                //entity.Property(e => e.IsOver).HasColumnName("assmt_q3_is_over");
                entity.Property(e => e.BalanceId).HasColumnName("assmt_nq3_assmt_balance_id");

                //entity.Property(e => e.WarrantBy).HasColumnName("assmt_q3_warrant_by");
                //entity.Property(e => e.DiscountRate).HasColumnName("assmt_q3_discount_rate");
                //entity.Property(e => e.WarrantRate).HasColumnName("assmt_q3_warrant_rate");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });

            modelBuilder.Entity<NQ4>(entity =>
            {
                entity.ToTable("assmt_next_year_q4");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_nq4_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_nq4_amount");

                //entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_q4_by_excess_deduction");
                //entity.Property(e => e.Paid).HasColumnName("assmt_q4_paid");
                //entity.Property(e => e.Discount).HasColumnName("assmt_q4_discount");
                //entity.Property(e => e.Warrant).HasColumnName("assmt_q4_warrant");
                //entity.Property(e => e.WarrantMethod).HasColumnName("assmt_q4_warrant_method");

                //entity.Property(e => e.StartDate).HasColumnName("assmt_q4_start_date");
                //entity.Property(e => e.EndDate).HasColumnName("assmt_q4_end_date");

                //entity.Property(e => e.IsCompleted).HasColumnName("assmt_q4_is_completed");
                //entity.Property(e => e.IsOver).HasColumnName("assmt_q4_is_over");
                entity.Property(e => e.BalanceId).HasColumnName("assmt_nq4_assmt_balance_id");

                //entity.Property(e => e.WarrantBy).HasColumnName("assmt_q4_warrant_by");
                //entity.Property(e => e.DiscountRate).HasColumnName("assmt_q4_discount_rate");
                //entity.Property(e => e.WarrantRate).HasColumnName("assmt_q4_warrant_rate");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });

            // assessmnt balances histroy 
            modelBuilder.Entity<AssessmentBalancesHistory>(entity =>
            {
                entity.ToTable("assmt_balance_history");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_bal_hstry_id");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_bal_hstry_assmt_id");
                entity.Property(e => e.Year).HasColumnName("assmt_bal_hstry_year");
                entity.Property(e => e.StartDate).HasColumnName("assmt_bal_hstry_start_date");
                entity.Property(e => e.EndData).HasColumnName("assmt_bal_hstry_end_date");

                entity.Property(e => e.ExcessPayment).HasColumnName("assmt_bal_hstry_excess_payment");

                entity.Property(e => e.LYArrears).HasColumnName("assmt_bal_hstry_ly_arrears");
                entity.Property(e => e.LYWarrant).HasColumnName("assmt_bal_hstry_ly_warrant");

                entity.Property(e => e.TYArrears).HasColumnName("assmt_bal_hstry_ty_arrears");
                entity.Property(e => e.TYWarrant).HasColumnName("assmt_bal_hstry_ty_warrant");

                entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_bal_hstry_by_excess_deduction");
                entity.Property(e => e.Paid).HasColumnName("assmt_bal_hstry_paid");
                entity.Property(e => e.NumberOfPayments).HasDefaultValue(0).HasColumnName("assmt_bal_histry_number_of_payments");
                entity.Property(e => e.NumberOfCancels).HasDefaultValue(0).HasColumnName("assmt_bal_histry_number_of_cancels");
                entity.Property(e => e.OverPayment).HasColumnName("assmt_bal_hstry_over_payment");

                entity.Property(e => e.TransactionsType).HasColumnName("assmt_bal_hstry_transaction_type");
                entity.Property(e => e.SessionDate).HasColumnName("assmt_bal_hstry_session_date");
                entity.Property(e => e.DiscountRate).HasColumnName("assmt_bal_hstry_discount_rate");
                entity.Property(e => e.Discount).HasColumnName("assmt_bal_hstry_discount");
                entity.Property(e => e.IsCompleted).HasColumnName("assmt_bal_hstry_is_completed");


                entity.HasOne(bal => bal.QH1)
                   .WithOne(q => q.AssessmentBalanceHistory)
                   .HasForeignKey<QH1>(bal => bal.BalanceHistoryId);


                entity.HasOne(bal => bal.QH2)
                  .WithOne(q => q.AssessmentBalanceHistory)
                  .HasForeignKey<QH2>(bal => bal.BalanceHistoryId);

                entity.HasOne(bal => bal.QH3)
                  .WithOne(q => q.AssessmentBalanceHistory)
                  .HasForeignKey<QH3>(bal => bal.BalanceHistoryId);

                entity.HasOne(bal => bal.QH4)
                  .WithOne(q => q.AssessmentBalanceHistory)
                  .HasForeignKey<QH4>(bal => bal.BalanceHistoryId);


                entity.HasOne(bh => bh.Assessment)
                .WithMany(e => e.AssessmentBalanceHistories)
                .HasForeignKey(obli => obli.AssessmentId)  // Rename the foreign key column
                .HasConstraintName("fk_assmt_bal_hstry_id");




                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("assmt_bal_hstry_status");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("assmt_bal_hstry_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("assmt_bal_hstry_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("assmt_bal_hstry_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_bal_hstry_updated_by");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();


                //entity.HasIndex(e => new { e.AssessmentId, e.Year,}).IsUnique(true);
            });

            // assessmnt balances histroy Quarter 1

            modelBuilder.Entity<QH1>(entity =>
            {
                entity.ToTable("assmt_qh1");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_qh1_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_qh1_amount");

                entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_qh1_by_excess_deduction");
                entity.Property(e => e.Paid).HasColumnName("assmt_qh1_paid");
                entity.Property(e => e.Discount).HasColumnName("assmt_qh1_discount");
                entity.Property(e => e.Warrant).HasColumnName("assmt_qh1_warrant");
                entity.Property(e => e.WarrantMethod).HasColumnName("assmt_qh1_warrant_method");

                entity.Property(e => e.StartDate).HasColumnName("assmt_qh1_start_date");
                entity.Property(e => e.EndDate).HasColumnName("assmt_qh1_end_date");

                entity.Property(e => e.IsCompleted).HasColumnName("assmt_qh1_is_completed");
                entity.Property(e => e.BalanceHistoryId).HasColumnName("assmt_qh1_assmt_balance_hstry_id");

                entity.Property(e => e.WarrantBy).HasColumnName("assmt_qh1_warrant_by");
                entity.Property(e => e.DiscountRate).HasColumnName("assmt_qh1_discount_rate");
                entity.Property(e => e.WarrantRate).HasColumnName("assmt_qh1_warrant_rate");

                entity.Property(e => e.Adjustment).HasDefaultValue(0m).HasColumnName("assmt_qh1_adjusment");
                entity.Property(e => e.QReportAdjustment).HasDefaultValue(0m).HasColumnName("assmt_qh1_report_adjusment");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });
            // assessmnt balances histroy Quarter 2
            modelBuilder.Entity<QH2>(entity =>
            {
                entity.ToTable("assmt_qh2");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_qh2_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_qh2_amount");

                entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_qh2_by_excess_deduction");
                entity.Property(e => e.Paid).HasColumnName("assmt_qh2_paid");
                entity.Property(e => e.Discount).HasColumnName("assmt_qh2_discount");
                entity.Property(e => e.Warrant).HasColumnName("assmt_qh2_warrant");
                entity.Property(e => e.WarrantMethod).HasColumnName("assmt_qh2_warrant_method");

                entity.Property(e => e.StartDate).HasColumnName("assmt_qh2_start_date");
                entity.Property(e => e.EndDate).HasColumnName("assmt_qh2_end_date");

                entity.Property(e => e.IsCompleted).HasColumnName("assmt_qh2_is_completed");
                entity.Property(e => e.BalanceHistoryId).HasColumnName("assmt_qh2_assmt_balance_hstry_id");

                entity.Property(e => e.WarrantBy).HasColumnName("assmt_qh2_warrant_by");
                entity.Property(e => e.DiscountRate).HasColumnName("assmt_qh2_discount_rate");
                entity.Property(e => e.WarrantRate).HasColumnName("assmt_qh2_warrant_rate");

                entity.Property(e => e.Adjustment).HasDefaultValue(0m).HasColumnName("assmt_qh2_adjusment");
                entity.Property(e => e.QReportAdjustment).HasDefaultValue(0m).HasColumnName("assmt_qh2_report_adjusment");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });
            // assessmnt balances histroy Quarter 3
            modelBuilder.Entity<QH3>(entity =>
            {
                entity.ToTable("assmt_qh3");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_qh3_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_qh3_amount");

                entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_qh3_by_excess_deduction");
                entity.Property(e => e.Paid).HasColumnName("assmt_qh3_paid");
                entity.Property(e => e.Discount).HasColumnName("assmt_qh3_discount");
                entity.Property(e => e.Warrant).HasColumnName("assmt_qh3_warrant");
                entity.Property(e => e.WarrantMethod).HasColumnName("assmt_qh3_warrant_method");

                entity.Property(e => e.StartDate).HasColumnName("assmt_qh3_start_date");
                entity.Property(e => e.EndDate).HasColumnName("assmt_qh3_end_date");

                entity.Property(e => e.IsCompleted).HasColumnName("assmt_qh3_is_completed");
                entity.Property(e => e.BalanceHistoryId).HasColumnName("assmt_qh3_assmt_balance_hstry_id");

                entity.Property(e => e.WarrantBy).HasColumnName("assmt_qh3_warrant_by");
                entity.Property(e => e.DiscountRate).HasColumnName("assmt_qh3_discount_rate");
                entity.Property(e => e.WarrantRate).HasColumnName("assmt_qh3_warrant_rate");

                entity.Property(e => e.Adjustment).HasDefaultValue(0m).HasColumnName("assmt_qh3_adjusment");
                entity.Property(e => e.QReportAdjustment).HasDefaultValue(0m).HasColumnName("assmt_qh3_report_adjusment");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();


            });
            // assessmnt balances histroy Quarter 3
            modelBuilder.Entity<QH4>(entity =>
            {
                entity.ToTable("assmt_qh4");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_qh4_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_qh4_amount");

                entity.Property(e => e.ByExcessDeduction).HasColumnName("assmt_qh4_by_excess_deduction");
                entity.Property(e => e.Paid).HasColumnName("assmt_qh4_paid");
                entity.Property(e => e.Discount).HasColumnName("assmt_qh4_discount");
                entity.Property(e => e.Warrant).HasColumnName("assmt_qh4_warrant");
                entity.Property(e => e.WarrantMethod).HasColumnName("assmt_qh4_warrant_method");

                entity.Property(e => e.StartDate).HasColumnName("assmt_qh4_start_date");
                entity.Property(e => e.EndDate).HasColumnName("assmt_qh4_end_date");
                entity.Property(e => e.IsCompleted).HasColumnName("assmt_qh4_is_completed");
                entity.Property(e => e.BalanceHistoryId).HasColumnName("assmt_qh4_assmt_balance_hstry_id");

                entity.Property(e => e.WarrantBy).HasColumnName("assmt_qh4_warrant_by");
                entity.Property(e => e.DiscountRate).HasColumnName("assmt_qh4_discount_rate");
                entity.Property(e => e.WarrantRate).HasColumnName("assmt_qh4_warrant_rate");

                entity.Property(e => e.Adjustment).HasDefaultValue(0m).HasColumnName("assmt_qh4_adjusment");
                entity.Property(e => e.QReportAdjustment).HasDefaultValue(0m).HasColumnName("assmt_qh4_report_adjusment");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();


            });

            modelBuilder.Entity<AssessmentVoteAssign>(entity =>
            {
                entity.ToTable("assmt_vote_assigns");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("assmt_vtasgn_id");
                entity.HasIndex(e => new { e.SabhaId, e.PaymentTypeId }).IsUnique();
                entity.Property(e => e.SabhaId).HasColumnName("assmt_vtasgn_sabha_id");
                entity.Property(e => e.PaymentTypeId).HasColumnName("assmt_vtasgn_votetype_id");
                entity.Property(e => e.VoteAssignmentDetailId).HasColumnName("assmt_vtasgn_vote");
                entity.Property(e => e.VoteDetailId).HasColumnName("assmt_vtasgn_vote_detail_id");
                entity.Ignore(e => e.VoteAssignmentDetails);




                // Foreign key referencing
                entity.HasOne(vt => vt.VotePaymentType)
                        .WithMany(pc => pc.VoteAssigns)
                        .HasForeignKey(vt => vt.PaymentTypeId)  // Rename the foreign key column
                        .HasConstraintName("fk_assmt_vtasgn_vote_type");


                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("assmt_vtasgn_status");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("assmt_vtasgn_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("assmt_vtasgn_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("assmt_vtasgn_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_vtasgn_updated_by");
            });


            modelBuilder.Entity<VotePaymentType>(entity =>
            {
                entity.ToTable("assmt_vote_payment_types");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("assmt_vt_pay_type_id");
                entity.Property(e => e.Description).HasColumnName("assm_vt_pay_type_desc");

                entity.Property(e => e.Status).HasColumnName("assm_vt_pay_type_status");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("assm_vt_pay_type_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("assm_vt_pay_type_cat_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("assm_vt_pay_type_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("assm_vt_pay_type_updated_by");
            });


            modelBuilder.Entity<AssessmentAuditLog>(entity =>
            {

                entity.ToTable("assmt_audit_logs");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("assmt_atl_id");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_atl_assmt_id");
                entity.Property(e => e.Action).HasColumnName("assmt_atl_action");

                entity.Property(e => e.Timestamp).HasColumnName("assmt_atl_time_stamp");
                entity.Property(e => e.EntityType).HasColumnName("assmt_entity_type");

                //Foreign key referencing
                entity.HasOne(alt => alt.Assessment)
                        .WithMany(e => e.AssessmentAuditLogs)
                        .HasForeignKey(obli => obli.AssessmentId)  // Rename the foreign key column
                        .HasConstraintName("fk_atl_assmt_id");


                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("assmt_atl_time_stamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ActionBy).HasColumnName("assmt_action_by");


            });


            modelBuilder.Entity<AssessmentTransaction>(entity =>
            {
                entity.ToTable("assmt_transactions");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_tr_id");

                entity.Property(e => e.AssessmentId).HasColumnName("assmt_tr_assmt_id");
                entity.Property(e => e.Type).HasColumnName("assmt_tr_type");

                entity.Property(e => e.LYArrears).HasColumnName("assmt_tr_ly_arrears");
                entity.Property(e => e.LYWarrant).HasColumnName("assmt_tr_ly_warrant");

                entity.Property(e => e.TYArrears).HasColumnName("assmt_tr_ty_arrears");
                entity.Property(e => e.TYWarrant).HasColumnName("assmt_tr_ty_warrant");

                entity.Property(e => e.Q1).HasColumnName("assmt_tr_q1");
                entity.Property(e => e.Q2).HasColumnName("assmt_tr_q2");
                entity.Property(e => e.Q3).HasColumnName("assmt_tr_q3");
                entity.Property(e => e.Q4).HasColumnName("assmt_tr_q4");



                entity.Property(e => e.RunningOverPay).HasColumnName("assmt_tr_rn_overpay");
                entity.Property(e => e.DiscountRate).HasDefaultValue(0m).HasColumnName("assmt_tr_rn_discount_rate");
                entity.Property(e => e.RunningDiscount).HasColumnName("assmt_tr_rn_discount");
                entity.Property(e => e.RunningTotal).HasColumnName("assmt_tr_rn_total");
                entity.Property(e => e.SessionDate).HasColumnName("assmt_tr_session_date");

                entity.Property(e => e.DateTime)
                   .HasColumnType("datetime")
                   .HasColumnName("assmt_tr_date_time")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");


            });


            modelBuilder.Entity<AssessmentQuarterReport>(entity =>
            {
                entity.ToTable("assmt_quarter_report");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_qrt_id");

                entity.Property(e => e.AssessmentId).HasColumnName("assmt_qrt_assmt_id");
                entity.Property(e => e.Year).HasColumnName("assmt_qrt_year");
                entity.Property(e => e.QuarterNo).HasColumnName("assmt_qrt_quater_no");

                entity.Property(e => e.LYArrears).HasColumnName("assmt_qrt_ly_arrears");
                entity.Property(e => e.LYWarrant).HasColumnName("assmt_qrt_ly_warrant");

                entity.Property(e => e.TYArrears).HasColumnName("assmt_qrt_ty_arrears");
                entity.Property(e => e.TYWarrant).HasColumnName("assmt_qrt_ty_warrant");
                entity.Property(e => e.Adjustment).HasDefaultValue(0m).HasColumnName("assmt_qrt_adjustment");

                entity.Property(e => e.AnnualAmount).HasColumnName("assmt_qrt_annual_amount");
                entity.Property(e => e.QAmount).HasColumnName("assmt_qrt_q_amount");
                entity.Property(e => e.QWarrant).HasColumnName("assmt_qrt_q_warrant");
                entity.Property(e => e.QDiscount).HasColumnName("assmt_qrt_q_discount");
                entity.Property(e => e.M1Paid).HasColumnName("assmt_qrt_m1_paid");
                entity.Property(e => e.M2Paid).HasColumnName("assmt_qrt_m2_paid");
                entity.Property(e => e.M3Paid).HasColumnName("assmt_qrt_m3_paid");
                entity.Property(e => e.UseTransactionsType).HasColumnName("assmt_used_tr_type");
                entity.Property(e => e.RunningBalance).HasColumnName("assmt_qrt_running_balance");


                entity.Property(e => e.DateTime)
                   .HasColumnType("datetime")
                   .HasColumnName("assmt_tr_date_time")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

                // mandatory fields for entity
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_qrt_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_qrt_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_qrt_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assmt_qrt_updated_at");


                entity.HasOne(rpt => rpt.Assessment)
               .WithMany(e => e.AssessmentQuarterReport)
               .HasForeignKey(obli => obli.AssessmentId)  // Rename the foreign key column
               .HasConstraintName("fk_assmt_q_rpt_id");
                
                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

                entity.HasIndex(e => new { e.Year, e.QuarterNo, e.AssessmentId }).IsUnique(true);
            });


            modelBuilder.Entity<AssessmentQuarterReportLog>(entity =>
            {
                entity.ToTable("assmt_quarter_report_logs");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_qrt_id");

                entity.Property(e => e.AssessmentId).HasColumnName("assmt_qrt_assmt_id");
                entity.Property(e => e.Year).HasColumnName("assmt_qrt_year");
                entity.Property(e => e.QuarterNo).HasColumnName("assmt_qrt_quater_no");

                entity.Property(e => e.LYArrears).HasColumnName("assmt_qrt_ly_arrears");
                entity.Property(e => e.LYWarrant).HasColumnName("assmt_qrt_ly_warrant");

                entity.Property(e => e.TYArrears).HasColumnName("assmt_qrt_ty_arrears");
                entity.Property(e => e.TYWarrant).HasColumnName("assmt_qrt_ty_warrant");

                entity.Property(e => e.AnnualAmount).HasColumnName("assmt_qrt_annual_amount");
                entity.Property(e => e.QAmount).HasColumnName("assmt_qrt_q_amount");
                entity.Property(e => e.QWarrant).HasColumnName("assmt_qrt_q_warrant");
                entity.Property(e => e.QDiscount).HasColumnName("assmt_qrt_q_discount");
                entity.Property(e => e.M1Paid).HasColumnName("assmt_qrt_m1_paid");
                entity.Property(e => e.M2Paid).HasColumnName("assmt_qrt_m2_paid");
                entity.Property(e => e.M3Paid).HasColumnName("assmt_qrt_m3_paid");
                entity.Property(e => e.UseTransactionsType).HasColumnName("assmt_used_tr_type");
                entity.Property(e => e.RunningBalance).HasColumnName("assmt_qrt_running_balance");
                entity.Property(e => e.SessionDate).HasColumnName("assmt_qrt_session_date");


                entity.Property(e => e.DateTime)
                   .HasColumnType("datetime")
                   .HasColumnName("assmt_tr_date_time")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

                // mandatory fields for entity
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_qrt_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_qrt_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_qrt_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assmt_qrt_updated_at");


                entity.HasOne(rpt => rpt.AssessmentQuarterReport)
               .WithMany(e => e.AssessmentQuarterReportLogs)
               .HasForeignKey(obli => obli.AssessmentId)  // Rename the foreign key column
               .HasConstraintName("fk_assmt_q_rpt_log_id");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

                //entity.HasIndex(e => new { e.Year, e.QuarterNo, e.AssessmentId }).IsUnique(true);
            });

            modelBuilder.Entity<AssessmentRates>(entity =>
            {
                entity.ToTable("assmt_rates");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_rates_id");
                entity.Property(e => e.AnnualDiscount).HasColumnName("assmt_annual_discount");

                entity.Property(e => e.QuarterDiscount).HasColumnName("assmt_quater_discount");


            });




            modelBuilder.Entity<AllocationLog>(entity =>
            {
                entity.ToTable("assmt_allocation_logs");

                entity.HasCharSet("utf8mb4")
                  .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("assmt_alg_id");
                entity.Property(e => e.Amount).HasColumnName("assmt_allocation_amount");
                entity.Property(e => e.FromDate).HasColumnName("assmt_alg_from_date");
                entity.Property(e => e.ToDate).HasColumnName("assmt_alg_to_date");
                entity.Property(e => e.Description).HasColumnName("assmt_alg_description");
                entity.Property(e => e.AllocationId).HasColumnName("assmt_alg_allocation_id");


                //many to one relationship

                entity.HasOne(d => d.Allocation)
                   .WithMany(p => p.AllocationLogs)
                   .HasForeignKey(d => d.AllocationId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_assmt_alg_allocation_id");

                // mandatory fields for entity
                entity.Property(e => e.Status).HasColumnType("int(11)").HasColumnName("assmt_alg_status");
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_alg_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_alg_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_alg_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assmt_alg_updated_at");

            });



            modelBuilder.Entity<AssessmentProcess>(entity =>
            {
                entity.ToTable("assmt_processes");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_process_id");
                entity.Property(e => e.ActionBy).HasColumnName("assmt_process_action_by");

                entity.Property(e => e.Year).HasColumnName("assmt_process_year");
                entity.Property(e => e.ShabaId).HasColumnName("assmt_process_sbaha_id");
                entity.Property(e => e.ProcessType).HasColumnName("assmt_process_type");
                entity.Property(e => e.ProceedDate).HasColumnName("assmt_proceed_date").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.BackUpKey).HasColumnName("assmt_process_backupkey");

                entity.HasIndex(e => new { e.Year, e.ShabaId, e.ProcessType }).IsUnique(true);
            });



            modelBuilder.Entity<AssessmentDescriptionLog>(entity =>
            {
                entity.ToTable("assmt_description_logs");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_deslog_id");
                entity.Property(e => e.DescriptionId).HasColumnName("assmt_deslog_des_id");
                entity.Property(e => e.Comment).HasColumnName("assmt_deslog_cmt");
                entity.Property(e => e.ActionBy).HasColumnName("assmt_deslog_action_by");
                entity.Property(e => e.ActionNote).HasColumnName("assmt_deslog_action_note");
                entity.Property(e => e.ActivatedDate).HasColumnName("assmt_deslog_activate_date");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_deslog_assmt_id");
                
                entity.Property(e => e.ActivationYear).HasColumnName("assmt_deslog_act_year");
                entity.Property(e => e.ActivationQuarter).HasColumnName("assmt_deslog_act_quarter");

                //Foreign key referencing
                entity.HasOne(alt => alt.Assessment)
                        .WithMany(e => e.AssessmentDescriptionLogs)
                        .HasForeignKey(obli => obli.AssessmentId)  // Rename the foreign key column
                        .HasConstraintName("fk_deslog_assmt_id");

                entity.Property(e => e.Status).HasColumnName("assmt_deslog_status");
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_deslog_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_deslog_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_deslog_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assmt_deslog_updated_at");

            });


            modelBuilder.Entity<AssessmentPropertyTypeLog>(entity =>
            {
                entity.ToTable("assmt_property_types_logs");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("assmt_proptypeslog_id");
                entity.Property(e => e.PropertyTypeId).HasColumnName("assmt_proptypeslog_des_id");
                entity.Property(e => e.Comment).HasColumnName("assmt_proptypeslog_cmt");
                entity.Property(e => e.ActionBy).HasColumnName("assmt_proptypeslog_action_by");
                entity.Property(e => e.ActionNote).HasColumnName("assmt_proptypeslog_action_note");
                entity.Property(e => e.ActivatedDate).HasColumnName("assmt_proptypeslog_activate_date");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_proptypeslog_assmt_id");
                
                entity.Property(e => e.ActivationYear).HasColumnName("assmt_proptypeslog_act_year");
                entity.Property(e => e.ActivationQuarter).HasColumnName("assmt_proptypeslog_act_quarter");


                //Foreign key referencing
                entity.HasOne(alt => alt.Assessment)
                        .WithMany(e => e.AssessmentPropertyTypeLogs)
                        .HasForeignKey(obli => obli.AssessmentId)  // Rename the foreign key column
                        .HasConstraintName("fk_proptypes_log_assmt_id");


                entity.Property(e => e.Status).HasColumnName("assmt_proptypeslog_status");
                entity.Property(e => e.CreatedBy).HasColumnName("assmt_proptypeslog_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_proptypeslog_updated_by");
                entity.Property(e => e.CreatedAt).HasColumnName("assmt_proptypeslog_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("assmt_proptypeslog_updated_at");

            });

            modelBuilder.Entity<AssessmentDocument>(entity =>
            {


                entity.ToTable("assessment_documents");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("assmt_doc_id");
                entity.Property(e => e.DocType).HasColumnName("assmt_doc_type");
                entity.Property(e => e.Uri).HasColumnName("assmt_doc_uri");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_doc_assessment_id");

                entity.Ignore(wp => wp.File);





                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("assmt_doc_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_doc_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_doc_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("assmt_doc_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("assmt_doc_updated_by");
            });


            modelBuilder.Entity<AssessmentJournal>(entity =>
            {


                entity.ToTable("assessment_journals");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("assmt_jnl_id");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_jnl_assessment_id");

                entity.Property(e => e.OldAllocation).HasColumnName("assmt_jnl_old_allocation");
                entity.Property(e => e.NewAllocation).HasColumnName("assmt_jnl_new_allocation");

                entity.Property(e => e.OldExcessPayment).HasColumnName("assmt_jnl_old_excess_payment");
                entity.Property(e => e.NewExcessPayment).HasColumnName("assmt_jnl_new_excess_payment");

                entity.Property(e => e.OldLYArrears).HasColumnName("assmt_jnl_old_ly_arrears");
                entity.Property(e => e.NewLYArrears).HasColumnName("assmt_jnl_new_ly_arrears");

                entity.Property(e => e.OldLYWarrant).HasColumnName("assmt_jnl_old_ly_warrant");
                entity.Property(e => e.NewLYWarrant).HasColumnName("assmt_jnl_new_ly_warrant");

                entity.Property(e => e.OldTYArrears).HasColumnName("assmt_jnl_old_ty_arrears");
                entity.Property(e => e.NewTYArrears).HasColumnName("assmt_jnl_new_ty_arrears");

                entity.Property(e => e.OldTYWarrant).HasColumnName("assmt_jnl_old_ty_warrant");
                entity.Property(e => e.NewTYWarrant).HasColumnName("assmt_jnl_new_ty_warrant");

                entity.Property(e => e.OldPropertyTypeId).HasColumnName("assmt_jnl_old_prop_type");
                entity.Property(e => e.NewPropertyTypeId).HasColumnName("assmt_jnl_new_prop_type");

                entity.Property(e => e.changingProperties).HasColumnName("assmt_jnl_change_properties");
                entity.Property(e => e.DraftApproveReject).HasColumnName("assmt_jnl_draft_approve_reject");

                entity.Property(e => e.RequestDate).HasColumnName("assmt_jnl_request_date");
                entity.Property(e => e.RequestBy).HasColumnName("assmt_jnl_request_by");
                entity.Property(e => e.RequestNote).HasColumnName("assmt_jnl_request_note");
                
                
                entity.Property(e => e.Q1Adjustment).HasColumnName("assmt_q1_adjustment");
                entity.Property(e => e.Q2Adjustment).HasColumnName("assmt_q2_adjustment");
                entity.Property(e => e.Q3Adjustment).HasColumnName("assmt_q3_adjustment");
                entity.Property(e => e.Q4Adjustment).HasColumnName("assmt_q4_adjustment");



                


                entity.Property(e => e.ActionDate).HasColumnName("assmt_jnl_action_date");
                entity.Property(e => e.ActionBy).HasColumnName("assmt_jnl_action_by");
                entity.Property(e => e.ActionNote).HasColumnName("assmt_jnl_action_note");



            });

            modelBuilder.Entity<AssessmentAssetsChange>(entity =>
            {


                entity.ToTable("assessment_assets_change");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("assmt_assets_cng_id");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_assets_cng_assessment_id");

                entity.Property(e => e.OldNumber).HasColumnName("assmt_assets_cng_old_asmt_number");
                entity.Property(e => e.NewNumber).HasColumnName("assmt_assets_cng_new_asmt_number");

                entity.Property(e => e.OldName).HasColumnName("assmt_assets_cng_old_name");
                entity.Property(e => e.NewName).HasColumnName("assmt_assets_cng_new_name");

                entity.Property(e => e.OldAddressLine1).HasColumnName("assmt_assets_cng_old_address_1");
                entity.Property(e => e.NewAddressLine1).HasColumnName("assmt_assets_cng_new_address_1");

                entity.Property(e => e.OldAddressLine2).HasColumnName("assmt_assets_cng_old_address_2");
                entity.Property(e => e.NewAddressLine2).HasColumnName("assmt_assets_cng_new_address_2");

                entity.Property(e => e.ChangingProperties).HasColumnName("assmt_assets_cng_change_properties");
                entity.Property(e => e.DraftApproveReject).HasColumnName("assmt_assets_cng_draft_approve_reject");

                entity.Property(e => e.RequestDate).HasColumnName("assmt_assets_cng_request_date");
                entity.Property(e => e.RequestBy).HasColumnName("assmt_assets_cng_request_by");
                entity.Property(e => e.RequestNote).HasColumnName("assmt_assets_cng_request_note");



                entity.Property(e => e.ActionDate).HasColumnName("assmt_assets_cng_action_date");
                entity.Property(e => e.ActionBy).HasColumnName("assmt_assets_cng_action_by");
                entity.Property(e => e.ActionNote).HasColumnName("assmt_assets_cng_action_note");




            });

            modelBuilder.Entity<AssessmentBillAdjustment>(entity =>
            {


                entity.ToTable("assessment_bill_adjustment");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("assmt_bil_adj_id");

                entity.Property(e => e.AssessmentId).HasColumnName("assmt_bil_adj_assessment_id");
                entity.Property(e => e.MixOrderId).HasColumnName("assmt_bil_adj_mix_order_id");
                entity.Property(e => e.DraftApproveRejectWithdraw).HasDefaultValue(1).HasColumnName("assmt_bil_adj_araft_approve_reject_withdraw");


                entity.Property(e => e.RequestDate).HasColumnName("assmt_bil_adj_request_date");
                entity.Property(e => e.RequestBy).HasColumnName("assmt_bil_adj_request_by");
                entity.Property(e => e.RequestNote).HasColumnName("assmt_bil_adj_request_note");



                entity.Property(e => e.ActionDate).HasColumnName("assmtbil_adj_action_date");
                entity.Property(e => e.ActionBy).HasColumnName("assmt_bil_adj_action_by");
                entity.Property(e => e.ActionNote).HasColumnName("assmt_bil_adj_action_note");


                //Foreign key referencing
                entity.HasOne(alt => alt.Assessment)
                        .WithMany(e => e.AssessmentBillAdjustments)
                        .HasForeignKey(obli => obli.AssessmentId)  // Rename the foreign key column
                        .HasConstraintName("fk_bill_adj_assmt_id");

            });


            modelBuilder.Entity<AmalgamationAssessment>(entity =>
            {
                entity.ToTable("assmt_amalgamation_assessment");

                entity.HasCharSet("utf8mb4")
                  .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("assmt_amalgamation_assmt_id");
                entity.Property(e => e.KFormId).HasColumnName("k_form_id");
                entity.Property(e => e.AssessmentId).HasColumnName("assmt_id");

                //Foreign key referencing
                entity.HasOne(alt => alt.Assessment)
                        .WithMany(e => e.AmalgamationAssessment)
                        .HasForeignKey(obli => obli.AssessmentId)  // Rename the foreign key column
                        .HasConstraintName("fk_amalgamation_assmt_id");

            });


            modelBuilder.Entity<AmalgamationSubDivision>(entity =>
            {
                entity.ToTable("assmt_amalgamation_subdivision");

                entity.HasCharSet("utf8mb4")
                  .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("assmt_amalgamation_assmt_id");
                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.ParentAssessmentId).HasColumnName("parent_assmt_id");
                entity.Property(e => e.DerivedAssessmentId).HasColumnName("derived_assmt_id");
                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.RequestedAction).HasColumnName("requested_action");
                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");


            });

            modelBuilder.Entity<Amalgamation>(entity =>
            {
                entity.ToTable("amalgamation");

                entity.HasCharSet("utf8mb4")
                  .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("amalgamation_id");
                entity.Property(e => e.KFormId).HasColumnName("k_form_id");
                entity.Property(e => e.AmalgamationSubDivisionId).HasColumnName("amalgamation_subdivision_id");

                //Foreign key referencing
                entity.HasOne(alt => alt.AmalgamationSubDivision)
                        .WithMany(e => e.Amalgamations)
                        .HasForeignKey(obli => obli.AmalgamationSubDivisionId)  // Rename the foreign key column
                        .HasConstraintName("fk_amalgamation_subdivision_id");

            });

            modelBuilder.Entity<SubDivision>(entity =>
            {
                entity.ToTable("sub_division"); // Changed table name

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sub_division_id"); // Updated column name
                entity.Property(e => e.KFormId).HasColumnName("k_form_id");
                entity.Property(e => e.AmalgamationSubDivisionId).HasColumnName("subdivision_amalgamation_id"); // Updated column name

                // Foreign key referencing
                entity.HasOne(alt => alt.AmalgamationSubDivision) 
                    .WithMany(e => e.SubDivisions)
                    .HasForeignKey(obli => obli.AmalgamationSubDivisionId) 
                    .HasConstraintName("fk_subdivision_amalgamation_id"); 
            });

            modelBuilder.Entity<AmalgamationSubDivisionActions>(entity =>
            {
                entity.ToTable("amalgamation_subdivision_actions");
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AmalgamationSubDivisionId).HasColumnName("amalgamation_subdivision_id");
                entity.Property(e => e.ActionState).HasColumnName("action_state");
                entity.Property(e => e.ActionBy).HasColumnName("action_by");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.ActionDateTime).HasColumnName("action_date");
                entity.Property(e => e.SystemActionAt).HasColumnName("system_action_at");

                entity.HasOne(val => val.AmalgamationSubDivision)
                    .WithMany(v => v.AmalgamationSubDivisionActions)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(val => val.AmalgamationSubDivisionId);

            });

            modelBuilder.Entity<AmalgamationSubDivisionDocuments>(entity =>
            {
                entity.ToTable("amalgamation_subdivision_documents");
                entity.HasCharSet("utf8mb4").UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);


                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AmalgamationSubDivisionId).HasColumnName("amalgamation_subdivision_id");
                entity.Property(e => e.DocType).HasColumnName("doc_type");

                entity.Property(e => e.Uri).HasColumnName("uri");


                entity.HasOne(vl => vl.AmalgamationSubDivision)
                    .WithMany(v => v.AmalgamationSubDivisionDocuments)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasForeignKey(vl => vl.AmalgamationSubDivisionId);
                //.HasConstraintName("fk_voucherId_id");


                entity.Property(e => e.Status).HasColumnName("row_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            });


            modelBuilder.Entity<AssessmentATD>(entity =>
            {
                entity.ToTable("assessment_atd");

                entity.Property(e => e.Id)
                    .HasColumnName("assmt_atd_id")
                    .IsRequired();

                entity.Property(e => e.AssessmentId)
                    .HasColumnName("assmt_atd_assessment_id");

                entity.Property(e => e.ATDRequestStatus)
                    .HasColumnName("assmt_atd_request_status")
                    .HasColumnType("int");

                entity.Property(e => e.RequestDate)
                    .HasColumnName("assmt_atd_request_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.RequestBy)
                    .HasColumnName("assmt_atd_request_by")
                    .HasColumnType("int");

                entity.Property(e => e.RequestNote)
                    .HasColumnName("assmt_atd_request_note")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ActionDate)
                    .HasColumnName("assmt_atd_action_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ActionBy)
                    .HasColumnName("assmt_atd_action_by")
                    .HasColumnType("int");

                entity.Property(e => e.ActionNote)
                    .HasColumnName("assmt_atd_action_note")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OfficeId)
                   .HasColumnName("assmt_atd_office_id")
                   .HasColumnType("int");

                entity.Property(e => e.SabhaId)
                    .HasColumnName("assmt_atd_sabha_id")
                    .HasColumnType("int");

                entity.Property(e => e.Status)
                    .HasColumnName("assmt_atd_status")
                    .HasColumnType("int");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("int");

                entity.HasMany(e => e.AssessmentATDOwnerslogs)
                    .WithOne(o => o.AssessmentATD)
                    .HasForeignKey(o => o.AssessmentATDId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(atd => atd.Assessment)
                        .WithMany(e => e.AssessmentATDs)
                        .HasForeignKey(obli => obli.AssessmentId);
            });

            modelBuilder.Entity<AssessmentATDOwnerslog>(entity =>
            {
                entity.ToTable("assessment_atd_ownerslog");

                entity.Property(e => e.Id)
                    .HasColumnName("atd_ownrlog_id")  
                    .IsRequired();

                entity.Property(e => e.AssessmentATDId)
                    .HasColumnName("atd_ownrlog_assmt_atd_id")  
                    .HasColumnType("int");

                entity.Property(e => e.PartnerId)
                    .HasColumnName("atd_ownrlog_partner_id")  
                    .HasColumnType("int");

                entity.Property(e => e.OwnerName)
                    .HasColumnName("atd_ownrlog_owner_full_name")  
                    .HasColumnType("varchar(255)")
                    .IsRequired();

                entity.Property(e => e.AddressLine1)
                    .HasColumnName("atd_ownrlog_owner_address_line_1")  
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("atd_ownrlog_owner_address_line_2")  
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.OwnerType)
                    .HasColumnName("atd_ownrlog_type_of_owner") 
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(e => e.OwnerStatus)
                    .HasColumnName("atd_ownrlog_status_of_owner")  
                    .HasColumnType("int")
                    .IsRequired();
            });

        }
    }
}
