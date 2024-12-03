using CAT20.Core.Models.Control;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using Microsoft.Extensions.Logging;
using CAT20.Core.Models.Common;
using CAT20.Core.Models.OnlienePayment;
using System.Net;


namespace CAT20.Data
{
    public partial class ControlDbContext : DbContext
    {
        public ControlDbContext()
        {
        }

        public ControlDbContext(DbContextOptions<ControlDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppCategory> AppCategories { get; set; }
        public virtual DbSet<BankDetail> BankDetails { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Month> Months { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<OfficeType> OfficeTypes { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Sabha> Sabhas { get; set; }
        public virtual DbSet<SelectedLanguage> SelectedLanguages { get; set; }
        public virtual DbSet<Year> Years { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<EmailConfiguration> EmailConfigurations { get; set; }
        public virtual DbSet<EmailOutBox> EmailOutBoxes { get; set; }
        public virtual DbSet<GnDivisions> GnDivisions { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Partner> Partner { get; set; }
        public virtual DbSet<PartnerTitle> PartnerTitle { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<PaymentNbt> PaymentNbt { get; set; }
        public virtual DbSet<PaymentVat> PaymentVat { get; set; }
        public virtual DbSet<DocumentSequenceNumber> DocumentSequenceNumber { get; set; }
        public virtual DbSet<TaxType> TaxTypes { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<BusinessTaxes> BusinessTaxes { get; set; }
        public virtual DbSet<BusinessPlace> BusinessPlaces { get; set; }
        public virtual DbSet<SMSConfiguration> SMSConfigurations { get; set; }
        public virtual DbSet<SMSOutBox> SMSOutBoxes { get; set; }
        
        public virtual DbSet<PartnerMobile> PartnerMobile { get; set; }
        public virtual DbSet<PartnerDocument> PartnerDocuments { get; set; }
        public virtual DbSet<PermittedThirdPartyAssessments> PermittedThirdPartyAssessments { get; set; }
        public virtual DbSet<BankBranch> BankBranches { get; set; }

        public virtual DbSet<Agents> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_sinhala_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<AppCategory>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("cd_app_category");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("cd_app_cat_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("cd_app_cat_name");

                entity.Property(e => e.Status).HasColumnName("cd_app_cat_status");

                entity.Property(e => e.CreatedAt).HasColumnName("cd_app_cat_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("cd_app_cat_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<BankDetail>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("cd_bank_details");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("cd_bd_id");

                entity.HasIndex(e => e.BankCode).IsUnique();

                entity.Property(e => e.BankCode).HasColumnName("cd_bank_code");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("cd_bd_name");

                entity.Property(e => e.Status).HasColumnName("cd_bd_status").HasDefaultValueSql("'1'"); 

                entity.Property(e => e.CreatedAt).HasColumnName("cd_bd_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("cd_bd_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

           modelBuilder.Entity<BankBranch>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("cd_bank_branches");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasIndex(e => e.BankCode);

                entity.Property(e => e.ID).HasColumnName("cd_bb_id");

                entity.Property(e => e.BankCode).HasColumnName("cd_bb_bank_code");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(255)
                    .HasColumnName("cd_bb_branch_name");

                entity.Property(e => e.BranchCode).HasColumnName("cd_bb_branch_code");

                entity.Property(e => e.BranchAddress)
                    .HasColumnName("cd_bb_branch_address");

                entity.Property(e => e.TelNo1)
                    .HasMaxLength(50)
                    .HasColumnName("cd_bb_tel1");

                entity.Property(e => e.TelNo2)
                    .HasMaxLength(50)
                    .HasColumnName("cd_bb_tel2");

                entity.Property(e => e.TelNo3)
                    .HasMaxLength(50)
                    .HasColumnName("cd_bb_tel3");

                entity.Property(e => e.TelNo4)
                    .HasMaxLength(50)
                    .HasColumnName("cd_bb_tel4");

                entity.Property(e => e.FaxNo)
                   .HasMaxLength(50)
                   .HasColumnName("cd_bb_fax_no");

                entity.Property(e => e.District)
                   .HasMaxLength(100)
                   .HasColumnName("cd_bb_district");

                entity.Property(e => e.Status)
                   .HasMaxLength(1)
                   .HasColumnName("cd_bb_status");
                entity.Property(e => e.Status).HasColumnName("cd_d_status").HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Bank)
                .WithMany(p => p.bankBranch)
                .HasForeignKey(d => d.BankCode)
                .HasPrincipalKey(p => p.BankCode) 
                .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.CreatedAt).HasColumnName("cd_d_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("cd_d_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Agents>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("cd_agents");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");


                entity.Property(e => e.Id).HasColumnName("cd_agent_id");

                entity.Property(e => e.AgentCode).HasColumnName("cd_agent_code");
                entity.Property(e => e.Reference).HasColumnName("cd_agent_reference");
                entity.Property(e => e.BranchId).HasColumnName("cd_agent_branch_id");
                entity.Property(e => e.BankAccountNumber).HasColumnName("cd_agent_bank_account");
                entity.Property(e => e.SabhaId).HasColumnName("cd_agent_sabha_id");

                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("cd_agent_status");
                entity.Property(e => e.CreatedBy).HasColumnName("cd_agent_create_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("cd_agent_update_by");

                entity.Property(e => e.CreatedAt).HasColumnName("cd_agent_create_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("cd_agent_update_at");
                entity.Property(e => e.SystemActionAt).HasColumnName("cd_agent_system_at");
                entity.Property(e => e.RowVersion).HasColumnName("cd_agent_row_version");


                entity.HasOne(d => d.BankBranch)
               .WithMany(p => p.Agents)
               .HasForeignKey(d => d.BranchId)
               .HasConstraintName("cd_agent_bank_branch_id")
               .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("cd_district");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasIndex(e => e.ProvinceID, "fk_cd_d_cd_p_id");

                entity.Property(e => e.ID).HasColumnName("cd_d_id");

                entity.Property(e => e.ProvinceID).HasColumnName("cd_d_cd_p_id");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("cd_d_name_english");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("cd_d_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("cd_d_name_tamil");

                entity.Property(e => e.Status).HasColumnName("cd_d_status");

                entity.HasOne(d => d.province)
                    .WithMany(p => p.district)
                    .HasForeignKey(d => d.ProvinceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cd_d_cd_p_id");

                entity.Property(e => e.CreatedAt).HasColumnName("cd_d_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("cd_d_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("cd_gender");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("cd_gender_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("cd_gender");

                entity.Property(e => e.Status).HasColumnName("cd_status");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("cd_languages");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID)
                    .ValueGeneratedNever()
                    .HasColumnName("cd_languages_id");

                entity.Property(e => e.Status).HasColumnName("cd_language_status");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("cd_languages_name");
            });

            modelBuilder.Entity<Month>(entity =>
            {
                entity.ToTable("cd_month");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID)
                    .ValueGeneratedNever()
                    .HasColumnName("cd_month_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("cd_month");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("cd_office");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasIndex(e => e.SabhaID, "fk_cd_o_cd_s_id");

                entity.HasIndex(e => e.OfficeTypeID, "fk_cd_o_office_type_id");

                entity.Property(e => e.ID).HasColumnName("cd_o_id");

                entity.Property(e => e.SabhaID).HasColumnName("cd_o_cd_s_id");
                entity.Property(e => e.Latitude).HasColumnName("cd_o_latitude");
                entity.Property(e => e.Longitude).HasColumnName("cd_o_longitude");

                entity.Property(e => e.CreatedDate).HasColumnName("cd_o_create_date");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("cd_o_name_english");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("cd_o_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("cd_o_name_tamil");

                entity.Property(e => e.OfficeTypeID).HasColumnName("cd_o_office_type_id");

                entity.Property(e => e.Status).HasColumnName("cd_o_status");
                entity.Property(e => e.Code)
                   .HasMaxLength(100)
                   .HasColumnName("cd_o_code");

                entity.HasOne(d => d.sabha)
                    .WithMany(p => p.office)
                    .HasForeignKey(d => d.SabhaID)
                    .HasConstraintName("fk_cd_o_cd_s_id");

                entity.HasOne(d => d.officeType)
                    .WithMany(p => p.office)
                    .HasForeignKey(d => d.OfficeTypeID)
                    .HasConstraintName("fk_cd_o_office_type_id");

                entity.Property(e => e.Status).HasColumnType("int(1)").HasColumnName("cd_o_status").HasDefaultValueSql("'1'");
                entity.Property(e => e.CreatedAt).HasColumnName("cd_o_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("cd_o_updated_at");
            });

            modelBuilder.Entity<OfficeType>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("cd_office_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("cd_ot_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("cd_ot_name");

                entity.Property(e => e.Status).HasColumnName("cd_ot_status");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("cd_province");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("cd_p_id");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("cd_p_name_english");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("cd_p_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("cd_p_name_tamil");

                entity.Property(e => e.Status).HasColumnName("cd_p_status");
            });

            modelBuilder.Entity<Sabha>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("cd_sabha");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasIndex(e => e.DistrictID, "fk_cd_s_cd_d_id");

                entity.Property(e => e.ID).HasColumnName("cd_s_id");

                entity.Property(e => e.AddressEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_address_english");

                entity.Property(e => e.AddressSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_address_sinhala");

                entity.Property(e => e.AddressTamil)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_address_tamil");

                entity.Property(e => e.DistrictID).HasColumnName("cd_s_cd_d_id");

                entity.Property(e => e.AccountSystemVersionId).HasColumnName("cd_s_account_system_versionid").HasDefaultValueSql("'0'");
                entity.Property(e => e.IsFinalAccountsEnabled)
                   .HasColumnName("cd_s_is_final_accounts_enabled")
                   .HasDefaultValueSql("'0'");

                entity.Property(e => e.ChartOfAccountVersionId)
                   .HasColumnName("cd_s_chart_of_acc_version_id").HasDefaultValueSql("'0'");

                entity.Property(e => e.SabhaType).HasColumnName("cd_s_sabha_type");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_code");

                entity.Property(e => e.CreatedDate).HasColumnName("cd_s_create_date");

                entity.Property(e => e.LogoPath)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_logo_path");

                entity.Property(e => e.NameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_name_english");

                entity.Property(e => e.NameSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_name_sinhala");

                entity.Property(e => e.NameTamil)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_name_tamil");

                entity.Property(e => e.Status).HasColumnName("cd_s_status");

                entity.Property(e => e.Telephone1)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_tp_no1");

                entity.Property(e => e.Telephone2)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_tp_no2");

                entity.HasOne(d => d.district)
                    .WithMany(p => p.sabha)
                    .HasForeignKey(d => d.DistrictID)
                    .HasConstraintName("fk_cd_s_cd_d_id");

                entity.Property(e => e.Latitude).HasColumnName("cd_s_latitude");
                entity.Property(e => e.Longitude).HasColumnName("cd_s_longitude");

                //entity.HasOne(d => d.SabhaTypeModel)
                //    .WithMany(p => p.Sabhas)
                //    .HasForeignKey(d => d.SabhaType);

                entity.HasMany(e => e.office)
                    .WithOne(e => e.sabha)
                    .HasForeignKey(e => e.SabhaID);

                //entity.HasMany(e => e.partners)
                //    .WithOne(e => e.Sabha)
                //    .HasForeignKey(e => e.SabhaId);

                entity.Property(e => e.SecretarySignPath)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_secretary_sign_path");

                entity.Property(e => e.ChairmanSignPath)
                    .HasMaxLength(255)
                    .HasColumnName("cd_s_chairman_sign_path");

                entity.Property(e => e.Status).HasColumnType("int(1)").HasColumnName("cd_s_status").HasDefaultValueSql("'1'");
                entity.Property(e => e.CreatedAt).HasColumnName("cd_s_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("cd_s_updated_at");
            });

            modelBuilder.Entity<SelectedLanguage>(entity =>
            {
                entity.ToTable("cd_selected_languages");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("cd_selected_languages_id");

                entity.Property(e => e.LanguageID).HasColumnName("cd_selected_languages_lang_id");

                entity.Property(e => e.SabhaID).HasColumnName("cd_selected_languages_sabha_id");

                entity.Property(e => e.Status).HasColumnName("cd_selected_languages_status");

                entity.Property(e => e.CreatedAt).HasColumnName("cd_selected_languages_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("cd_selected_languages_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Year>(entity =>
            {
                entity.ToTable("cd_year");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID)
                    .ValueGeneratedNever()
                    .HasColumnName("cd_year_id");

                entity.Property(e => e.Description).HasColumnName("cd_year");
            });

            modelBuilder.Entity<CustomerType>(entity =>
            {
                entity.HasKey(e => e.ID)
                    .HasName("PRIMARY");

                entity.ToTable("cdb_customer_types");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID).HasColumnName("cdb_cus_type_id");

                entity.Property(e => e.NameInEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("cdb_cus_type_name_in_english");

                entity.Property(e => e.NameInSinhala)
                    .HasMaxLength(255)
                    .HasColumnName("cdb_cus_type_name_in_sinhala");

                entity.Property(e => e.NameInTamil)
                    .HasMaxLength(255)
                    .HasColumnName("cdb_cus_type_name_in_tamil");

                entity.Property(e => e.Status).HasColumnName("cdb_cus_type_status");
            });

            modelBuilder.Entity<GnDivisions>(entity =>
            {
                entity.ToTable("cd_gn_divisions");

                entity.HasIndex(e => e.OfficeId, "cd_gn_divisions_FK");

                //entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id)
                 .HasColumnName("id")
                .ValueGeneratedOnAdd();  // Indicates that the value is generated on add (auto-increment)

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.OfficeId).HasColumnName("office_id");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();

                //entity.HasMany(p => p.Partners)
                //.WithOne(g => g.GnDivision)
                // .HasForeignKey(p => p.GnDivisionId);

            });

           

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("ir_session");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active");
                    //.HasDefaultValueSql("'1'");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.Rescue).HasColumnName("rescue");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Module)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("module");

                entity.Property(e => e.StartAt)
                    .HasColumnType("datetime")
                    .HasColumnName("start_at");

                entity.Property(e => e.StopAt)
                    .HasColumnType("datetime")
                    .HasColumnName("stop_at");

                entity.Property(e => e.RescueStartedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("rescue_started_at");

                entity.Property(e => e.OfficeId).HasColumnName("office_id");

                entity.Property(e => e.SessionDate)
                    .HasColumnName("session_date");

                entity.HasIndex(e => new { e.OfficeId, e.SessionDate }).IsUnique(true);

                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                //entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime");

            });


            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("res_application");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.ToTable("res_partner");

                entity.HasIndex(e => e.CreatedBy, "res_partner_FK");

                //entity.HasIndex(e => e.PartnerTitleId, "res_partner_FK_1");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.City)
                    .HasMaxLength(45)
                    .HasColumnName("city");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.IsEditable)
                    .IsRequired()
                    .HasColumnName("is_editable")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsTempory)
                   .IsRequired()
                   .HasColumnName("is_tempory")
                   .HasDefaultValueSql("'0'");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("mobile_number");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.NicNumber)
                    .HasMaxLength(45)
                    .HasColumnName("nic_number");

                entity.Property(e => e.PassportNo)
                    .HasMaxLength(10)
                    .HasColumnName("passport_number");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(45)
                    .HasColumnName("phone_number");

                //entity.Property(e => e.PartnerTitleId).HasColumnName("res_partner_title_id");

                entity.Property(e => e.Street1)
                    .HasColumnName("street_1");

                entity.Property(e => e.Street2)
                    .HasColumnName("street_2");

                entity.Property(e => e.Zip)
                    .HasMaxLength(45)
                    .HasColumnName("zip");

                entity.Property(e => e.GnDivisionId).HasColumnName("gn_division_id");

                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");

                //entity.Property(e => e.RIUserId)
                //   .HasColumnName("ri_user_id")
                //   .HasDefaultValueSql("'0'");

                entity.Property(e => e.IsPropertyOwner)
                   .HasColumnName("is_property_owner")
                   .HasDefaultValueSql("'0'");

                entity.Property(e => e.IsBusinessOwner)
                   .HasColumnName("is_business_owner")
                   .HasDefaultValueSql("'0'");

                entity.Property(e => e.IsBusiness)
                   .HasColumnName("is_business")
                   .HasDefaultValueSql("'0'");

                entity.Property(e => e.BusinessRegNo)
                   .HasColumnName("business_reg_no");

                entity.Property(e => e.ProfilePicPath)
                   .HasColumnName("profile_picture_path");

                // entity.HasOne(d => d.PartnerTitle)
                //     .WithMany(p => p.Partner)
                //     .HasForeignKey(d => d.PartnerTitleId)
                //     .OnDelete(DeleteBehavior.Cascade)
                //     .HasConstraintName("res_partner_FK_1");
                entity.HasMany(e => e.PartnerMobiles)
                    .WithOne(e => e.Partner)
                    .HasForeignKey(e => e.PartnerId);

                entity.HasMany(e => e.PermittedThirdPartyAssessments)
                    .WithOne(e => e.Partner)
                    .HasForeignKey(e => e.PartnerId);

                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();

            });
            
            modelBuilder.Entity<PartnerMobile>(entity =>
            {
                entity.ToTable("partner_mobile"); // Ensure correct table name

                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NIC)
                    .HasMaxLength(45)
                    .HasColumnName("nic_number");

                entity.Property(e => e.NickName)
                    .HasMaxLength(255)
                    .HasColumnName("nick_name");

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(45)
                    .HasColumnName("mobile_no");

                entity.HasIndex(e => new { e.MobileNo, e.PartnerId }).IsUnique(true);

                // Define the relationship with Partner table
                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.PartnerMobiles)
                    .HasForeignKey(d => d.PartnerId) // Assuming NIC is the foreign key in PartnerMobile
                    .OnDelete(DeleteBehavior.Cascade) // Adjust the delete behavior as needed
                    .HasConstraintName("FK_PartnerMobile_Partner_Id");


                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<PartnerDocument>(entity =>
            {
                entity.ToTable("partner_documents");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FileName)
                    .HasColumnName("file_name");

                entity.Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.DocumentType)
                    .HasColumnName("document_type");

                entity.Property(e => e.PartnerId)
                    .HasColumnName("partner_id");

                entity.Property(e => e.Status)
                   .HasColumnName("status")
                   .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.PartnerDocuments)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.Cascade); 


                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<PermittedThirdPartyAssessments>(entity =>
            {
                entity.ToTable("ptnr_permitted_trdparty_assmnts");

                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PartnerId)
                    .HasColumnName("partner_id");

                entity.Property(e => e.AssessmentId)
                    .HasColumnName("assmt_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Ignore(e => e.Assessment);

                entity.HasIndex(e => new { e.PartnerId, e.AssessmentId }).IsUnique(true);

                // Define the relationship with Partner table
                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.PermittedThirdPartyAssessments)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<PartnerTitle>(entity =>
            {
                entity.ToTable("res_partner_title");

                entity.HasIndex(e => e.Code, "res_partner_title_UN")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("code");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("res_payment_method");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<PaymentNbt>(entity =>
            {
                entity.ToTable("res_payment_nbt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AmountPercentage)
                    .HasPrecision(4, 2)
                    .HasColumnName("amount_percentage");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasColumnName("date_modified");
            });

            modelBuilder.Entity<PaymentVat>(entity =>
            {
                entity.ToTable("res_payment_vat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AmountPercentage)
                    .HasPrecision(4, 2)
                    .HasColumnName("amount_percentage");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("date_created");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasColumnName("date_modified");
            });

            modelBuilder.UseCollation("utf8mb4_sinhala_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<DocumentSequenceNumber>(entity =>
            {
                entity.ToTable("document_sequence_numbers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.Property(e => e.OfficeId).HasColumnName("office_id");

                entity.Property(e => e.LastIndex).HasColumnName("last_index");

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("prefix");

                entity.HasIndex(e => new { e.Year, e.OfficeId }).IsUnique(true);

                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
            });

            modelBuilder.Entity<TaxType>(entity =>
            {
                entity.ToTable("tax_types");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.ID)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("tax_type_name");
                entity.Property(e => e.IsMainTax).HasColumnName("is_main_tax");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.ToTable("res_businesses");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BusinessName).HasColumnName("business_name");
                entity.Property(e => e.BusinessSubOwners).HasColumnName("business_sub_owners");
                entity.Property(e => e.BusinessNatureId).HasColumnName("business_nature");
                entity.Property(e => e.BusinessSubNatureId).HasColumnName("business_sub_nature");
                entity.Property(e => e.BusinessStartDate).HasColumnName("business_start_date");
                entity.Property(e => e.BusinessRegNo).HasColumnName("business_reg_no");
                entity.Property(e => e.TaxTypeId).HasColumnName("tax_type");
                entity.Property(e => e.BusinessTelNo).HasColumnName("business_tel_no");
                entity.Property(e => e.BusinessEmail).HasColumnName("business_email");
                entity.Property(e => e.BusinessWeb).HasColumnName("business_web");
                entity.Property(e => e.NoOfEmployees).HasColumnName("no_of_employees");
                //entity.Property(e => e.LastYearValue).HasColumnName("last_year_value");
                //entity.Property(e => e.CurrentYear).HasColumnName("current_year");
                //entity.Property(e => e.OtherCharges).HasColumnName("other_charges");
                //entity.Property(e => e.AnnualValue).HasColumnName("annual_value");
                //entity.Property(e => e.TaxAmountByNature).HasColumnName("tax_amount_by_nature");
                //entity.Property(e => e.TaxAmount).HasColumnName("tax_amount");
                //entity.Property(e => e.TotalTaxAmount).HasColumnName("total_tax_amount");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.BusinessOwnerId).HasColumnName("business_owner_id");
                entity.Property(e => e.PropertyOwnerId).HasColumnName("property_owner_id");
                entity.Property(e => e.BusinessPlaceId).HasColumnName("business_place_id");
                //entity.Property(e => e.ApplicationNo).HasColumnName("application_no");
                //entity.Property(e => e.LicenseNo).HasColumnName("license_no");
                //entity.Property(e => e.TaxState).HasColumnName("tax_state");
                entity.Ignore(e => e.BusinessNature);
                entity.Ignore(e => e.BusinessSubNature);
                entity.Ignore(e => e.TaxType);
                entity.Ignore(e => e.BusinessOwner);
                entity.Ignore(e => e.BusinessTaxes);
                entity.Ignore(e => e.PropertyOwner);
                entity.Ignore(e => e.BusinessPlace);
            });

            modelBuilder.Entity<BusinessTaxes>(entity =>
            {
                entity.ToTable("business_taxes");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");
                entity.HasIndex(e => e.BusinessId, "business_taxes_FK");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BusinessId).HasColumnName("business_id");
                entity.Property(e => e.CurrentYear).HasColumnName("current_year");
                entity.Property(e => e.ApplicationNo).HasColumnName("application_no");
                entity.Property(e => e.LicenseNo).HasColumnName("license_no");
                entity.Property(e => e.LastYearValue).HasColumnName("last_year_value");
                entity.Property(e => e.OtherCharges).HasColumnName("other_charges");
                entity.Property(e => e.AnnualValue).HasColumnName("annual_value");
                entity.Property(e => e.TaxAmountByNature).HasColumnName("tax_amount_by_nature");
                entity.Property(e => e.TaxAmount).HasColumnName("tax_amount");
                entity.Property(e => e.TotalTaxAmount).HasColumnName("total_tax_amount");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.TaxState).HasColumnName("tax_state");
                entity.Property(e => e.is_moh_approved).HasColumnName("is_moh_approved");
                entity.Property(e => e.is_secretary_approved).HasColumnName("is_secretary_approved");
                entity.Property(e => e.is_chairman_approved).HasColumnName("is_chairman_approved");
                entity.Property(e => e.MOHApprovedBy).HasColumnName("moh_approved_by");
                entity.Property(e => e.SecretaryApprovedBy).HasColumnName("secretary_approved_by");
                entity.Property(e => e.ChairmanApprovedBy).HasColumnName("chairman_approved_by");
                entity.Property(e => e.MOHApprovedAt).HasColumnName("moh_approved_at");
                entity.Property(e => e.SecretaryApprovedAt).HasColumnName("secretary_approved_at");
                entity.Property(e => e.ChairmanApprovedAt).HasColumnName("chairman_approved_at");

                entity.Ignore(e => e.Business);

                entity.HasOne(d => d.Business)
                   .WithMany(p => p.BusinessTaxes)
                   .HasForeignKey(d => d.BusinessId)
                   .HasConstraintName("business_taxes_FK");
            });

            modelBuilder.Entity<BusinessPlace>(entity =>
            {
                entity.ToTable("res_business_places");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.GnDivision).HasColumnName("gn_division");
                entity.Property(e => e.Ward).HasColumnName("ward");
                entity.Property(e => e.Street).HasColumnName("street");
                entity.Property(e => e.AssessmentNo).HasColumnName("assessment_no");
                entity.Property(e => e.Address1).HasColumnName("address_1");
                entity.Property(e => e.Address2).HasColumnName("address_2");
                entity.Property(e => e.City).HasColumnName("city");
                entity.Property(e => e.Zip).HasColumnName("zip");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.BusinessId).HasColumnName("business_id");
                entity.Property(e => e.PropertyOwnerId).HasColumnName("property_owner_id");
                entity.Property(e => e.RIUserId).HasColumnName("ri_id");
            });

            modelBuilder.Entity<SMSOutBox>(entity =>
            {
                entity.Property(e => e.CreatedBy);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<SMSConfiguration>(entity =>
            {
                entity.Property(e => e.CreatedBy);
                entity.Property(e => e.UpdatedBy);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<EmailConfiguration>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValueSql("'1'");
                entity.Property(e => e.OfficeId);
                entity.Property(e => e.SabhaId);
                entity.Property(e => e.CreatedBy);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.UpdatedBy);
            });

            modelBuilder.Entity<EmailOutBox>(entity =>
            {
                entity.Property(e => e.OfficeId);
                entity.Property(e => e.SabhaId);
                entity.Property(e => e.CreatedByID);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}