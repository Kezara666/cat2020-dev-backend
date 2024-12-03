using CAT20.Core.Models.AssessmentTax;
using CAT20.Core.Models.WaterBilling;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data
{
    public partial class WaterBillingDbContext : DbContext
    {
        public WaterBillingDbContext()
        {

        }
        public WaterBillingDbContext(DbContextOptions<WaterBillingDbContext> options) : base(options)
        {

        }

        public virtual DbSet<WaterProject> WaterProjects { get; set; }
        public virtual DbSet<WaterTariff> WaterTariffs { get; set; }
        public virtual DbSet<NonMeterFixCharge> NonMeterFixCharges { get; set; }
        public virtual DbSet<WaterProjectNature> WaterProjectNatures { get; set; }
        public virtual DbSet<WaterProjectSubRoad> WaterProjectSubRoads { get; set; }
        public virtual DbSet<WaterProjectMainRoad> WaterProjectMainRoads { get; set; }

        public virtual DbSet<MeterReaderAssign> MeterReaderAssigns { get; set; }
        public virtual DbSet<MeterConnectInfo> MeterConnectInfos { get; set; }
        public virtual DbSet<NumberSequence> NumberSequences { get; set; }

        public virtual DbSet<VoteAssign> VoteAssigns { get; set; }
        public virtual DbSet<PaymentCategory> PaymentCategories { get; set; }

        public virtual DbSet<ApplicationForConnection> ApplicationForConnections { get; set; }
        public virtual DbSet<ApplicationForConnectionDocument> ApplicationForConnectionDocuments { get; set; }
        public virtual DbSet<WaterConnection> WaterConnections { get; set; }
        public virtual DbSet<WaterBillDocument> WaterBillDocuments { get; set; }
        public virtual DbSet<WaterConnectionNatureLog> WaterConnectionNatureLogs { get; set; }
        public virtual DbSet<WaterConnectionStatusLog> WaterConnectionStatusLogs { get; set; }


        public virtual DbSet<OpeningBalanceInformation> OpeningBalanceInformations { get; set; }
        public virtual DbSet<OBLIApprovalStatus> OBLIApprovalStatuses { get; set; }
        public virtual DbSet<ConnectionAuditLog> ConnectionAuditLogs { get; set; }
        public virtual DbSet<WaterConnectionBalance> Balances { get; set; }
        public virtual DbSet<WaterConnectionBalanceHistory> BalanceHistory { get; set; }
        public virtual DbSet<WaterMonthEndReport> WaterMonthEndReport { get; set; }






        // Relations
        public DbSet<WaterProjectGnDivision> WaterProjectGnDivisions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_sinhala_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<WaterProject>(entity =>
            {
                entity.ToTable("wb_water_projects");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_wp_id");
                entity.Property(e => e.Name).HasColumnName("wb_wp_name");
                entity.Property(e => e.OfficeId).HasColumnName("wb_wp_office_id");

                //entity.Ignore(wp => wp.MainRoads);
                //entity.Ignore(wp => wp);

                // Foreign key referencing

                //entity.HasMany(s => s.Natures)
                //.WithMany(c => c.WaterProjects)
                //.UsingEntity(j => j.ToTable("water_projects_natures_assign"));



                entity.HasMany(s => s.Natures)
                .WithMany(c => c.WaterProjects)
                .UsingEntity(j =>
                {
                    j.ToTable("water_projects_natures_assign");
                    j.Property<int>("CreatedBy");
                });



                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_wp_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_wp_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_wp_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_wp_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_wp_updated_by");

                entity.HasIndex(e => new { e.OfficeId, e.Name }).IsUnique();
            });








            modelBuilder.Entity<WaterTariff>(entity =>
            {


                entity.ToTable("wb_water_tariffs");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_wt_id");
                entity.Property(e => e.WaterProjectId).HasColumnName("wb_wt_wp_id");
                entity.Property(e => e.NatureId).HasColumnName("wb_wt_nature_id");

                entity.Property(e => e.RangeStart).HasColumnName("wb_wt_range_start");
                entity.Property(e => e.RangeEnd).HasColumnName("wb_wt_range_end");
                entity.Property(e => e.UnitPrice).HasColumnName("wb_wt_unit_price");
                entity.Property(e => e.FixedCharge).HasColumnName("wb_wt_fixed_charge");


                // Foreign key referencing
                entity.HasOne(nmfc => nmfc.WaterProjectNature)
                        .WithMany(n => n.WaterTariffs)
                        .HasForeignKey(nmfc => nmfc.NatureId)  // Rename the foreign key column
                        .HasConstraintName("fk_wp_tariff_wp_nature");

                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_wt_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_wt_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_wt_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_wt_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_wt_updated_by");
            });



            modelBuilder.Entity<NonMeterFixCharge>(entity =>
            {


                entity.ToTable("wb_non_meter_fix_charges");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_nmfc_id");
                entity.Property(e => e.WaterProjectId).HasColumnName("wb_nmfc_wp_id");
                entity.Property(e => e.NatureId).HasColumnName("wb_nmfc_nature_id");


                entity.Property(e => e.FixedCharge).HasColumnName("wb_nmfc_fixed_charge");

                entity.HasIndex(e => new { e.NatureId, e.WaterProjectId }).IsUnique();



                // Foreign key referencing
                entity.HasOne(nmfc => nmfc.WaterProjectNature)
                .WithMany(n => n.NonMeterFixCharges)
                .HasForeignKey(nmfc => nmfc.NatureId)  // Rename the foreign key column
                .HasConstraintName("fk_wp_nmfc_wp_nature");


                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_nmfc_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_nmfc_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_nmfc_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_nmfc_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_nmfc_updated_by");
            });


            modelBuilder.Entity<WaterProjectNature>(entity =>
            {


                entity.ToTable("wb_wp_natures");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_wp_nature_id");
                entity.Property(e => e.Type).HasColumnName("wb_wp_nature_type");
                entity.Property(e => e.SabhaId).HasColumnName("wb_wp_nature_sabha_id");
                entity.Property(e => e.CType).HasColumnName("wb_wp_nature_c_type");


                entity.HasIndex(n => new { n.Type, n.SabhaId }).IsUnique();

                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_wp_nature_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_wp_nature_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_wp_nature_updated_at");


                //entity.HasOne(nat => nat.NonMeterFixCharge)
                //   .WithOne(nmc => nmc.WaterProjectNature)
                //   .HasForeignKey<NonMeterFixCharge>(nmc => nmc.NatureId);

                entity.Property(e => e.CreatedBy).HasColumnName("wb_wp_nature_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_wp_nature_updated_by");
            });


            modelBuilder.Entity<WaterProjectMainRoad>(entity =>
            {


                entity.ToTable("wb_wp_mainroads");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_wp_mr_id");
                entity.Property(e => e.Name).HasColumnName("wb_wp_mr_name");
                entity.Property(e => e.SabhaId).HasColumnName("wb_wp_mr_sabha_id");

                entity.HasIndex(mr => new { mr.Name, mr.SabhaId }).IsUnique();

                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_wp_mr_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_wp_mr_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_wp_mr_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_wp_mr_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_wp_mr_updated_by");
            });




            modelBuilder.Entity<WaterProjectSubRoad>(entity =>
            {

                entity.ToTable("wb_wp_subroads");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("wb_wp_sr_id");
                entity.Property(e => e.Name).HasColumnName("wb_wp_sr_name");
                entity.Property(e => e.WaterProjectId).HasColumnName("wb_wp_sr_water_project_id");
                entity.Property(e => e.MainRoadId).HasColumnName("wb_wp_sr_main_id");




                entity.Property(e => e.Status).HasColumnName("wb_wp_sr_status");

                // Foreign key referencing
                entity.HasOne(sr => sr.MainRoad)
                        .WithMany(mr => mr.SubRoads)
                        .HasForeignKey(sr => sr.MainRoadId)  // Rename the foreign key column
                        .HasConstraintName("fk_wp_subroad_wp_mainRoad");

                entity.HasIndex(sr => new { sr.Name, sr.MainRoadId }).IsUnique();

                // Foreign key referencing

                entity.HasOne(wpgd => wpgd.WaterProject)
                .WithMany(wp => wp.SubRoads)
                .HasForeignKey(wpgd => wpgd.WaterProjectId) // Rename the foreign key column
                    .HasConstraintName("fk_wp_subroad_wp_project_id");

                // mandatory fields for entity

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_wp_sr_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_wp_sr_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_wp_sr_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_wp_sr_updated_by");
            });



            modelBuilder.Entity<WaterProjectGnDivision>(entity =>
            {
                entity.ToTable("wb_waterproject_gndivisions");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("wb_wp_gnd_id");
                entity.Property(e => e.WaterProjectId).HasColumnName("wb_waterprojet_id");
                entity.Property(e => e.ExternalGnDivisionId).HasColumnName("wb_wp_ex_gnd_id");

                entity.HasKey(wpgd => new { wpgd.WaterProjectId, wpgd.ExternalGnDivisionId });

                entity.Ignore(e => e.WaterProject);
                entity.Ignore(e => e.GnDivision);

                // Foreign key referencing

                entity.HasOne(wpgd => wpgd.WaterProject)
                    .WithMany(wp => wp.WaterProjectGnDivisions)
                    .HasForeignKey(wpgd => wpgd.WaterProjectId)
                    .HasConstraintName("fk_wp_gnd_wp_project_id");



                // mandatory fields for entity

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_wp_gnd_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_wp_gnd_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_wp_gnd_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_wp_gnd_updated_by");
            });



            modelBuilder.Entity<MeterReaderAssign>(entity =>
            {
                entity.ToTable("wb_meter_reader_assigns");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("wb_wp_mras_id");
                entity.Property(e => e.MeterReaderId).HasColumnName("wb_mras_reader_id");
                entity.Property(e => e.SubRoadId).HasColumnName("wb_mras_subroad_id");



                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_wp_mras_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_wp_mras_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_wp_mras_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_wp_mras_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_wp_mras_updated_by");
            });

            modelBuilder.Entity<MeterConnectInfo>(entity =>
            {
                entity.ToTable("wb_meter_connection_info");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasIndex(e => e.ConnectionId).IsUnique();
                entity.Property(e => e.ConnectionId).HasColumnName("wb_wp_mci_conn_id");

                entity.Property(e => e.ConnectionNo).HasColumnName("wb_mci_conn_no");
                entity.Property(e => e.MeterNo).HasColumnName("wb_mci_meter_no");
                //entity.Property(e => e.InstallDate).HasColumnName("wb_mci_install_date");
                entity.Property(e => e.SubRoadId).HasColumnName("wb_mci_subroad_id");
                entity.Property(e => e.OrderNo).HasColumnName("wb_mci_order_no");
                entity.Property(e => e.IsAssigned).HasColumnName("wb_mci_is_assign");



                // Foreign key referencing
                entity.HasOne(mci => mci.WaterProjectSubRoad)
                        .WithMany(sr => sr.MeterConnectInfos)
                        .HasForeignKey(mci => mci.SubRoadId)  // Rename the foreign key column
                        .HasConstraintName("fk_wp_meter_connectInfo_wp_subRoad");


                entity.HasOne(w => w.WaterConnection)
                   .WithOne(m => m.MeterConnectInfo)
                   .HasForeignKey<WaterConnection>(w => w.ConnectionId);

                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_wp_mci_status");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_wp_mci_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_wp_mci_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_wp_mci_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_wp_mci_updated_by");
            });


            modelBuilder.Entity<NumberSequence>(entity =>
            {
                entity.ToTable("wb_number_sequence");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.HasIndex(e => e.OfficeId).IsUnique();
                entity.Property(e => e.OfficeId).HasColumnName("wb_office_id");

                entity.Property(e => e.CoreNumber).HasColumnName("wb_core_no");
                entity.Property(e => e.ApplicationNumber).HasColumnName("wb_application_no");




                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_wp_ns_status");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_wp_ns_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_wp_ns_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_wp_ns_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_wp_ns_updated_by");
            });



            modelBuilder.Entity<VoteAssign>(entity =>
            {
                entity.ToTable("wb_vote_assigns");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("wb_vas_id");
                entity.HasIndex(e => new { e.WaterProjectId, e.PaymentCategoryId }).IsUnique();
                entity.Property(e => e.WaterProjectId).HasColumnName("wb_vas_water_project_id");
                entity.Property(e => e.PaymentCategoryId).HasColumnName("wb_vas_payment_category_Id");
                entity.Property(e => e.vote).HasColumnName("wb_vas_vote");
                entity.Ignore(e => e.voteAssignmentDetails);




                // Foreign key referencing
                entity.HasOne(vt => vt.PaymentCategory)
                        .WithMany(pc => pc.VoteAssigns)
                        .HasForeignKey(vt => vt.PaymentCategoryId)  // Rename the foreign key column
                        .HasConstraintName("fk_wp_vt_pcat");


                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_wp_vas_status");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_wp_vas_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_wp_vas_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_wp_vas_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_wp_vas_updated_by");
                entity.Property(e => e.VoteDetailsId).HasColumnName("wb_vote_detail_id");
            });


            modelBuilder.Entity<PaymentCategory>(entity =>
            {
                entity.ToTable("wb_payment_category");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("wb_pay_cat_id");
                entity.Property(e => e.Description).HasColumnName("wb_pay_cat_desc");
                entity.Property(e => e.SabhaId).HasColumnName("wb_pay_cat_sabhaId");



                entity.Property(e => e.Status).HasColumnName("wb_pay_cat_status");
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_pay_cat_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_pay_cat_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_pay_cat_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_pay_cat_updated_by");
            });



            modelBuilder.Entity<ApplicationForConnection>(entity =>
            {


                entity.ToTable("wb_application_for_connections");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.ApplicationNo).HasColumnName("wb_afc_application_no");
                entity.Property(e => e.PartnerId).HasColumnName("wb_afc_partner_id");
                entity.Property(e => e.BillingId).HasColumnName("wb_afc_billing_id");

                entity.Property(e => e.SubRoadId).HasColumnName("wb_afc_subroad_id");
                entity.Property(e => e.RequestedNatureId).HasColumnName("wb_afc_req_nature_id");
                entity.Property(e => e.RequestedConnectionId).HasColumnName("wb_afc_req_connection_id");
                entity.Property(e => e.OnlyBillingChange).HasColumnName("wb_afc_req_only_billing_change");
                entity.Property(e => e.IsApproved).HasColumnName("wb_afc_is_approved");
                entity.Property(e => e.Comment).HasColumnName("wb_afc_rejt_cmt");
                entity.Property(e => e.ApprovedBy).HasColumnName("wb_afc_approved_by");
                entity.Property(e => e.ApprovedAt).HasColumnName("wb_afc_approved_at");
                entity.Property(e => e.IsAssigned).HasDefaultValue(false).HasColumnName("wb_afc_is_assigned");

                entity.Property(e => e.ApplicationType).HasColumnName("wb_afc_conn_type");



                // Foreign key referencing
                entity.HasOne(afc => afc.SubRoad)
                        .WithMany(sr => sr.ApplicationForConnections)
                        .HasForeignKey(afc => afc.SubRoadId)  // Rename the foreign key column
                        .HasConstraintName("fk_wp_afc_subroad_id");




                entity.HasOne(afc => afc.Nature)
                        .WithMany(sr => sr.ApplicationForConnections)
                        .HasForeignKey(afc => afc.RequestedNatureId)  // Rename the foreign key column
                        .HasConstraintName("fk_wb_afc_req_nature_id");

                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_afc_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_afc_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_afc_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_afc_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_afc_updated_by");


                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
            });





            modelBuilder.Entity<ApplicationForConnectionDocument>(entity =>
            {


                entity.ToTable("wb_application_for_connections_documents");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.HasIndex(e => e.Id).IsUnique();
                entity.Property(e => e.Id).HasColumnName("wb_afc_doc_id");
                entity.Property(e => e.DocType).HasColumnName("wb_afc_doc_type");
                entity.Property(e => e.ApplicationNo).HasColumnName("wb_afc_doc_application_no");
                entity.Property(e => e.Uri).HasColumnName("wb_doc_uri");

                entity.Ignore(wp => wp.File);


                // Foreign key referencing
                entity.HasOne(afc => afc.ApplicationForConnection)
                        .WithMany(afc => afc.SubmittedDocuments)
                        .HasForeignKey(afc => afc.ApplicationNo)  // Rename the foreign key column
                        .HasConstraintName("fk_wp_doc_afwc_id");

                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_afc_doc_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_afc_doc_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_afc_doc_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_afc_doc_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_afc_doc_updated_by");
            });

            modelBuilder.Entity<WaterConnection>(entity =>
            {


                entity.ToTable("wb_water_connections");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_wc_id");
                entity.Property(e => e.ConnectionId).HasColumnName("wb_wc_connection_id");
                entity.Property(e => e.PartnerId).HasColumnName("wb_wc_partner_id");
                entity.Property(e => e.BillingId).HasColumnName("wb_wc_billing_id");
                entity.Property(e => e.SubRoadId).HasColumnName("wb_wc_subroad_id");
                entity.Property(e => e.OfficeId).HasColumnName("wb_wc_office_id");
                entity.Property(e => e.InstallDate).HasColumnName("wb_wc_install_date");

                entity.Property(e => e.ActiveNatureId).HasColumnName("wb_wc_active_nature_id");
                entity.Property(e => e.ActiveStatus).HasColumnName("wb_wc_active_sataus");

                entity.Property(e => e.NatureChangeRequest).HasColumnName("wb_wc_nature_change_request");
                entity.Property(e => e.StatusChangeRequest).HasColumnName("wb_wc_status_change_request");
                entity.Property(e => e.RunningOverPay).HasDefaultValue(0m).HasColumnName("wb_wc_running_overpay");
                entity.Property(e => e.RunningVatRate).HasDefaultValue(0m).HasColumnName("wb_wc_running_vat_rate");






                // Foreign key referencing
                entity.HasOne(afc => afc.SubRoad)
                        .WithMany(sr => sr.WaterConnections)
                        .HasForeignKey(afc => afc.SubRoadId)  // Rename the foreign key column
                        .HasConstraintName("fk_wp_wc_subroad_id");

                entity.HasOne(w => w.OpeningBalanceInformation)
                       .WithOne(o => o.WaterConnection)
                       .HasForeignKey<OpeningBalanceInformation>(o => o.WaterConnectionId);


                // Foreign key referencing
                entity.HasOne(wc => wc.ActiveNature)
                        .WithMany(n => n.WaterConnections)
                        .HasForeignKey(w => w.ActiveNatureId)  // Rename the foreign key column
                        .HasConstraintName("fk_wb_wc_active_nature_id");





                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_wc_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_wc_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_wc_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_wc_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_wc_updated_by");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
            });

            modelBuilder.Entity<WaterBillDocument>(entity =>
            {


                entity.ToTable("wb_wb_documents");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_doc_id");
                entity.Property(e => e.DocType).HasColumnName("wb_doc_type");
                entity.Property(e => e.Uri).HasColumnName("wb_doc_uri");
                entity.Property(e => e.ConnectionId).HasColumnName("wb_doc_connection_id");

                entity.Ignore(wp => wp.File);


                // Foreign key referencing
                entity.HasOne(doc => doc.WaterConnection)
                        .WithMany(wc => wc.Documents)
                        .HasForeignKey(doc => doc.ConnectionId)  // Rename the foreign key column
                        .HasConstraintName("fk_wp_doc_conn_id");



                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_doc_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_doc_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_doc_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_doc_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_doc_updated_by");
            });




            modelBuilder.Entity<WaterConnectionNatureLog>(entity =>
            {


                entity.ToTable("wb_wc_nature_log");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_wc_nlog_id");
                entity.Property(e => e.ConnectionId).HasColumnName("wb_wc_con_id");
                entity.Property(e => e.NatureId).HasColumnName("wb_nlog_nature_id");
                entity.Property(e => e.Comment).HasColumnName("wb_wc_nlog_comment");

                entity.Property(e => e.Action).HasColumnName("wb_nlog_action");
                entity.Property(e => e.ActionBy).HasColumnName("wb_nlog_action_by");
                entity.Property(e => e.ActionNote).HasColumnName("wb_slog_action_note");
                entity.Property(e => e.ActivatedDate).HasColumnName("wb_nlog_activated_date");


                // Foreign key referencing
                entity.HasOne(wcnlog => wcnlog.WaterConnection)
                        .WithMany(wc => wc.NatureInfos)
                        .HasForeignKey(afc => afc.ConnectionId)  // Rename the foreign key column
                        .HasConstraintName("fk_wp_wc_nlog_id");



                // mandatory fields for entity

                entity.Property(e => e.Status).HasColumnName("wb_nlog_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_nlog_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_nlog_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_nlog_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_nlog_updated_by");
            });



            modelBuilder.Entity<WaterConnectionStatusLog>(entity =>
            {


                entity.ToTable("wb_wc_connection_status_log");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_wc_slog_id");
                entity.Property(e => e.ConnectionId).HasColumnName("wb_wc_con_id");
                entity.Property(e => e.ConnectionStatus).HasColumnName("wb_wc_slog_conn_satus");
                entity.Property(e => e.Comment).HasColumnName("wb_wc_slog_comment");

                entity.Property(e => e.Action).HasColumnName("wb_slog_action");
                entity.Property(e => e.ActionBy).HasColumnName("wb_slog_action_by");
                entity.Property(e => e.ActionNote).HasColumnName("wb_slog_action_note");
                entity.Property(e => e.ActivatedDate).HasColumnName("wb_slog_activated_date");

                // Foreign key referencing
                entity.HasOne(wcnlog => wcnlog.WaterConnection)
                        .WithMany(wc => wc.StatusInfos)
                        .HasForeignKey(afc => afc.ConnectionId)  // Rename the foreign key column
                        .HasConstraintName("fk_wp_wc_slog_id");


                entity.Property(e => e.Status).HasColumnName("wb_slog_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_slog_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_slog_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_slog_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_slog_updated_by");
            });


            modelBuilder.Entity<OpeningBalanceInformation>(entity =>
            {


                entity.ToTable("wb_open_balance_info");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_opn_bal_info_id");
                entity.Property(e => e.WaterConnectionId).HasColumnName("wb_opn_bal_info_wcon_id");
                entity.Property(e => e.Month).HasColumnName("wb_opn_bal_month");

                entity.Property(e => e.LastYearArrears).HasColumnName("wb_opn_bal_info_ly_arrears");

                entity.Property(e => e.LastMeterReading).HasColumnName("wb_opn_bal_info_lm_reading");

                entity.Property(e => e.BalanceIdForLastYearArrears).HasColumnName("wb_opn_bal_bal_id_for_last_year_arrears");
                entity.Property(e => e.BalanceIdForCurrentBalance).HasColumnName("wb_opn_bal_bal_id_for_current_year_bal");
                entity.Property(e => e.IsProcessed).HasColumnName("wb_opn_bal_is_processed");
                entity.Property(e => e.Year).HasColumnName("wb_opn_bal_year");
                entity.Property(e => e.MonthlyBalance).HasColumnName("wb_opn_bal_monthly");

                //entity.HasIndex(o => o.WaterConnectionId);



                entity.Property(e => e.Status).HasColumnName("wb_opn_bal_info_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_opn_bal_info_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_opn_bal_info_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_opn_bal_info_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_opn_bal_info_updated_by");



            });



            modelBuilder.Entity<OBLIApprovalStatus>(entity =>
            {

                entity.ToTable("wb_open_balance_approval_status");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_obli_aprl_sts_id");
                entity.Property(e => e.OpenBalStatus).HasColumnName("wb_obli_aprl_status");

                entity.Property(e => e.Comment).HasColumnName("wb_obli_aprl_sts_comment");

                //Foreign key referencing
                entity.HasOne(obli => obli.OpeningBalanceInformation)
                        .WithMany(e => e.OBLIApprovalStatus)
                        .HasForeignKey(obli => obli.OpnBalInfoId)  // Rename the foreign key column
                        .HasConstraintName("fk_opn_bal_info_aprl_sts_id");


                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_opn_bal_info_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_opn_bal_info_created_by");


            });



            modelBuilder.Entity<ConnectionAuditLog>(entity =>
            {

                entity.ToTable("wb_connection_audit_log");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_atl_id");
                entity.Property(e => e.WaterConnectionId).HasColumnName("wb_atl_wc_id");
                entity.Property(e => e.Action).HasColumnName("wb_atl_action");

                entity.Property(e => e.Timestamp).HasColumnName("wb_atl_time_stamp");
                entity.Property(e => e.EntityType).HasColumnName("wb_entity_type");


                entity.Property(e => e.PartnerId).HasColumnName("wb_alt_partner_id");
                entity.Property(e => e.BillingId).HasColumnName("wb_alt_billing_id");
                entity.Property(e => e.SubRoadId).HasColumnName("wb_alt_subroad_id");
                entity.Property(e => e.OfficeId).HasColumnName("wb_alt_office_id");

                entity.Property(e => e.ActiveNatureId).HasColumnName("wb_alt_active_nature_id");
                entity.Property(e => e.ActiveStatus).HasColumnName("wb_alt_active_sataus");

                //Foreign key referencing
                entity.HasOne(alt => alt.WaterConnection)
                        .WithMany(e => e.ConnectionAuditLogs)
                        .HasForeignKey(obli => obli.WaterConnectionId)  // Rename the foreign key column
                        .HasConstraintName("fk_atl_wc_id");


                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_atl_time_stamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ActionBy).HasColumnName("wb_action_by");


            });

            modelBuilder.Entity<WaterConnectionBalance>(entity =>
            {


                entity.ToTable("wb_balances");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_bal_id");
                entity.Property(e => e.WcPrimaryId).HasColumnName("wb_bal_wc_primary_id");
                entity.Property(e => e.ConnectionNo).HasColumnName("wb_bal_mci_conn_no");
                entity.Property(e => e.BarCode).HasColumnName("wb_bal_bar_code");
                entity.Property(e => e.InvoiceNo).HasColumnName("wb_bal_invoice_no");
                entity.Property(e => e.Year).HasColumnName("wb_bal_year");
                entity.Property(e => e.Month).HasColumnName("wb_bal_month");
                entity.Property(e => e.FromDate).HasColumnName("wb_bal_from_date");
                entity.Property(e => e.ToDate).HasColumnName("wb_bal_to_date");
                entity.Property(e => e.ReadBy).HasColumnName("wb_bal_read_by");
                entity.Property(e => e.BillProcessDate).HasColumnName("wb_bal_bill_process_date");

                entity.Property(e => e.MeterNo).HasColumnName("wb_bal_meter_no");
                entity.Property(e => e.MeterCondition).HasColumnName("wb_bal_meter-condition");
                entity.Property(e => e.PreviousMeterReading).HasColumnName("wb_bal_prev_meter_reading");
                entity.Property(e => e.ThisMonthMeterReading).HasColumnName("wb_bal_this_month_meter_reading");

                entity.Property(e => e.WaterCharge).HasDefaultValue(0m).HasColumnName("wb_bal_water_charge");
                entity.Property(e => e.FixedCharge).HasDefaultValue(0m).HasColumnName("wb_bal_fix_charge");

                entity.Property(e => e.VATRate).HasDefaultValue(0m).HasColumnName("wb_bal_vat_rate");
                entity.Property(e => e.VATAmount).HasDefaultValue(0m).HasColumnName("wb_bal_vat_amount");
                entity.Property(e => e.ThisMonthCharge).HasColumnName("wb_bal_this_month_charges");
                entity.Property(e => e.ThisMonthChargeWithVAT).HasColumnName("wb_bal_this_month_charges_with_vat");



                entity.Property(e => e.TotalDue).HasColumnName("wb_bal_total_due");


                entity.Property(e => e.ByExcessDeduction).HasDefaultValue(0m).HasColumnName("wb_bal_by_excess_deduction");
                entity.Property(e => e.OnTimePaid).HasDefaultValue(0m).HasColumnName("wb_bal_ontime_paid");
                entity.Property(e => e.LatePaid).HasDefaultValue(0m).HasColumnName("wb_bal_late_paid");

                entity.Property(e => e.OverPay).HasDefaultValue(0m).HasDefaultValue(0m).HasColumnName("wb_bal_over_pay");


                entity.Property(e => e.Payments).HasDefaultValue(0m).HasColumnName("wb_bal_payments");



                entity.Property(e => e.IsCompleted).HasDefaultValue(false).HasColumnName("wb_bal_is_completed");
                entity.Property(e => e.IsFilled).HasDefaultValue(false).HasColumnName("wb_bal_is_filled");
                entity.Property(e => e.IsProcessed).HasDefaultValue(false).HasColumnName("wb_bal_is_processed");

                //to print info 


                entity.Property(e => e.LastBillYearMonth).HasColumnName("wb_bal_print_last_bill_year_month");
                entity.Property(e => e.PrintBillingDetails).HasColumnName("wb_bal_print_billing_details");
                entity.Property(e => e.PrintBalanceBF).HasColumnName("wb_bal_print_balance_b_f");
                entity.Property(e => e.PrintLastMonthPayments).HasColumnName("wb_bal_print_last_month_payments");
                entity.Property(e => e.PrintLastBalance).HasColumnName("wb_bal_print_last_balance");
                entity.Property(e => e.NumPrints).HasColumnName("wb_bal_num_print");
                entity.Property(e => e.CalculationString).HasColumnName("wb_bal_cal_string");
                entity.Property(e => e.NoOfPayments).HasDefaultValue(0).HasColumnName("wb_bal_no_of_payments");
                entity.Property(e => e.NoOfCancels).HasDefaultValue(0).HasColumnName("wb_bal_no_of_cancels");

                entity.Property(e => e.LastYearArrears).HasDefaultValue(0m).HasColumnName("wb_bal_ly_arrears");
                entity.Property(e => e.ThisYearArrears).HasDefaultValue(0m).HasColumnName("wb_bal_ty_arrears");
                entity.Property(e => e.OverPayment).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_overpay");

                //entity.HasIndex(o => o.WaterConnectionId);


                //Foreign key referencing
                entity.HasOne(b => b.WaterConnection)
                        .WithMany(e => e.Balances)
                        .HasForeignKey(b => b.WcPrimaryId)  // Rename the foreign key column
                        .HasConstraintName("fk_bal_wc_id");

                entity.HasIndex(e => new { e.WcPrimaryId, e.Year, e.Month }).IsUnique();


                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_bal_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_bal_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_bal_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_bal_updated_by");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();

            });



            modelBuilder.Entity<WaterConnectionBalanceHistory>(entity =>
            {


                entity.ToTable("wb_balance_history");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_bal_hstry_id");
                entity.Property(e => e.BalanceId).HasColumnName("wb_bal_hstry_bal_id");
                entity.Property(e => e.TransactionType).HasColumnName("wb_bal_hstry_tr_type");

                entity.Property(e => e.ActionAt)
               .HasColumnName("wb_bal_hstry_action_at");

                entity.Property(e => e.ActionBy).HasColumnName("wb_bal_hstry_action_by");

                entity.Property(e => e.WcPrimaryId).HasColumnName("wb_bal_hstry_wc_primary_id");
                entity.Property(e => e.ConnectionNo).HasColumnName("wb_bal_hstry_mci_conn_no");
                entity.Property(e => e.BarCode).HasColumnName("wb_bal_hstry_bar_code");
                entity.Property(e => e.InvoiceNo).HasColumnName("wb_bal_hstry_invoice_no");
                entity.Property(e => e.Year).HasColumnName("wb_bal_hstry_year");
                entity.Property(e => e.Month).HasColumnName("wb_bal_hstry_month");
                entity.Property(e => e.FromDate).HasColumnName("wb_bal_hstry_from_date");
                entity.Property(e => e.ToDate).HasColumnName("wb_bal_hstry_to_date");
                entity.Property(e => e.ReadBy).HasColumnName("wb_bal_hstry_read_by");
                entity.Property(e => e.BillProcessDate).HasColumnName("wb_bal_hstry_bill_process_date");

                entity.Property(e => e.MeterNo).HasColumnName("wb_bal_hstry_meter_no");
                entity.Property(e => e.MeterCondition).HasColumnName("wb_bal_hstry_meter-condition");
                entity.Property(e => e.PreviousMeterReading).HasColumnName("wb_bal_hstry_prev_meter_reading");
                entity.Property(e => e.ThisMonthMeterReading).HasColumnName("wb_bal_hstry_this_month_meter_reading");

                entity.Property(e => e.WaterCharge).HasDefaultValue(0m).HasColumnName("wb_bal_hstry_water_charge");
                entity.Property(e => e.FixedCharge).HasDefaultValue(0m).HasColumnName("wb_bal_hstry_fix_charge");

                entity.Property(e => e.VATRate).HasDefaultValue(0m).HasColumnName("wb_bal_hstry_vat_rate");
                entity.Property(e => e.VATAmount).HasDefaultValue(0m).HasColumnName("wb_bal_hstry_vat_amount");
                entity.Property(e => e.ThisMonthCharge).HasColumnName("wb_bal_hstry_this_month_charges");
                entity.Property(e => e.ThisMonthChargeWithVAT).HasColumnName("wb_bal_hstry_this_month_charges_with_vat");



                entity.Property(e => e.TotalDue).HasColumnName("wb_bal_hstry_total_due");


                entity.Property(e => e.ByExcessDeduction).HasDefaultValue(0m).HasColumnName("wb_bal_hstry_by_excess_deduction");
                entity.Property(e => e.OnTimePaid).HasDefaultValue(0m).HasColumnName("wb_bal_hstry_ontime_paid");
                entity.Property(e => e.LatePaid).HasDefaultValue(0m).HasColumnName("wb_bal_hstry_late_paid");

                entity.Property(e => e.OverPay).HasDefaultValue(0m).HasDefaultValue(0m).HasColumnName("wb_bal_hstry_over_pay");


                entity.Property(e => e.Payments).HasDefaultValue(0m).HasColumnName("wb_bal_hstry_payments");



                entity.Property(e => e.IsCompleted).HasDefaultValue(false).HasColumnName("wb_bal_hstry_is_completed");
                entity.Property(e => e.IsFilled).HasDefaultValue(false).HasColumnName("wb_bal_hstry_is_filled");
                entity.Property(e => e.IsProcessed).HasDefaultValue(false).HasColumnName("wb_bal_hstry_is_processed");

                //to print info 


                entity.Property(e => e.LastBillYearMonth).HasColumnName("wb_bal_hstry_print_last_bill_year_month");
                entity.Property(e => e.PrintBillingDetails).HasColumnName("wb_bal_hstry_print_billing_details");
                entity.Property(e => e.PrintBalanceBF).HasColumnName("wb_bal_hstry_print_balance_b_f");
                entity.Property(e => e.PrintLastMonthPayments).HasColumnName("wb_bal_hstry_print_last_month_payments");
                entity.Property(e => e.PrintLastBalance).HasColumnName("wb_bal_hstry_print_last_balance");
                entity.Property(e => e.NumPrints).HasColumnName("wb_bal_hstry_num_print");
                entity.Property(e => e.CalculationString).HasColumnName("wb_bal_hstry_cal_string");
                entity.Property(e => e.NoOfPayments).HasDefaultValue(0).HasColumnName("wb_bal_hstry_no_of_payments");
                entity.Property(e => e.NoOfCancels).HasDefaultValue(0).HasColumnName("wb_bal_hstry_no_of_cancels");



                //Foreign key referencing
                entity.HasOne(b => b.WaterConnection)
                        .WithMany(e => e.BalanceHistory)
                        .HasForeignKey(b => b.WcPrimaryId)  // Rename the foreign key column
                        .HasConstraintName("fk_bal_hstry_wc_id");

            });
            modelBuilder.Entity<WaterMonthEndReport>(entity =>
            {

                entity.ToTable("wb_balance_month_end_report");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("wb_bal_rpt_id");
                entity.Property(e => e.WcPrimaryId).HasColumnName("wb_bal_rpt_wcon_id");

                entity.Property(e => e.Year).HasColumnName("wb_bal_rpt_year");
                entity.Property(e => e.Month).HasColumnName("wb_bal_rpt_month");

                entity.Property(e => e.LastYearArrears).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_ly_arrears");
                entity.Property(e => e.LYABalanceVAT).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_lya_vat");

                entity.Property(e => e.ThisYearArrears).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_ty_arrears");
                entity.Property(e => e.TYABalanceVAT).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_tya_vat");


                entity.Property(e => e.TMCharge).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_tm_chrage");
                entity.Property(e => e.TMBalanceVAT).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_tm_vat");

                entity.Property(e => e.RemainOverPay).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_remain_overpay");
                entity.Property(e => e.RemainOverPayVat).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_remain_overpay_vat");

                entity.Property(e => e.ReceivedOverPay).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_received_overpay");
                entity.Property(e => e.ReceivedOverPayVAT).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_received_overpay_vat");

                entity.Property(e => e.MonthlyBill).HasDefaultValue(0m).HasColumnName("wb_bal_monthly_bill");
                entity.Property(e => e.MonthlyBillWithVat).HasDefaultValue(0m).HasColumnName("wb_bal_monthly_bill_vat");



                entity.Property(e => e.LYArrearsPaying).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_ly_paying");

                entity.Property(e => e.TYArrearsPaying).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_ty_paying");


                entity.Property(e => e.TMPaying).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_tm_paying");
                entity.Property(e => e.OverPaying).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_over_paying");
                entity.Property(e => e.OverPaymentWithVat).HasDefaultValue(0m).HasColumnName("wb_bal_rpt_overpay_vat");




                //Foreign key referencing
                entity.HasOne(b => b.WaterConnection)
                        .WithMany(e => e.WaterMonthEndReports)
                        .HasForeignKey(b => b.WcPrimaryId)  // Rename the foreign key column
                        .HasConstraintName("fk_bal_rpt_wc_id");



                entity.Property(e => e.Status).HasColumnName("wb_bal_rpt_status");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("wb_bal_rpt_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("wb_bal_rpt_updated_at");

                entity.Property(e => e.CreatedBy).HasColumnName("wb_bal_rpt_created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("wb_bal_rpt_updated_by");


            });

            }

    }


}
