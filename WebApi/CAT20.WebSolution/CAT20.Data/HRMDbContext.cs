using Microsoft.EntityFrameworkCore;
using CAT20.Core.Models.HRM.PersonalFile;
using CAT20.Core.Models.HRM.LoanManagement;
using CAT20.Core.Models.Enums.HRM;
using CAT20.Core.Models.HRM;


namespace CAT20.Data
{
    public partial class HRMDbContext : DbContext
    {
        public HRMDbContext()
        {

        }

        public HRMDbContext(DbContextOptions<HRMDbContext> options) : base(options)
        {

        }

        // PersonalFile
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<SpouserInfo> SpouserInfos { get; set; }
        public virtual DbSet<ChildInfo> ChildrenInfos { get; set; }       
        public virtual DbSet<ServiceInfo> ServiceInfos { get; set; }
        public virtual DbSet<SalaryInfo> SalaryInfos { get; set; }
        public virtual DbSet<NetSalaryAgent> NetSalaryAgent { get; set; }
        public virtual DbSet<OtherRemittanceAgent> OtherRemittanceAgents { get; set; }
        public virtual DbSet<EmployeeTypeData> EmployeeTypeData { get; set; }
        public virtual DbSet<CarderStatusData> CarderStatusData { get; set; }
        public virtual DbSet<SalaryStructureData> SalaryStructureData { get; set; }
        public virtual DbSet<ServiceTypeData> ServiceTypeData { get; set; }
        public virtual DbSet<JobTitleData> JobTitleData { get; set; }
        public virtual DbSet<ClassLevelData> ClassLevelData { get; set; }
        public virtual DbSet<GradeLevelData> GradeLevelData { get; set; }
        public virtual DbSet<AgraharaCategoryData> AgraharaCategoryData { get; set; }
        public virtual DbSet<AppointmentTypeData> AppointmentTypeData { get; set; }
        public virtual DbSet<FundingSourceData> FundingSourceData { get; set; }
        public virtual DbSet<SupportingDocTypeData> SupportingDocTypeData { get; set; }
        public virtual DbSet<SupportingDocument> SupportingDocuments { get; set; }

        public virtual DbSet<HRMSequenceNumber> HRMSequenceNumbers { get; set; }

        // Loan Management
        public DbSet<AdvanceB> Loans { get; set; }
        public DbSet<AdvanceBTypeData> LoanTypeDatas { get; set; }
        public DbSet<AdvanceBTypeLedgerMapping> LoanTypeLedgerMappings { get; set; }
        public DbSet<AdvanceBAttachment> LoanAttachments { get; set; }
        public DbSet<AdvanceBSettlement> LoanSettlements { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_sinhala_ci")
                .HasCharSet("utf8mb4");

            // PersonalFile

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("hr_pf_employee");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_emp_id");
                entity.Property(e => e.EmployeeTypeID).HasColumnName("hr_pf_emp_type");
                entity.Property(e => e.CarderStausID).HasColumnName("hr_pf_emp_carder_status");
                entity.Property(e => e.NICNumber).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_nic_no");
                entity.Property(e => e.PassportNumber).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_passport_no");
                entity.Property(e => e.PersonalFileNumber).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_personal_file_no");
                entity.Property(e => e.EmployeeNo).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_no");
                entity.Property(e => e.PayNo).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_pay_no");
                entity.Property(e => e.Title).HasColumnName("hr_pf_emp_title");
                entity.Property(e => e.Initials).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_initials");
                entity.Property(e => e.FirstName).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_first_name");
                entity.Property(e => e.MiddleName).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_middle_name");
                entity.Property(e => e.LastName).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_last_name");
                entity.Property(e => e.FullName).HasColumnType("varchar(500)").HasColumnName("hr_pf_emp_full_name");
                entity.Property(e => e.GenderID).HasColumnName("hr_pf_emp_gender");
                entity.Property(e => e.DateOfBirth).HasColumnName("hr_pf_emp_dob");
                entity.Property(e => e.CivilStatus).HasColumnName("hr_pf_emp_civil_status");
                entity.Property(e => e.MarriedDate).HasColumnName("hr_pf_emp_married_date");
                entity.Property(e => e.RailwayWarrant).HasColumnName("hr_pf_emp_railway_warrant");
                entity.Property(e => e.MobileNo).HasColumnType("varchar(10)").HasColumnName("hr_pf_emp_mobile_no");
                entity.Property(e => e.PersonalEmail).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_personal_email");
                entity.Property(e => e.PhotographPath).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_photo_path");
                entity.Property(e => e.SabhaId).HasColumnName("hr_pf_emp_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("hr_pf_emp_office_id");
                entity.Property(e => e.ProgrammeId).HasColumnName("hr_pf_emp_programme_id");
                entity.Property(e => e.ProjectId).HasColumnName("hr_pf_emp_project_id");
                entity.Property(e => e.SubProjectId).HasColumnName("hr_pf_emp_sub_project_id");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_pf_emp_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_pf_emp_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_pf_emp_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_pf_emp_updated_by");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_pf_emp_status_id");

                entity.HasIndex(e => new { e.EmployeeNo, e.SabhaId }).IsUnique();
                entity.HasIndex(e => new { e.NICNumber }).IsUnique();

                entity.HasOne(e => e.EmployeeTypeDatas)
                       .WithMany(e => e.Employees)
                       .HasForeignKey(e => e.EmployeeTypeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_emp_typ_dt_id");

                entity.HasOne(e => e.CarderStatusDatas)
                       .WithMany(e => e.Employees)
                       .HasForeignKey(e => e.CarderStausID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_emp_cs_dt_id");

            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("hr_pf_address");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_addr_id");
                entity.Property(e => e.EmployeeID).HasColumnName("hr_pf_addr_employee_id");
                entity.Property(e => e.AddressType).HasColumnName("hr_pf_addr_typ_id");
                entity.Property(e => e.AddressLine1).HasColumnType("varchar(255)").HasColumnName("hr_pf_addr_line1");
                entity.Property(e => e.AddressLine2).HasColumnType("varchar(255)").HasColumnName("hr_pf_addr_line2");
                entity.Property(e => e.CityTown).HasColumnType("varchar(255)").HasColumnName("hr_pf_addr_city_town");
                entity.Property(e => e.GnDivision).HasColumnName("hr_pf_addr_gn_division");
                entity.Property(e => e.PostalCode).HasColumnName("hr_pf_addr_postal_code");
                entity.Property(e => e.Telephone).HasColumnType("varchar(10)").HasColumnName("hr_pf_addr_telephone");
                entity.Property(e => e.Fax).HasColumnType("varchar(10)").HasColumnName("hr_pf_addr_fax");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_pf_addr_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_pf_addr_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_pf_addr_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_pf_addr_updated_by");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_pf_addr_status_id");

                entity.HasOne(e => e.Employee)
                       .WithMany(e => e.Addresses)
                       .HasForeignKey(e => e.EmployeeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_addrs_emp");

            });

            modelBuilder.Entity<SpouserInfo>(entity =>
            {
                entity.ToTable("hr_pf_spouser_info");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_si_id");
                entity.Property(e => e.EmployeeID).HasColumnName("hr_pf_si_emp_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_si_name");
                entity.Property(e => e.DateOfBirth).HasColumnName("hr_pf_si_dob");
                entity.Property(e => e.JobTitle).HasColumnType("varchar(255)").HasColumnName("hr_pf_si_job_title");
                entity.Property(e => e.WorkPlace).HasColumnType("varchar(255)").HasColumnName("hr_pf_si_work_place");
                entity.Property(e => e.GnDivision).HasColumnName("hr_pf_si_gn_division");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_pf_si_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_pf_si_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_pf_si_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_pf_si_updated_by");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_pf_si_status_id");

                entity.HasOne(e => e.Employee)
                       .WithMany(e => e.SpouserInfos)
                       .HasForeignKey(e => e.EmployeeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_si_emp");

            });

            modelBuilder.Entity<ChildInfo>(entity =>
            {
                entity.ToTable("hr_pf_child_info");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_ci_id");
                entity.Property(e => e.EmployeeID).HasColumnName("hr_pf_ci_emp_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_ci_name");
                entity.Property(e => e.DateOfBirth).HasColumnName("hr_pf_ci_dob");


                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_pf_ci_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_pf_ci_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_pf_ci_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_pf_ci_updated_by");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_pf_ci_status_id");

                entity.HasOne(e => e.Employee)
                       .WithMany(e => e.ChildrenInfos)
                       .HasForeignKey(e => e.EmployeeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_ci_emp");

            });

            modelBuilder.Entity<ServiceInfo>(entity =>
            {
                entity.ToTable("hr_pf_service_info");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_si_id");
                entity.Property(e => e.EmployeeID).HasColumnName("hr_pf_si_emp_id");
                entity.Property(e => e.SalaryStructureID).HasColumnName("hr_pf_si_salary_structure_id");
                entity.Property(e => e.ServiceTypeID).HasColumnName("hr_pf_si_service_id");
                entity.Property(e => e.ServiceLevelID).HasColumnName("hr_pf_si_level");
                entity.Property(e => e.JobTitleID).HasColumnName("hr_pf_si_post_name");
                entity.Property(e => e.Class).HasColumnName("hr_pf_si_class");
                entity.Property(e => e.Grade).HasColumnName("hr_pf_si_grade");
                entity.Property(e => e.SalaryStep).HasColumnType("varchar(255)").HasColumnName("hr_pf_si_salary_step");
                entity.Property(e => e.SalaryStepLevelID).HasColumnName("hr_pf_si_salary_step_level");
                entity.Property(e => e.BasicSalary).HasColumnType("decimal(18,2)").HasColumnName("hr_pf_si_basic_salary");
                entity.Property(e => e.IncrementDate).HasColumnName("hr_pf_si_increment_date");
                entity.Property(e => e.AppointmentTypeID).HasColumnName("hr_pf_si_appointment_type");
                entity.Property(e => e.AppointmentLetterNumber).HasColumnType("varchar(255)").HasColumnName("hr_pf_si_appointment_letter_number");
                entity.Property(e => e.AppointmentDate).HasColumnName("hr_pf_si_appointment_date");
                entity.Property(e => e.FundingSourceID).HasColumnName("hr_pf_si_funding_source");
                entity.Property(e => e.ReimbursedPercentageID).HasColumnName("hr_pf_si_reimbursed_pct");
                entity.Property(e => e.AgraharaCategoryID).HasColumnName("hr_pf_si_agrahara_category");
                entity.Property(e => e.PensionNumber).HasColumnType("varchar(255)").HasColumnName("hr_pf_si_pension_number");
                entity.Property(e => e.WOPNumber).HasColumnType("varchar(255)").HasColumnName("hr_pf_si_wop_number");
                entity.Property(e => e.PSPFNumber).HasColumnType("varchar(255)").HasColumnName("hr_pf_si_pspf_number");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_pf_si_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_pf_si_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_pf_si_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_pf_si_updated_by");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_pf_si_status_id");

                entity.HasOne(e => e.Employee)
                       .WithMany(e => e.ServiceInfos)
                       .HasForeignKey(e => e.EmployeeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_serin_emp");

                entity.HasOne(e => e.AppointmentTypeDatas)
                       .WithMany(e => e.ServiceInfos)
                       .HasForeignKey(e => e.AppointmentTypeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_si_Appt_dt_id");

                entity.HasOne(e => e.FundingSourceDatas)
                       .WithMany(e => e.ServiceInfos)
                       .HasForeignKey(e => e.FundingSourceID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_si_fs_dt_id");

                entity.HasOne(e => e.AgraharaCategoryDatas)
                       .WithMany(e => e.ServiceInfos)
                       .HasForeignKey(e => e.AgraharaCategoryID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_si_ac_id");

                entity.HasOne(e => e.SalaryStructureDatas)
                       .WithMany(e => e.ServiceInfos)
                       .HasForeignKey(e => e.SalaryStructureID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_si_sc_id");

                entity.HasOne(e => e.ServiceTypeDatas)
                       .WithMany(e => e.ServiceInfos)
                       .HasForeignKey(e => e.ServiceTypeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_si_st_id");

                entity.HasOne(e => e.JobTitleDatas)
                       .WithMany(e => e.ServiceInfos)
                       .HasForeignKey(e => e.JobTitleID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_si_jt_id");
                
                entity.HasOne(e => e.ClassLevelDatas)
                       .WithMany(e => e.ServiceInfos)
                       .HasForeignKey(e => e.Class)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_si_cl_id");

                entity.HasOne(e => e.GradeLevelDatas)
                       .WithMany(e => e.ServiceInfos)
                       .HasForeignKey(e => e.Grade)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_si_gl_id");

            });

            modelBuilder.Entity<SalaryInfo>(entity =>
            {
                entity.ToTable("hr_pf_salary_info");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_saly_id");
                entity.Property(e => e.EmployeeID).HasColumnName("hr_pf_saly_emp_id");
                entity.Property(e => e.WAndOPRate).HasColumnName("hr_pf_saly_w_op_rates");
                entity.Property(e => e.PSPFRate).HasColumnName("hr_pf_saly_pspf_rate");
                entity.Property(e => e.EmployeePSPFRate).HasColumnName("hr_pf_emp_pspf_rate");
                entity.Property(e => e.LocalAuthoritiyPSPFRate).HasColumnName("hr_pf_la_pspf_rate");
                entity.Property(e => e.OTRate).HasColumnName("hr_pf_saly_ot_rate");
                entity.Property(e => e.DaysPayRate).HasColumnName("hr_pf_saly_days_pay_rate");
                entity.Property(e => e.ETFRate).HasColumnName("hr_pf_saly_etf_rate");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_pf_saly_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_pf_saly_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_pf_saly_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_pf_saly_updated_by");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_pf_saly_status_id");

                entity.HasOne(e => e.Employee)
                       .WithMany(e => e.SalaryInfos)
                       .HasForeignKey(e => e.EmployeeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_salin_emp");

            });

            modelBuilder.Entity<NetSalaryAgent>(entity =>
            {
                entity.ToTable("hr_pf_net_salary_agent");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_nt_agnt_id");
                entity.Property(e => e.EmployeeID).HasColumnName("hr_pf_nt_agnt_emp_id");
                entity.Property(e => e.BankName).HasColumnName("hr_pf_nt_agnt_bank_name");
                entity.Property(e => e.BankCode).HasColumnName("hr_pf_nt_agnt_bank_code");
                entity.Property(e => e.BranchCode).HasColumnName("hr_pf_nt_agnt_branch_code");
                entity.Property(e => e.AccountNo).HasColumnType("varchar(255)").HasColumnName("hr_pf_nt_agnt_account_no");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_pf_nt_agnt_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_pf_nt_agnt_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_pf_nt_agnt_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_pf_nt_agnt_updated_by");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_pf_nt_agnt_status_id");

                entity.HasOne(e => e.Employee)
                       .WithMany(e => e.NetSalaryAgents)
                       .HasForeignKey(e => e.EmployeeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_nt_agnt_emp");

            });

            modelBuilder.Entity<OtherRemittanceAgent>(entity =>
            {
                entity.ToTable("hr_pf_other_remittance_agent");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_or_agnt_id");
                entity.Property(e => e.EmployeeID).HasColumnName("hr_pf_or_agnt_emp_id");
                entity.Property(e => e.BankName).HasColumnName("hr_pf_or_agnt_bank_name");
                entity.Property(e => e.BankCode).HasColumnName("hr_pf_or_agnt_bank_code");
                entity.Property(e => e.BranchCode).HasColumnName("hr_pf_or_agnt_branch_code");
                entity.Property(e => e.AccountNo).HasColumnType("varchar(255)").HasColumnName("hr_pf_or_agnt_account_no");
                entity.Property(e => e.AgreDate).HasColumnName("hr_pf_or_agnt_agre_date");
                entity.Property(e => e.AgreMinimumAmount).HasColumnType("decimal(18,2)").HasColumnName("hr_pf_or_agnt_agre_min_amount");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_pf_or_agnt_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_pf_or_agnt_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_pf_or_agnt_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_pf_or_agnt_updated_by");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_pf_or_agnt_status_id");

                entity.HasOne(e => e.Employee)
                       .WithMany(e => e.OtherRemittanceAgents)
                       .HasForeignKey(e => e.EmployeeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_or_agnt_emp");

            });

            modelBuilder.Entity<EmployeeTypeData>(entity =>
            {
                entity.ToTable("hr_pf_employee_type_data");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_emp_typ_dt_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_emp_typ_dt_name");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("hr_pf_emp_typ_dt_status");

                entity.HasData(
                    new EmployeeTypeData { Id = 1, Name = "New Appointment" },
                    new EmployeeTypeData { Id = 2, Name = "Existing" },
                    new EmployeeTypeData { Id = 3, Name = "Transfer In" },
                    new EmployeeTypeData { Id = 4, Name = "Temporary Hold" });

            });

            modelBuilder.Entity<CarderStatusData>(entity =>
            {
                entity.ToTable("hr_pf_carder_status_data");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_cs_dt_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_cs_dt_name");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("hr_pf_cs_dt_status");

                entity.HasData(
                    new CarderStatusData { Id = 1, Name = "Local Authority" },
                    new CarderStatusData { Id = 2, Name = "Local Government" },
                    new CarderStatusData { Id = 3, Name = "Attached from Other Office" },
                    new CarderStatusData { Id = 4, Name = "Attached to Other Office" });

            });

            modelBuilder.Entity<SupportingDocTypeData>(entity =>
            {
                entity.ToTable("hr_pf_supporting_doc_type_data");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_sdt_dt_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_sdt_dt_name");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("hr_pf_sdt_dt_status");

                entity.HasData(
                    new SupportingDocTypeData { Id = 1, Name = "National Identity Card (NIC)" },
                    new SupportingDocTypeData { Id = 2, Name = "Birth Certificate" },
                    new SupportingDocTypeData { Id = 3, Name = "Marriage Certificate" },
                    new SupportingDocTypeData { Id = 4, Name = "Educational Certificate" },
                    new SupportingDocTypeData { Id = 5, Name = "Appointment Letter" },
                    new SupportingDocTypeData { Id = 6, Name = "EB" },
                    new SupportingDocTypeData { Id = 7, Name = "Child's Birth Certificate" },
                    new SupportingDocTypeData { Id = 8, Name = "Other Certificate" });

            });

            modelBuilder.Entity<SupportingDocument>(entity =>
            {
                entity.ToTable("hr_pf_supporting_document");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_sd_id");
                entity.Property(e => e.EmployeeID).HasColumnName("hr_pf_sd_emp_id");
                entity.Property(e => e.SupportingDocTypeID).HasColumnName("hr_pf_sd_type_id");
                entity.Property(e => e.DocumentPath).HasColumnType("varchar(255)").HasColumnName("hr_pf_sd_document_path");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_pf_sd_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_pf_sd_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_pf_sd_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_pf_sd_updated_by");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_pf_sd_status_id");

                entity.HasOne(e => e.Employee)
                       .WithMany(e => e.SupportingDocuments)
                       .HasForeignKey(e => e.EmployeeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_sd_emp");

                entity.HasOne(e => e.SupportingDocTypeDatas)
                       .WithMany(e => e.SupportingDocuments)
                       .HasForeignKey(e => e.SupportingDocTypeID)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_sdt_dt_id");

            });

            modelBuilder.Entity<SalaryStructureData>(entity =>
            {
                entity.ToTable("hr_pf_salary_structure_data");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_salst_dt_id");
                entity.Property(e => e.ServiceCategory).HasColumnType("varchar(255)").HasColumnName("hr_pf_salst_dt_name");
                entity.Property(e => e.SalaryCode).HasColumnType("varchar(255)").HasColumnName("hr_pf_salst_dt_salary_code");
                entity.Property(e => e.InitialStep).HasColumnType("decimal(18,2)").HasColumnName("hr_pf_salst_dt_initial_step");
                entity.Property(e => e.Years1).HasColumnName("hr_pf_salst_dt_years1");
                entity.Property(e => e.FirstSlab).HasColumnType("decimal(18,2)").HasColumnName("hr_pf_salst_dt_first_slab");
                entity.Property(e => e.Years2).HasColumnName("hr_pf_salst_dt_years2");
                entity.Property(e => e.SecondSlab).HasColumnType("decimal(18,2)").HasColumnName("hr_pf_salst_dt_second_slab");
                entity.Property(e => e.Years3).HasColumnName("hr_pf_salst_dt_years3");
                entity.Property(e => e.ThirdSlab).HasColumnType("decimal(18,2)").HasColumnName("hr_pf_salst_dt_third_slab");
                entity.Property(e => e.Years4).HasColumnName("hr_pf_salst_dt_years4");
                entity.Property(e => e.FourthSlab).HasColumnType("decimal(18,2)").HasColumnName("hr_pf_salst_dt_fourth_slab");
                entity.Property(e => e.Maximum).HasColumnType("decimal(18,2)").HasColumnName("hr_pf_salst_dt_maximum");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("hr_pf_salst_dt_status");

                entity.HasMany(e => e.ServiceTypes)
                       .WithOne(e => e.SalaryStructureData)
                       .HasForeignKey(e => e.SalaryStructureDataId)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_ssd_dt_id");

                entity.HasData(
                    new SalaryStructureData { Id = 1, ServiceCategory = "Primary Level Unskilled", SalaryCode = "PL 1-2016", InitialStep = 24250.00m, Years1 = 10, FirstSlab = 250.00m, Years2 = 10, SecondSlab = 270.00m, Years3 = 10, ThirdSlab = 300.00m, Years4 = 12, FourthSlab = 330.00m, Maximum = 36410.00m, Status = 1 },
                    new SalaryStructureData { Id = 2, ServiceCategory = "Primary Level Semi-skilled", SalaryCode = "PL 2-2016", InitialStep = 25250.00m, Years1 = 10, FirstSlab = 270.00m, Years2 = 10, SecondSlab = 300.00m, Years3 = 10, ThirdSlab = 330.00m, Years4 = 12, FourthSlab = 350.00m, Maximum = 38450.00m, Status = 1 },
                    new SalaryStructureData { Id = 3, ServiceCategory = "Primary Level Skilled", SalaryCode = "PL 3-2016", InitialStep = 25790.00m, Years1 = 10, FirstSlab = 270.00m, Years2 = 10, SecondSlab = 300.00m, Years3 = 10, ThirdSlab = 330.00m, Years4 = 12, FourthSlab = 350.00m, Maximum = 38990.00m, Status = 1 },
                    new SalaryStructureData { Id = 4, ServiceCategory = "Management Assistants Seg 2", SalaryCode = "MN 1-2016", InitialStep = 27140.00m, Years1 = 10, FirstSlab = 300.00m, Years2 = 11, SecondSlab = 350.00m, Years3 = 10, ThirdSlab = 495.00m, Years4 = 10, FourthSlab = 660.00m, Maximum = 45540.00m, Status = 1 },
                    new SalaryStructureData { Id = 5, ServiceCategory = "Management Assistants Seg 1", SalaryCode = "MN 2-2016", InitialStep = 28940.00m, Years1 = 10, FirstSlab = 300.00m, Years2 = 11, SecondSlab = 350.00m, Years3 = 10, ThirdSlab = 560.00m, Years4 = 10, FourthSlab = 660.00m, Maximum = 47990.00m, Status = 1 },
                    new SalaryStructureData { Id = 6, ServiceCategory = "MA Supervisory Non-tech / Tech", SalaryCode = "MN 3-2016", InitialStep = 31040.00m, Years1 = 10, FirstSlab = 445.00m, Years2 = 11, SecondSlab = 660.00m, Years3 = 10, ThirdSlab = 730.00m, Years4 = 10, FourthSlab = 750.00m, Maximum = 57550.00m, Status = 1 },
                    new SalaryStructureData { Id = 7, ServiceCategory = "Associate Officer", SalaryCode = "MN 4-2016", InitialStep = 31490.00m, Years1 = 10, FirstSlab = 445.00m, Years2 = 11, SecondSlab = 660.00m, Years3 = 10, ThirdSlab = 730.00m, Years4 = 5, FourthSlab = 750.00m, Maximum = 54250.00m, Status = 1 },
                    new SalaryStructureData { Id = 8, ServiceCategory = "Field/Office based Officer Seg 2", SalaryCode = "MN 5-2016", InitialStep = 34605.00m, Years1 = 10, FirstSlab = 660.00m, Years2 = 11, SecondSlab = 755.00m, Years3 = 15, ThirdSlab = 930.00m, Maximum = 63460.00m, Status = 1 },
                    new SalaryStructureData { Id = 9, ServiceCategory = "Field/Office based Officer Seg 1", SalaryCode = "MN 6-2016", InitialStep = 36585.00m, Years1 = 10, FirstSlab = 660.00m, Years2 = 11, SecondSlab = 755.00m, Years3 = 15, ThirdSlab = 930.00m, Maximum = 65440.00m, Status = 1 },
                    new SalaryStructureData { Id = 10, ServiceCategory = "Management Assistants Supra", SalaryCode = "MN 7-2016", InitialStep = 41580.00m, Years1 = 11, FirstSlab = 755.00m, Years2 = 18, SecondSlab = 1030.00m, Maximum = 68425.00m, Status = 1 },
                    new SalaryStructureData { Id = 11, ServiceCategory = "MA Technical Seg 3", SalaryCode = "MT 1-2016", InitialStep = 29840.00m, Years1 = 10, FirstSlab = 300.00m, Years2 = 11, SecondSlab = 350.00m, Years3 = 10, ThirdSlab = 560.00m, Years4 = 10, FourthSlab = 660.00m, Maximum = 48890.00m, Status = 1 },
                    new SalaryStructureData { Id = 12, ServiceCategory = "MA Technical Seg 2", SalaryCode = "MT 2-2016", InitialStep = 30140.00m, Years1 = 10, FirstSlab = 350.00m, Years2 = 11, SecondSlab = 370.00m, Years3 = 10, ThirdSlab = 560.00m, Years4 = 10, FourthSlab = 660.00m, Maximum = 49910.00m, Status = 1 },
                    new SalaryStructureData { Id = 13, ServiceCategory = "MA Technical Seg 1", SalaryCode = "MT 3-2016", InitialStep = 30840.00m, Years1 = 10, FirstSlab = 350.00m, Years2 = 11, SecondSlab = 370.00m, Years3 = 10, ThirdSlab = 560.00m, Years4 = 10, FourthSlab = 660.00m, Maximum = 50610.00m, Status = 1 },
                    new SalaryStructureData { Id = 14, ServiceCategory = "Para Medical Services Seg 3", SalaryCode = "MT 4-2016", InitialStep = 31190.00m, Years1 = 10, FirstSlab = 445.00m, Years2 = 11, SecondSlab = 660.00m, Years3 = 10, ThirdSlab = 730.00m, Years4 = 10, FourthSlab = 750.00m, Maximum = 57700.00m, Status = 1 },
                    new SalaryStructureData { Id = 15, ServiceCategory = "Para Medical Services Seg 2", SalaryCode = "MT 5-2016", InitialStep = 31635.00m, Years1 = 10, FirstSlab = 445.00m, Years2 = 11, SecondSlab = 660.00m, Years3 = 10, ThirdSlab = 730.00m, Years4 = 10, FourthSlab = 750.00m, Maximum = 58145.00m, Status = 1 },
                    new SalaryStructureData { Id = 16, ServiceCategory = "PSM/Para Medical Services Seg 1", SalaryCode = "MT 6-2016", InitialStep = 32080.00m, Years1 = 10, FirstSlab = 445.00m, Years2 = 11, SecondSlab = 660.00m, Years3 = 10, ThirdSlab = 730.00m, Years4 = 10, FourthSlab = 750.00m, Maximum = 58590.00m, Status = 1 },
                    new SalaryStructureData { Id = 17, ServiceCategory = "Nurses", SalaryCode = "MT 7-2016", InitialStep = 32525.00m, Years1 = 10, FirstSlab = 445.00m, Years2 = 11, SecondSlab = 660.00m, Years3 = 10, ThirdSlab = 730.00m, Years4 = 10, FourthSlab = 750.00m, Maximum = 59035.00m, Status = 1 },
                    new SalaryStructureData { Id = 18, ServiceCategory = "Nurses, PSM, PMS Spl. Grade", SalaryCode = "MT 8-2016", InitialStep = 50200.00m, Years1 = 10, FirstSlab = 1345.00m, Years2 = 8, SecondSlab = 1630.00m, Maximum = 76690.00m, Status = 1 },
                    new SalaryStructureData { Id = 19, ServiceCategory = "Executive", SalaryCode = "SL 1-2016", InitialStep = 47615.00m, Years1 = 10, FirstSlab = 1335.00m, Years2 = 8, SecondSlab = 1630.00m, Years3 = 17, ThirdSlab = 2170.00m, Maximum = 110895.00m, Status = 1 },
                    new SalaryStructureData { Id = 20, ServiceCategory = "Medical Officers", SalaryCode = "SL 2-2016", InitialStep = 52955.00m, Years1 = 3, FirstSlab = 1335.00m, Years2 = 7, SecondSlab = 1345.00m, Years3 = 2, ThirdSlab = 1630.00m, Years4 = 16, FourthSlab = 2170.00m, Maximum = 104355.00m, Status = 1 },
                    new SalaryStructureData { Id = 21, ServiceCategory = "Senior executive/MO Specialists", SalaryCode = "SL 3-2016", InitialStep = 88000.00m, Years1 = 12, FirstSlab = 2700.00m, Maximum = 120400.00m, Status = 1 },
                    new SalaryStructureData { Id = 22, ServiceCategory = "Secretaries", SalaryCode = "SL 4-2016", InitialStep = 98650.00m, Years1 = 12, FirstSlab = 2925.00m, Maximum = 133750.00m, Status = 1 },
                    new SalaryStructureData { Id = 23, ServiceCategory = "Law Officers", SalaryCode = "SL 5-2016", InitialStep = 58295.00m, Years1 = 5, FirstSlab = 1335.00m, Years2 = 5, SecondSlab = 1630.00m, Years3 = 15, ThirdSlab = 2170.00m, Maximum = 105670.00m, Status = 1 },
                    new SalaryStructureData { Id = 24, ServiceCategory = "Addl. SG", SalaryCode = "SL 7-2016", InitialStep = 106950.00m, Years1 = 5, FirstSlab = 2750.00m, Maximum = 120700.00m, Status = 1 },
                    new SalaryStructureData { Id = 25, ServiceCategory = "Sri Lanka Teachers Service", SalaryCode = "GE 1-2016", InitialStep = 27740.00m, Years1 = 6, FirstSlab = 300.00m, Years2 = 7, SecondSlab = 380.00m, Years3 = 2, ThirdSlab = 445.00m, Maximum = 33090.00m, Status = 1 },
                    new SalaryStructureData { Id = 26, ServiceCategory = "Sri Lanka Teachers Service", SalaryCode = "GE 2-2016", InitialStep = 33300.00m, Years1 = 5, FirstSlab = 495.00m, Years2 = 5, SecondSlab = 680.00m, Years3 = 7, ThirdSlab = 825.00m, Years4 = 20, FourthSlab = 1335.00m, Maximum = 71650.00m, Status = 1 },
                    new SalaryStructureData { Id = 27, ServiceCategory = "Sri Lanka Principals Service", SalaryCode = "GE 4-2016", InitialStep = 35280.00m, Years1 = 7, FirstSlab = 680.00m, Years2 = 6, SecondSlab = 825.00m, Years3 = 11, ThirdSlab = 1335.00m, Years4 = 14, FourthSlab = 1650.00m, Maximum = 82775.00m, Status = 1 },
                    new SalaryStructureData { Id = 28, ServiceCategory = "Medical Practitioners", SalaryCode = "MP 1-2016", InitialStep = 35195.00m, Years1 = 12, FirstSlab = 660.00m, Years2 = 13, SecondSlab = 745.00m, Years3 = 10, ThirdSlab = 1135.00m, Maximum = 64150.00m, Status = 1 },
                    new SalaryStructureData { Id = 29, ServiceCategory = "Medical Practitioners Spl. Gr.", SalaryCode = "MP 2-2016", InitialStep = 56205.00m, Years1 = 9, FirstSlab = 1345.00m, Years2 = 9, SecondSlab = 1630.00m, Maximum = 82980.00m, Status = 1 },
                    new SalaryStructureData { Id = 30, ServiceCategory = "Police / Regulatory Services", SalaryCode = "RS 1-2016", InitialStep = 29540.00m, Years1 = 7, FirstSlab = 300.00m, Years2 = 27, SecondSlab = 370.00m, Maximum = 41630.00m, Status = 1 },
                    new SalaryStructureData { Id = 31, ServiceCategory = "Police / Regulatory Services", SalaryCode = "RS 2-2016", InitialStep = 32010.00m, Years1 = 9, FirstSlab = 370.00m, Years2 = 17, SecondSlab = 495.00m, Maximum = 43755.00m, Status = 1 },
                    new SalaryStructureData { Id = 32, ServiceCategory = "Police / Regulatory Services", SalaryCode = "RS 3-2016", InitialStep = 32790.00m, Years1 = 7, FirstSlab = 370.00m, Years2 = 2, SecondSlab = 495.00m, Years3 = 25, ThirdSlab = 660.00m, Maximum = 52870.00m, Status = 1 },
                    new SalaryStructureData { Id = 33, ServiceCategory = "Police / Regulatory Services", SalaryCode = "RS 4-2016", InitialStep = 37030.00m, Years1 = 24, FirstSlab = 660.00m, Maximum = 52870.00m, Status = 1 },
                    new SalaryStructureData { Id = 34, ServiceCategory = "Police / Regulatory Services", SalaryCode = "RS 5-2016", InitialStep = 42425.00m, Years1 = 17, FirstSlab = 775.00m, Maximum = 55600.00m, Status = 1 },
                    new SalaryStructureData { Id = 35, ServiceCategory = "Solicitor General", SalaryCode = "SF 1-2016", InitialStep = 131500.00m, Years1 = 5, FirstSlab = 2925.00m, Maximum = 146125.00m, Status = 1 },
                    new SalaryStructureData { Id = 36, ServiceCategory = "Attorney General", SalaryCode = "SF 3-2016", InitialStep = 139000.00m, Years1 = 5, FirstSlab = 2925.00m, Maximum = 153625.00m, Status = 1 }
                    );

            });

            modelBuilder.Entity<ServiceTypeData>(entity =>
            {
                entity.ToTable("hr_pf_service_type_data");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_st_dt_id");
                entity.Property(e => e.SalaryStructureDataId).HasColumnName("hr_pf_st_sctgy_dt_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_st_dt_name");
                entity.Property(e => e.Code).HasColumnType("varchar(255)").HasColumnName("hr_pf_st_dt_code");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("hr_pf_st_dt_status");

                entity.HasMany(e => e.JobTitles)
                       .WithOne(e => e.ServiceTypeData)
                       .HasForeignKey(e => e.ServiceTypeDataId)
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("fk_hr_pf_std_dt_id");

            });

            modelBuilder.Entity<JobTitleData>(entity =>
            {
                entity.ToTable("hr_pf_job_title_data");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_jt_dt_id");
                entity.Property(e => e.ServiceTypeDataId).HasColumnName("hr_pf_jt_st_dt_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_jt_dt_name");
                entity.Property(e => e.Code).HasColumnType("varchar(255)").HasColumnName("hr_pf_jt_dt_code");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("hr_pf_tj_dt_status");

            });

            modelBuilder.Entity<ClassLevelData>(entity =>
            {
                entity.ToTable("hr_pf_class_level_data");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_cl_dt_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_cl_dt_name");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("hr_pf_cl_dt_status");

                entity.HasData(
                    new ClassLevelData { Id = 1, Name = "Level I" },
                    new ClassLevelData { Id = 2, Name = "Level II" },
                    new ClassLevelData { Id = 3, Name = "Level III" });
                    //new ClassLevelData { Id = 4, Name = "Supra" });

            });

            modelBuilder.Entity<GradeLevelData>(entity =>
            {
                entity.ToTable("hr_pf_grade_level_data");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_gl_dt_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_gl_dt_name");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("hr_pf_gl_dt_status");

                entity.HasData(
                    new GradeLevelData { Id = 1, Name = "Level I" },
                    new GradeLevelData { Id = 2, Name = "Level II" },
                    new GradeLevelData { Id = 3, Name = "Level III" });
                    //new GradeLevelData { Id = 4, Name = "Supra" });

            });

            modelBuilder.Entity<AppointmentTypeData>(entity =>
            {
                entity.ToTable("hr_pf_appointment_type_data");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_at_dt_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_at_dt_name");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("hr_pf_at_dt_status");

                entity.HasData(
                    new AppointmentTypeData { Id = 1, Name = "Permanent" },
                    new AppointmentTypeData { Id = 2, Name = "Casual" },
                    new AppointmentTypeData { Id = 3, Name = "Contract" },
                    new AppointmentTypeData { Id = 4, Name = "Temporary" },
                    new AppointmentTypeData { Id = 5, Name = "Adesaka" },
                    new AppointmentTypeData { Id = 6, Name = "Acting" },
                    new AppointmentTypeData { Id = 7, Name = "Itukirima" });

            });

            modelBuilder.Entity<AgraharaCategoryData>(entity =>
            {
                entity.ToTable("hr_pf_agrahara_category_data");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_ac_dt_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_ac_dt_name");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("hr_pf_ac_dt_status");

                entity.HasData(
                    new AgraharaCategoryData { Id = 1, Name = "Gold" },
                    new AgraharaCategoryData { Id = 2, Name = "Silver" },
                    new AgraharaCategoryData { Id = 3, Name = "Bronze" });

            });

            modelBuilder.Entity<FundingSourceData>(entity =>
            {
                entity.ToTable("hr_pf_funding_source_data");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_pf_fs_dt_id");
                entity.Property(e => e.Name).HasColumnType("varchar(255)").HasColumnName("hr_pf_fs_dt_name");
                entity.Property(e => e.Status).HasDefaultValue(1).HasColumnName("hr_pf_fs_dt_status");

                entity.HasData(
                    new FundingSourceData { Id = 1, Name = "Reimbursed" },
                    new FundingSourceData { Id = 2, Name = "Council Fund" });

            });

            // Loan Management

            modelBuilder.Entity<AdvanceB>(entity =>
            {
                entity.ToTable("hr_advance_b");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_ab_loan_id");
                entity.Property(e => e.EmployeeId).HasColumnName("hr_ab_employee_id");
                entity.Property(e => e.AdvanceBTypeId).HasColumnName("hr_ab_type_id");
                entity.Property(e => e.LedgerAccId).HasColumnName("hr_ab_ledger_acc_id");
                entity.Property(e => e.IsNew).HasColumnName("hr_ab_is_new");
                entity.Property(e => e.VoucherId).HasColumnName("hr_ab_voucher_id");
                entity.Property(e => e.VoucherNo).HasColumnName("hr_ab_voucher_no");
                entity.Property(e => e.AdvanceBNumber).HasColumnType("varchar(255)").HasColumnName("hr_ab_number");
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)").HasColumnName("hr_ab_loan_amount");
                entity.Property(e => e.InterestPercentage).HasColumnType("decimal(18,2)").HasColumnName("hr_ab_interest_percentage");
                entity.Property(e => e.InstallmentAmount).HasColumnType("decimal(18,2)").HasColumnName("hr_ab_installment_amount");
                entity.Property(e => e.OddInstallmentAmount).HasColumnType("decimal(18,2)").HasColumnName("hr_ab_odd_installment_amount");
                entity.Property(e => e.InterestAmount).HasColumnType("decimal(18,2)").HasColumnName("hr_ab_interest_amount");
                entity.Property(e => e.NumberOfInstallments).HasColumnName("hr_ab_number_of_installments");
                entity.Property(e => e.RemainingInstallments).HasColumnName("hr_ab_remaining_installments");
                entity.Property(e => e.StartDate).HasColumnName("hr_ab_start_date");
                entity.Property(e => e.EndDate).HasColumnName("hr_ab_end_date");
                entity.Property(e => e.BankAccId).HasColumnName("hr_ab_bank_acc_id");
                entity.Property(e => e.Description).HasColumnType("varchar(500)").HasColumnName("hr_ab_description");
                entity.Property(e => e.Guarantor1Id).HasColumnName("hr_ab_guarantor1_id");
                entity.Property(e => e.Guarantor2Id).HasColumnName("hr_ab_guarantor2_id");
                entity.Property(e => e.SabhaId).HasColumnName("hr_ab_sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("hr_ab_office_id");
                entity.Property(e => e.AdvanceBStatus).HasDefaultValue(AdvanceBStatus.Init).HasColumnName("hr_ab_advance_b_status");
                entity.Property(e => e.TransferInOrOutDate).HasColumnName("hr_ab_transfer_in_or_out_date");
                entity.Property(e => e.TransferInOrOutBalanceAmount).HasColumnName("hr_ab_transfer_in_or_out_balance_amount");

                entity.Property(e => e.DeceasedDate).HasColumnName("hr_ab_deceased_date");
                entity.Property(e => e.DeceasedBalance).HasColumnName("hr_ab_deceased_balance");
               
                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_ab_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_ab_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_ab_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_ab_updated_by");
                entity.Property(e => e.RowStatus).HasDefaultValue(1).HasColumnName("hr_ab_status_id");


                entity.HasOne(e => e.Employee)
                    .WithMany()
                    .HasForeignKey(e => e.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.AdvanceBTypeData)
                    .WithMany()
                    .HasForeignKey(e => e.AdvanceBTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Guarantor1)
                    .WithMany()
                    .HasForeignKey(e => e.Guarantor1Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Guarantor2)
                    .WithMany()
                    .HasForeignKey(e => e.Guarantor2Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.AdvanceBSettlements)
                    .WithOne(ls => ls.AdvanceB)
                    .HasForeignKey(ls => ls.AdvanceBId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.SystemActionAt).HasColumnName("hr_ab_system_action_at");


            });



            modelBuilder.Entity<HRMSequenceNumber>(entity =>
            {
                entity.ToTable("hrm_sequence_numbers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");

                entity.Property(e => e.ModuleType).HasColumnName("module_type");
                entity.Property(e => e.LastIndex).HasColumnName("last_index");

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("prefix");

                entity.HasIndex(e => new { e.Year, e.SabhaId, e.ModuleType }).IsUnique(true);
            });

            modelBuilder.Entity<AdvanceBTypeData>(entity =>
            {
                entity.ToTable("hr_advance_b_type_data");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_ab_type_data_id");
                entity.Property(e => e.Description).HasColumnType("varchar(255)").HasColumnName("hr_ab_description").IsRequired();
                entity.Property(e => e.Interest).HasColumnType("decimal(18,2)").HasColumnName("hr_ab_interest");
                entity.Property(e => e.MaxInstalment).HasColumnName("hr_ab_max_instalment");
                entity.Property(e => e.HasInterest).HasColumnName("hr_ab_has_interest");
                entity.Property(e => e.AccountSystemVersionId).HasColumnName("hr_ab_acc_system_ver_id");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_ab_status_id");


            });

            modelBuilder.Entity<AdvanceBTypeLedgerMapping>(entity =>
            {
                entity.ToTable("hr_advance_b_type_ledger_mapping");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_ab_type_ledger_mapping_id");
                entity.Property(e => e.AdvanceBTypeId).HasColumnName("hr_ab_type_id");
                entity.Property(e => e.LedgerCode).HasColumnName("hr_ab_ledger_code");
                entity.Property(e => e.LedgerId).HasColumnName("hr_ab_loan_ledger_id");
                entity.Property(e => e.Prefix).HasColumnType("varchar(50)").HasColumnName("hr_ab_prefix");
                entity.Property(e => e.LastIndex).HasColumnName("hr_ab_last_index");
                entity.Property(e => e.SabhaId).HasColumnName("hr_ab_sabha_id");

                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_ab_status_id");

                entity.HasOne(d => d.AdvanceBTypeData)
                 .WithMany(p => p.AdvanceBTypeLedgerMapping)
                 .HasForeignKey(d => d.AdvanceBTypeId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            //modelBuilder.Entity<LoanOpeningBalance>(entity =>
            //{
            //    entity.ToTable("hr_lm_loan_opening_balance");

            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Id).HasColumnName("hr_lm_loan_opening_balance_id");
            //    entity.Property(e => e.LoanId).HasColumnName("hr_lm_loan_id");
            //    entity.Property(e => e.SettledInstallment).HasColumnType("decimal(18,2)").HasColumnName("hr_lm_settled_installment");
            //    entity.Property(e => e.SettledInterest).HasColumnType("decimal(18,2)").HasColumnName("hr_lm_settled_interest");
            //    entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18,2)").HasColumnName("hr_lm_opening_balance");

            //    entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_lm_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            //    entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_lm_updated_at").ValueGeneratedOnAddOrUpdate();
            //    entity.Property(e => e.CreatedBy).HasColumnName("hr_lm_created_by");
            //    entity.Property(e => e.UpdatedBy).HasColumnName("hr_lm_updated_by");
            //    entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_lm_status_id");

            //    entity.HasOne(e => e.Loan)
            //          .WithMany(l => l.LoanOpeningBalances)
            //          .HasForeignKey(e => e.LoanId)
            //          .OnDelete(DeleteBehavior.Restrict);
            //});

            modelBuilder.Entity<AdvanceBSettlement>(entity =>
            {
                entity.ToTable("hr_advance_b_settlement");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_ab_settlement_id");
                entity.Property(e => e.AdvanceBId).HasColumnName("hr_ab_advance_b_id");
                entity.Property(e => e.PayMonth).HasColumnName("hr_ab_pay_month");
                entity.Property(e => e.Amount).HasColumnName("hr_ab_installment_amount");
                entity.Property(e => e.InterestAmount).HasColumnName("hr_ab_interest_amount");
                entity.Property(e => e.Type).HasColumnName("hr_ab_settlemt_type");
                entity.Property(e => e.Balance).HasColumnName("hr_ab_balance");
                entity.Property(e => e.SettlementCode).HasColumnName("hr_ab_settlement_code");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_ab_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_ab_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_ab_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_ab_updated_by");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_ab_status_id");
                entity.Property(e => e.SystemActionAt).HasColumnName("hr_ab_system_action_at");



            });

            modelBuilder.Entity<AdvanceBAttachment>(entity =>
            {
                entity.ToTable("hr_advance_b_attachment");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("hr_ab_loan_attachment_id");
                entity.Property(e => e.LoanId).HasColumnName("hr_ab_loan_id");
                entity.Property(e => e.FilePath).HasColumnType("varchar(255)").HasColumnName("hr_ab_file_path");
                entity.Property(e => e.FileName).HasColumnType("varchar(255)").HasColumnName("hr_ab_file_name");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasColumnName("hr_ab_created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime").HasColumnName("hr_ab_updated_at").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedBy).HasColumnName("hr_ab_created_by");
                entity.Property(e => e.UpdatedBy).HasColumnName("hr_ab_updated_by");
                entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("hr_ab_status_id");
                entity.Property(e => e.SystemActionAt).HasColumnName("hr_ab_system_action_at");



            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}