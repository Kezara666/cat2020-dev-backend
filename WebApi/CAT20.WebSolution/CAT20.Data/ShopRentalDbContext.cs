using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CAT20.Core.Models.ShopRental;

namespace CAT20.Data
{
    public partial class ShopRentalDbContext : DbContext
    {
        public virtual DbSet<Floor> Floors { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyNature> PropertyNatures { get; set; }
        public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        public virtual DbSet<RentalPlace> RentalPlaces { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<OpeningBalance> OpeningBalances { get; set; }

        public virtual DbSet<ShopRentalBalance> ShopRentalBalance { get; set; }

        public virtual DbSet<ShopRentalVoteAssign> ShopRentalVoteAssign{ get; set; }

        public virtual DbSet<ShopRentalVotePaymentType> ShopRentalVotePaymentType { get; set; }

        public virtual DbSet<ShopRentalProcessConfigaration> ShopRentalProcessConfigaration { get; set; }
        public virtual DbSet<FineRateType> FineRateType { get; set; }
        public virtual DbSet<FineCalType> FineCalType { get; set; }
        public virtual DbSet<RentalPaymentDateType> RentalPaymentDateType { get; set; }
        public virtual DbSet<FineChargingMethod> FineChargingMethod { get; set; }
        public virtual DbSet<ShopAgreementChangeRequest> ShopAgreementChangeRequest { get; set; }
        public virtual DbSet<ProcessConfigurationSettingAssign> ProcessConfigurationSettingAssign { get; set; }
        public virtual DbSet<ShopRentalProcess> ShopRentalProcess { get; set; }
        public virtual DbSet<ShopRentalRecievableIncomeVoteAssign> ShopRentalRecievableIncomeVoteAssign { get; set; }
        public virtual DbSet<ShopDeposit> ShopDeposit { get; set; }

        public virtual DbSet<DailyFineProcessLog> DailyFineProcessLogs { get; set; }

        public virtual DbSet<ShopAgreementActivityLog> ShopAgreementActivityLog { get; set; }

        public virtual DbSet<ShopRentalBalanceLog> ShopRentalBalanceLog { get; set; }

        public ShopRentalDbContext(DbContextOptions<ShopRentalDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_sinhala_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Floor>(entity =>
            {
                entity.ToTable("sr_floor");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                //entity.HasIndex(e => e.RentalPlaceId, "fk_sr_flow_sr_building1_idx");

                entity.Property(e => e.Id).HasColumnName("sr_floor_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("sr_floor_code");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_floor_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_floor_created_by");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("sr_floor_name");

                entity.Property(e => e.Number).HasColumnName("sr_floor_number");

                entity.Property(e => e.RentalPlaceId).HasColumnName("sr_floor_rental_place_id");

                entity.Property(e => e.Status).HasColumnName("sr_floor_status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("sr_floor_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_floor_updated_by");

                entity.HasOne(d => d.RentalPlace)
                    .WithMany(p => p.Floors)
                    .HasForeignKey(d => d.RentalPlaceId)
                    //.OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("sr_floor_ibfk_1");
            });


            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("sr_property");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                //entity.HasIndex(e => e.FoorId, "fk_sr_shop_sr_flow1_idx");

                //entity.HasIndex(e => e.PropertyTypeId, "fk_sr_shop_sr_shop_type1_idx");
                //entity.HasIndex(e => e.PropertyNatureId, "fk_sr_shop_sr_property_nature_idx");

                //entity.HasIndex(e => e.RentalPlaceId, "sr_shop_ibfk_1");

                entity.Property(e => e.Id).HasColumnName("sr_property_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_property_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_property_created_by");

                entity.Property(e => e.FloorId)
                    .HasColumnName("sr_property_floor_id");
                    //.HasDefaultValueSql("'0'");

                entity.Property(e => e.PropertyNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("sr_property_no");

                entity.Property(e => e.OfficeId)
                    .HasColumnName("sr_property_office_id");
                //.HasDefaultValueSql("'0'");

                entity.Property(e => e.SabhaId)
                    .HasColumnName("sr_property_sabha_id");
                    //.HasDefaultValueSql("'0'");

                entity.Property(e => e.PropertyNatureId).HasColumnName("sr_property_property_nature_id");

                //entity.Property(e => e.RentalPlaceId)
                //    .HasColumnName("sr_property_property_place_id")
                //    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PropertyTypeId).HasColumnName("sr_property_property_type_id");

                entity.Property(e => e.Status)
                    .HasColumnName("sr_property_status")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("sr_property_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_property_updated_by");

                //entity.HasOne(d => d.Foor)
                //    .WithMany(p => p.Properties)
                //    .HasForeignKey(d => d.FoorId)
                //    .HasConstraintName("sr_property_ibfk_2");

                //entity.HasOne(d => d.RentalPlace)
                //    .WithMany(p => p.Properties)
                //    .HasForeignKey(d => d.RentalPlaceId)
                //    .HasConstraintName("sr_property_ibfk_5");

                entity.HasOne(d => d.PropertyType)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.PropertyTypeId)
                    //.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sr_property_ibfk_3");

                entity.HasOne(d => d.PropertyNature)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.PropertyNatureId)
                    //.OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sr_property_natureidfk_3");

                //------------[Start -update] Foreign key referencing-------------
                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.FloorId)
                    .HasConstraintName("sr_floor_idfk_3");
                //------------[End -update] Foreign key referencing---------------

                entity.Ignore(t => t.Office);
                //entity.Ignore(t => t.RentalPlace);
                //entity.Ignore(t => t.PropertyType);
                //entity.Ignore(t => t.PropertyNaturezz

                //making unique filels
                //entity.HasIndex(e => new { e.RentalPlaceId, e.PropertyNo }).IsUnique();

                entity.HasIndex(e => new {e.FloorId, e.PropertyNo }).IsUnique();
            });

            modelBuilder.Entity<PropertyNature>(entity =>
            {
                entity.ToTable("sr_property_nature");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_property_nature_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_property_nature_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_property_nature_created_by");
                entity.Property(e => e.SabhaId).HasColumnName("sr_property_nature_sabha_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("sr_property_nature_name");

                entity.Property(e => e.Status).HasColumnName("sr_property_nature_status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("sr_property_nature_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_property_nature_updated_by");
            });

            modelBuilder.Entity<PropertyType>(entity =>
            {
                entity.ToTable("sr_property_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_property_type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("sr_property_type_name");

                entity.Property(e => e.Status).HasColumnName("sr_property_type_status");
            });

            modelBuilder.Entity<RentalPlace>(entity =>
            {
                entity.ToTable("sr_rental_place");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_rental_place_id");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(255)
                    .HasColumnName("sr_rental_place_address_line1");

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(255)
                    .HasColumnName("sr_rental_place_address_line2");

                //entity.Property(e => e.City)
                //    .HasMaxLength(100)
                //    .HasColumnName("sr_rental_place_city");

                entity.Property(e => e.Code)
                    .HasMaxLength(11)
                    .HasColumnName("sr_rental_place_code");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_rental_place_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_rental_place_created_by");

                entity.Property(e => e.GnDivisionId).HasColumnName("sr_rental_place_gn_division_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("sr_rental_place_name");

                entity.Property(e => e.OfficeId).HasColumnName("sr_rental_place_office_id");
                entity.Property(e => e.SabhaId).HasColumnName("sr_rental_place_sabha_id");

                entity.Property(e => e.Status).HasColumnName("sr_rental_place_status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("sr_rental_place_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_rental_place_updated_by");

                entity.Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).ValueGeneratedOnAddOrUpdate();

                //entity.Property(e => e.Zip)
                //    .HasMaxLength(100)
                //    .HasColumnName("sr_rental_place_zip");

                entity.Ignore(t => t.Office);

                //Ignore Default fields in each entity
                entity.Ignore(t => t.ServiceStatus);
                entity.Ignore(t => t.Message);
                entity.Ignore(t => t.AuditReference);
                entity.Ignore(t => t.State);
                entity.Ignore(t => t.ClaimedUserID);
                entity.Ignore(t => t.ClaimedOfficeID);
                entity.Ignore(t => t.ClaimedSabhaID);
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("sr_shop");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                //entity.HasIndex(e => e.CustomerId, "fksr_shop_cus_id");

                //entity.HasIndex(e => e.Status, "fksr_shop_now_status");

                //entity.HasIndex(e => e.PropertyId, "sr_shop_id");

                entity.Property(e => e.Id).HasColumnName("sr_shop_id");

                entity.Property(e => e.AgreementEndDate).HasColumnName("sr_shop_agreement_end_date");

                entity.Property(e => e.AgreementCloseDate).HasColumnName("sr_shop_agreement_close_date"); //new (Shop agreement change request)

                entity.Property(e => e.AgreementNo)
                    .HasMaxLength(100)
                    .HasColumnName("sr_shop_agreement_no");

                entity.Property(e => e.AgreementStartDate).HasColumnName("sr_shop_agreement_start_date");

                entity.Property(e => e.ApprovedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_shop_approved_at");

                entity.Property(e => e.ApprovedBy).HasColumnName("sr_shop_approved_by");

                entity.Property(e => e.BusinessName)
                    .HasMaxLength(255)
                    .HasColumnName("sr_shop_business_name");

                entity.Property(e => e.BusinessNature)
                    .HasMaxLength(255)
                    .HasColumnName("sr_shop_business_nature");

                entity.Property(e => e.BusinessRegistrationNo)
                    .HasMaxLength(255)
                    .HasColumnName("sr_shop_business_registration_number");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_shop_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_shop_created_by");

                entity.Property(e => e.CustomerDesigntion)
                    .HasMaxLength(100)
                    .HasColumnName("sr_shop_customer_designtion");

                entity.Property(e => e.CustomerId).HasColumnName("sr_shop_customer_id");

                entity.Property(e => e.IsApproved).HasColumnName("sr_shop_is_approved");

                entity.Property(e => e.KeyMoney)
                    .HasPrecision(10, 2)
                    .HasColumnName("sr_shop_key_money").HasDefaultValueSql("'0'");

                entity.Property(e => e.PropertyId).HasColumnName("sr_shop_property_id");
                entity.Property(e => e.OfficeId).HasColumnName("sr_shop_office_id");
                entity.Property(e => e.SabhaId).HasColumnName("sr_shop_sabha_id");

                entity.Property(e => e.Rental)
                    .HasPrecision(10, 2)
                    .HasColumnName("sr_shop_rental").HasDefaultValueSql("'0'");

                entity.Property(e => e.SecurityDeposit)
                    .HasPrecision(10, 2)
                    .HasColumnName("sr_shop_security_deposit").HasDefaultValueSql("'0'");

                entity.Property(e => e.ServiceCharge)
                    .HasPrecision(10, 2)
                    .HasColumnName("sr_shop_service_charge").HasDefaultValueSql("'0'");

                entity.Property(e => e.Status).HasColumnName("sr_shop_status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("sr_shop_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_shop_updated_by");

                //making unique filels
                entity.HasIndex(e => new {e.OfficeId, e.Id, e.AgreementNo }).IsUnique();

                //------------[Start] Foreign key referencing---------------
                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Shops)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("sr_shop_ibfk_3");

                //shop - voteAssign --- 1:1
                entity.HasOne(w => w.VoteAssign)
                    .WithOne(p => p.Shop)
                    .HasForeignKey<ShopRentalVoteAssign>(v => v.ShopId);

                //shop - receivablevoteAssign --- 1:1
                entity.HasOne(w => w.RecievableIncomeVoteAssign)
                    .WithOne(p => p.Shop)
                    .HasForeignKey<ShopRentalRecievableIncomeVoteAssign>(v => v.ShopId);

                entity.HasOne(w => w.OpeningBalance)
                    .WithOne(p => p.Shop)
                    .HasForeignKey<OpeningBalance>(o => o.ShopId);

                entity.HasOne(w => w.ShopAgreementChangeRequest)
                    .WithOne(p => p.Shop)
                    .HasForeignKey<ShopAgreementChangeRequest>(o => o.ShopId);
                //------------[End] Foreign key referencing---------------

                entity.Ignore(t => t.Office);
                entity.Ignore(t => t.Customer);
                //entity.Ignore(t => t.RentalPlace);
            });

            modelBuilder.Entity<OpeningBalance>(entity =>
            {
                entity.ToTable("sr_opening_balance");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("sr_ob_id");
                entity.Property(e => e.PropertyId).HasColumnName("sr_ob_property_id");
                entity.Property(e => e.ShopId).HasColumnName("sr_ob_shop_id");
                entity.Property(e => e.Year).HasColumnName("sr_ob_year");
                entity.Property(e => e.MonthId).HasColumnName("sr_ob_month_id");
                entity.Property(e => e.LastYearArrearsAmount).HasColumnName("sr_ob_last_year_arrears_amount");
                entity.Property(e => e.ThisYearArrearsAmount).HasColumnName("sr_ob_this_year_arrears_amount");
                entity.Property(e => e.LastYearFineAmount).HasColumnName("sr_ob_last_year_fine_amount");
                entity.Property(e => e.ThisYearFineAmount).HasColumnName("sr_ob_this_year_fine_amount");
                entity.Property(e => e.ServiceChargeArreasAmount).HasColumnName("sr_ob_service_charge_arreas_amount");
                entity.Property(e => e.CurrentServiceChargeAmount).HasColumnName("sr_ob_current_service_charge_amount");
                entity.Property(e => e.CurrentRentalAmount).HasColumnName("sr_ob_current_rental_amount");
                entity.Property(e => e.OverPaymentAmount).HasColumnName("sr_ob_over_payment_amount");
                //-----
                entity.Property(e => e.BalanceIdForLastYearArrears).HasColumnName("sr_ob_bal_id_for_last_year_arrears");
                entity.Property(e => e.BalanceIdForCurrentBalance).HasColumnName("sr_ob_bal_bal_id_for_current_year_bal");
                //-----

                entity.Property(e => e.Status)
                    .HasColumnName("sr_ob_status")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.OfficeId).HasColumnName("sr_ob_office_id");
                entity.Property(e => e.SabhaId).HasColumnName("sr_ob_sabha_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_ob_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_ob_created_by");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("sr_ob_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_ob_updated_by");

                entity.Property(e => e.IsProcessed)
                    .HasColumnName("sr_bal_is_processed")
                    .HasDefaultValueSql("'0'"); ;

                //new fields----
                entity.Property(e => e.ApprovedAt)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("sr_ob_approved_at");

                entity.Property(e => e.ApprovedBy).HasColumnName("sr_ob_approved_by");

                entity.Property(e => e.ApproveComment).HasColumnName("sr_ob_approve_comment");

                entity.Property(e => e.ApproveStatus)
                    .HasColumnName("sr_ob_approve_status")
                    .HasDefaultValueSql("'0'");
                //--------------


                //Foreign key referencing-----------------------------------
                entity.HasOne(d => d.Property)
                   .WithMany(p => p.OpeningBalances)
                   .HasForeignKey(d => d.PropertyId)
                   .HasConstraintName("sr_opening_balance_FK");
               
                  entity.HasOne(w => w.Shop)
                    .WithOne(p => p.OpeningBalance)
                    .HasForeignKey<OpeningBalance>(o => o.ShopId)
                    .HasConstraintName("sr_opening_balance_FK_1");
                //----------------------------------------------------------

                //making unique filels
                entity.HasIndex(e => new { e.ShopId, e.Year, e.MonthId }).IsUnique();

                //entity.HasIndex(e => e.PropertyId, "sr_opening_balance_FK");
                //entity.HasIndex(e => e.ShopId, "sr_opening_balance_FK_1");
                //entity.HasKey(e => e.Id).HasName("PrimaryKey_OpeningBalanceId");
            });





            //-------------------------------------------------------------------
            modelBuilder.Entity<ShopRentalBalance>(entity =>
            {
                entity.ToTable("sr_shopRental_balance");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("sr_bal_id");
                entity.Property(e => e.PropertyId).HasColumnName("sr_bal_proprty_id");
                entity.Property(e => e.ShopId).HasColumnName("sr_bal_shop_id");
                entity.Property(e => e.Year).HasColumnName("sr_bal_year");
                entity.Property(e => e.Month).HasColumnName("sr_bal_month");
                entity.Property(e => e.FromDate).HasColumnName("sr_bal_from_date");
                entity.Property(e => e.ToDate).HasColumnName("sr_bal_to_date");
                entity.Property(e => e.BillProcessDate).HasColumnName("sr_bal_bill_process_date");
                entity.Property(e => e.ArrearsAmount).HasColumnName("sr_bal_arrears_amount");
                entity.Property(e => e.PaidArrearsAmount).HasColumnName("sr_bal_paid_arrears_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.FineAmount).HasColumnName("sr_bal_fine_amount");
                entity.Property(e => e.PaidFineAmount).HasColumnName("sr_bal_paid_fine_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.ServiceChargeArreasAmount).HasColumnName("sr_bal_service_charge_arreas_amount");
                entity.Property(e => e.PaidServiceChargeArreasAmount).HasColumnName("sr_bal_paid_service_charge_arreas_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.OverPaymentAmount).HasColumnName("sr_bal_over_payment_amount");
                entity.Property(e => e.IsCompleted).HasColumnName("sr_bal_is_completed");
                entity.Property(e => e.NoOfPayments).HasColumnName("sr_bal_no_of_payments");
                entity.Property(e => e.IsHold).HasColumnName("sr_bal_is_shop_hold");
                entity.Property(e => e.Status)
                        .HasColumnName("sr_bal_status")
                        .HasDefaultValueSql("'1'");

                entity.Property(e => e.OfficeId).HasColumnName("sr_bal_office_id");
                entity.Property(e => e.SabhaId).HasColumnName("sr_bal_sabha_id");

                //---- new modified : 2024/04/09-----
                entity.Property(e => e.HasTransaction).HasColumnName("sr_bal_has_transaction");
                //-----------------------------------


                //------[Start: fields for Report]-------
                entity.Property(e => e.LYFine).HasColumnName("sr_bal_ly_fine").HasDefaultValueSql("'0'");
                entity.Property(e => e.PaidLYFine).HasColumnName("sr_bal_paid_ly_fine").HasDefaultValueSql("'0'");
                
                entity.Property(e => e.LYArreas).HasColumnName("sr_bal_ly_arreas").HasDefaultValueSql("'0'");
                entity.Property(e => e.PaidLYArreas).HasColumnName("sr_bal_paid_ly_arreas").HasDefaultValueSql("'0'");
                
                entity.Property(e => e.TYFine).HasColumnName("sr_bal_ty_fine").HasDefaultValueSql("'0'");
                entity.Property(e => e.PaidTYFine).HasColumnName("sr_bal_paid_ty_fine").HasDefaultValueSql("'0'");
                
                entity.Property(e => e.TYArreas).HasColumnName("sr_bal_ty_arreas").HasDefaultValueSql("'0'");
                entity.Property(e => e.PaidTYArreas).HasColumnName("sr_bal_paid_ty_arreas").HasDefaultValueSql("'0'");

                entity.Property(e => e.TYLYServiceChargeArreas).HasColumnName("sr_bal_ly_ty_service_charge_arreas").HasDefaultValueSql("'0'");
                entity.Property(e => e.PaidTYLYServiceChargeArreas).HasColumnName("sr_bal_paid_ly_ty_service_charge_arreas").HasDefaultValueSql("'0'");

                entity.Property(e => e.CurrentServiceChargeAmount).HasColumnName("sr_bal_current_service_charge_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.PaidCurrentServiceChargeAmount).HasColumnName("sr_bal_paid_current_service_charge_amount").HasDefaultValueSql("'0'");
                
                entity.Property(e => e.CurrentRentalAmount).HasColumnName("sr_bal_current_rental_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.PaidCurrentRentalAmount).HasColumnName("sr_bal_paid_current_rental_amount").HasDefaultValueSql("'0'");

                entity.Property(e => e.CurrentMonthNewFine).HasColumnName("sr_bal_current_month_new_fine_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.PaidCurrentMonthNewFine).HasColumnName("sr_bal_paid_current_month_new_fine_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.SettledMixinOrderId).HasColumnName("sr_bal_settled_mixin_order_id");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
                //------[End: fields for Report]---------


                //------------[Start] Foreign key referencing---------------
                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Balances)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("sr_bal_FK_property");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Balances)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("sr_bal_FK_shop");
                //------------[End] Foreign key referencing-----------------


                //making unique filels
                entity.HasIndex(e => new { e.ShopId, e.Year, e.Month }).IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_bal_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_bal_created_by");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("sr_bal_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_bal_updated_by");

                entity.Property(e => e.IsProcessed)
                    .HasColumnName("sr_bal_is_processed")
                    .HasDefaultValueSql("'0'"); ;

                //Ignore field
                entity.Ignore(e => e.Customer);
            });
            //-------------------------------------------------------------------

            modelBuilder.Entity<ShopRentalBalanceLog>(entity =>
            {
                entity.ToTable("sr_shopRental_balance_log");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("sr_bal_lg_id");
                entity.Property(e => e.PropertyId).HasColumnName("sr_bal_lg_proprty_id");
                entity.Property(e => e.ShopId).HasColumnName("sr_bal_lg_shop_id");
                entity.Property(e => e.Year).HasColumnName("sr_bal_lg_year");
                entity.Property(e => e.Month).HasColumnName("sr_bal_lg_month");
                entity.Property(e => e.FromDate).HasColumnName("sr_bal_lg_from_date");
                entity.Property(e => e.ToDate).HasColumnName("sr_bal_lg__to_date");
                entity.Property(e => e.BillProcessDate).HasColumnName("sr_bal_lg_bill_process_date");
                entity.Property(e => e.PriviousArrearsAmount).HasColumnName("sr_bal_previous_arrears_amount");
                entity.Property(e => e.NewArrearsAmount).HasColumnName("sr_bal_new_arrears_amount");

                entity.Property(e => e.PreviousPaidArrearsAmount).HasColumnName("sr_bal_previous_paid_arrears_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewPaidArrearsAmount).HasColumnName("sr_bal_new_paid_arrears_amount").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousFineAmount).HasColumnName("sr_bal_previous_fine_amount");
                entity.Property(e => e.NewFineAmount).HasColumnName("sr_bal_new_fine_amount");

                entity.Property(e => e.PreviousPaidFineAmount).HasColumnName("sr_bal_previous_paid_fine_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewPaidFineAmount).HasColumnName("sr_bal_new_paid_fine_amount").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousServiceChargeArreasAmount).HasColumnName("sr_bal_previous_service_charge_arreas_amount");
                entity.Property(e => e.NewServiceChargeArreasAmount).HasColumnName("sr_bal_new_service_charge_arreas_amount");

                entity.Property(e => e.PreviousPaidServiceChargeArreasAmount).HasColumnName("sr_bal_previous_paid_service_charge_arreas_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewPaidServiceChargeArreasAmount).HasColumnName("sr_bal_new_paid_service_charge_arreas_amount").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousOverPaymentAmount).HasColumnName("sr_bal_previous_over_payment_amount");
                entity.Property(e => e.NewOverPaymentAmount).HasColumnName("sr_bal_over_new_payment_amount");

                entity.Property(e => e.IsCompleted).HasColumnName("sr_bal_is_completed");
                entity.Property(e => e.NoOfPayments).HasColumnName("sr_bal_no_of_payments");
                entity.Property(e => e.Status)
                        .HasColumnName("sr_bal_status")
                        .HasDefaultValueSql("'1'");

                entity.Property(e => e.OfficeId).HasColumnName("sr_bal_office_id");
                entity.Property(e => e.SabhaId).HasColumnName("sr_bal_sabha_id");

                //---- new modified : 2024/04/09-----
                entity.Property(e => e.HasTransaction).HasColumnName("sr_bal_has_transaction");
                //-----------------------------------


                //------[Start: fields for Report]-------
                entity.Property(e => e.PreviousLYFine).HasColumnName("sr_bal_lg_previous_ly_fine").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewLYFine).HasColumnName("sr_bal_lg_new_ly_fine").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousPaidLYFine).HasColumnName("sr_bal_lg_previous_paid_ly_fine").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewPaidLYFine).HasColumnName("sr_bal_lg_new_paid_ly_fine").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousLYArreas).HasColumnName("sr_bal_lg_previous_ly_arrears").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewLYArreas).HasColumnName("sr_bal_lg_new_ly_arrears").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousPaidLYArreas).HasColumnName("sr_bal_lg_previous_paid_ly_arrears").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewPaidLYArreas).HasColumnName("sr_bal_lg_new_paid_ly_arrears").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousTYFine).HasColumnName("sr_bal_lg_previous_ty_fine").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewTYFine).HasColumnName("sr_bal_lg_new_ty_fine").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousPaidTYFine).HasColumnName("sr_bal_lg_previous_paid_ty_fine").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewPaidTYFine).HasColumnName("sr_bal_lg_new_paid_ty_fine").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousTYArreas).HasColumnName("sr_bal_lg_previous_ty_arreas").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewTYArreas).HasColumnName("sr_bal_lg_new_ty_arreas").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousPaidTYArreas).HasColumnName("sr_bal_lg_previous_paid_ty_arreas").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewPaidTYArreas).HasColumnName("sr_bal_lg_new_paid_ty_arreas").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousTYLYServiceChargeArreas).HasColumnName("sr_bal_lg_previous_ly_ty_service_charge_arreas").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewTYLYServiceChargeArreas).HasColumnName("sr_bal_lg_new_ly_ty_service_charge_arreas").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousPaidTYLYServiceChargeArreas).HasColumnName("sr_bal_paid_lg_previous_ly_ty_service_charge_arreas").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewPaidTYLYServiceChargeArreas).HasColumnName("sr_bal_paid_lg_new_ly_ty_service_charge_arreas").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousCurrentServiceChargeAmount).HasColumnName("sr_bal_lg_previous_current_service_charge_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewPaidCurrentServiceChargeAmount).HasColumnName("sr_bal_lg_new_paid_current_service_charge_amount").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousCurrentRentalAmount).HasColumnName("sr_bal_lg_previous_current_rental_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewCurrentRentalAmount).HasColumnName("sr_bal_lg_new_current_rental_amount").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousPaidCurrentRentalAmount).HasColumnName("sr_bal_lg_previous_paid_current_rental_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewPaidCurrentRentalAmount).HasColumnName("sr_bal_lg_new_paid_current_rental_amount").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousCurrentMonthNewFine).HasColumnName("sr_bal_lg_precious_current_month_new_fine_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewCurrentMonthNewFine).HasColumnName("sr_bal_lg_new_current_month_new_fine_amount").HasDefaultValueSql("'0'");

                entity.Property(e => e.PreviousPaidCurrentMonthNewFine).HasColumnName("sr_bal_lg_previous_paid_current_month_new_fine_amount").HasDefaultValueSql("'0'");
                entity.Property(e => e.NewPaidCurrentMonthNewFine).HasColumnName("sr_bal_paid_lg_new_current_month_new_fine_amount").HasDefaultValueSql("'0'");

                entity.Property(e => e.SettledMixinOrderId).HasColumnName("sr_bal_settled_mixin_order_id");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
                //------[End: fields for Report]---------


                //------------[Start] Foreign key referencing---------------
                
                //------------[End] Foreign key referencing-----------------


                //making unique filels
                entity.HasIndex(e => new { e.ShopId, e.Year, e.Month }).IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_bal_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_bal_created_by");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("sr_bal_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_bal_updated_by");

                entity.Property(e => e.IsProcessed)
                    .HasColumnName("sr_bal_is_processed")
                    .HasDefaultValueSql("'0'"); ;

                //Ignore field
                entity.Ignore(e => e.Customer);
            });
            //-------------------------------------------------------------------
            modelBuilder.Entity<ShopRentalVoteAssign>(entity =>
            {
                entity.ToTable("sr_shopRental_vote_assign");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("sr_vote_id");
                entity.Property(e => e.PropertyId).HasColumnName("sr_vote_property_id");
                entity.Property(e => e.ShopId).HasColumnName("sr_vote_shop_id");

                //---------------- [Start - vote assignment detail id] -------------------------
                entity.Property(e => e.PropertyRentalVoteId).HasColumnName("sr_vote_property_rental_id");
                entity.Property(e => e.LastYearArreasAmountVoteId).HasColumnName("sr_vote_last_year_arreas_amount_id");
                entity.Property(e => e.ThisYearArrearsAmountVoteId).HasColumnName("sr_vote_this_year_arreas_amount_id");
                entity.Property(e => e.LastYearFineAmountVoteId).HasColumnName("sr_vote_last_year_fine_id");
                entity.Property(e => e.ThisYearFineAmountVoteId).HasColumnName("sr_vote_this_year_fine_id");
                entity.Property(e => e.ServiceChargeArreasAmountVoteId).HasColumnName("sr_vote_service_charge_arreas_amount_id");
                entity.Property(e => e.ServiceChargeAmountVoteId).HasColumnName("sr_vote_service_charge_id");
                entity.Property(e => e.OverPaymentAmountVoteId).HasColumnName("sr_vote_over_payment_id");
                //---------------- [End - vote assignment detail id] -------------------------


                //---------------- [Start - vote detail id fields] -------------------------
                entity.Property(e => e.PropertyRentalVoteDetailId).HasColumnName("sr_vote_detail_property_rental_id");
                entity.Property(e => e.LastYearArreasAmountVoteDetailId).HasColumnName("sr_vote_detail_last_year_arreas_amount_id");
                entity.Property(e => e.ThisYearArrearsAmountVoteDetailId).HasColumnName("sr_vote_detail_this_year_arreas_amount_id");
                entity.Property(e => e.LastYearFineAmountVoteDetailId).HasColumnName("sr_vote_detail_last_year_fine_id");
                entity.Property(e => e.ThisYearFineAmountVoteDetailId).HasColumnName("sr_vote_detail_this_year_fine_id");
                entity.Property(e => e.ServiceChargeArreasAmountVoteDetailId).HasColumnName("sr_vote_detail_service_charge_arreas_amount_id");
                entity.Property(e => e.ServiceChargeAmountVoteDetailId).HasColumnName("sr_vote_detail_service_charge_id");
                entity.Property(e => e.OverPaymentAmountVoteDetailId).HasColumnName("sr_vote_detail_over_payment_id");
                //---------------- [End - vote detail id fields] -------------------------

                //ignore - fields
                entity.Ignore(e => e.PropertyRentalVote);
                entity.Ignore(e => e.LastYearArreasAmountVote);
                entity.Ignore(e => e.ThisYearArrearsAmountVote);
                entity.Ignore(e => e.LastYearFineAmountVote);
                entity.Ignore(e => e.ThisYearFineAmountVote);
                entity.Ignore(e => e.ServiceChargeArreasAmountVote);
                entity.Ignore(e => e.ServiceChargeAmountVote);
                entity.Ignore(e => e.OverPaymentAmountVote);

                //making unique filels
                entity.HasIndex(e => new { e.ShopId }).IsUnique();

                //------------[Start] Foreign key referencing---------------
                entity.HasOne(d => d.Property)
                    .WithMany(p => p.ShopRentalVoteAssigns)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("sr_vote_FK_property");

                //shop - voteAssign --- 1:1
                entity.HasOne(w => w.Shop)
                  .WithOne(p => p.VoteAssign)
                  .HasForeignKey<ShopRentalVoteAssign>(v => v.ShopId)
                  .HasConstraintName("sr_vote_Fk_shop");
                //------------[End] Foreign key referencing-----------------

                entity.Property(e => e.Status).HasColumnName("sr_vote_status");

                entity.Property(e => e.OfficeId)
                    .HasColumnName("sr_vote_office_id")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SabhaId)
                    .HasColumnName("sr_vote_sabha_id")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_vote_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_vote_created_by");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("sr_vote_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_vote_updated_by");
            });
            //-------------------------------------------------------------------


            //-----------
            modelBuilder.Entity<ShopRentalVotePaymentType>(entity =>
            {
                entity.ToTable("sr_vote_payment_types");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_vt_pay_type_id");
                entity.Property(e => e.Description).HasColumnName("sr_vt_pay_type_desc");

                entity.Property(e => e.Status).HasColumnName("sr_vt_pay_type_status");
            });
            //-----------


            //-----------
            modelBuilder.Entity<ShopRentalProcessConfigaration>(entity =>
            {
                entity.ToTable("sr_process_configuration");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("sr_processConfig_id");
                entity.Property(e => e.SabhaId).HasColumnName("sr_processConfig_sabha_id");
                //-----
                entity.Property(e => e.Name).HasColumnName("sr_processConfig_group_name");
                entity.Property(e => e.FineRateTypeId).HasColumnName("sr_processConfig_fine_rate_type_id");
                entity.Property(e => e.FineDailyRate).HasColumnName("sr_processConfig_fine_daily_rate").HasDefaultValueSql("'0'");
                entity.Property(e => e.FineMonthlyRate).HasColumnName("sr_processConfig_fine_monthly_rate").HasDefaultValueSql("'0'");
                entity.Property(e => e.Fine1stMonthRate).HasColumnName("sr_processConfig_fine_1st_month_rate").HasDefaultValueSql("'0'");
                entity.Property(e => e.Fine2ndMonthRate).HasColumnName("sr_processConfig_fine_2nd_month_rate").HasDefaultValueSql("'0'");
                entity.Property(e => e.Fine3rdMonthRate).HasColumnName("sr_processConfig_fine_3rd_month_rate").HasDefaultValueSql("'0'");
                entity.Property(e => e.FineFixAmount).HasColumnName("sr_processConfig_fine_fixed_amount").HasDefaultValueSql("'0'");
                //-----
                entity.Property(e => e.FineDate).HasColumnName("sr_processConfig_fine_date");
                entity.Property(e => e.RentalPaymentDateTypeId).HasColumnName("sr_processConfig_rental_payment_date_type_id");
                entity.Property(e => e.FineCalTypeId).HasColumnName("sr_processConfig_fine_cal_type_id");
                entity.Property(e => e.FineChargingMethodId).HasColumnName("sr_processConfig_fine_charging_method_id");

                entity.Property(e => e.Status).HasColumnName("sr_processConfig_status");
           

                //------------[Start] Foreign key referencing---------------
                entity.HasOne(fr => fr.FineRateType)
                    .WithMany(pc => pc.ShopRentalProcessConfigarations)
                    .HasForeignKey(fr => fr.FineRateTypeId)
                    .HasConstraintName("sr_processConfig_ibfk_1");

               entity.HasOne(fct => fct.FineCalType)
                    .WithMany(pc => pc.ShopRentalProcessConfigarations)
                    .HasForeignKey(fct => fct.FineCalTypeId)
                    .HasConstraintName("sr_processConfig_ibfk_2");

               entity.HasOne(fpd => fpd.RentalPaymentDateType)
                    .WithMany(pc => pc.ShopRentalProcessConfigarations)
                    .HasForeignKey(fpd => fpd.RentalPaymentDateTypeId)
                    .HasConstraintName("sr_processConfig_ibfk_3");

                entity.HasOne(fcm => fcm.FineChargingMethod)
                    .WithMany(pc => pc.ShopRentalProcessConfigarations)
                    .HasForeignKey(fcm => fcm.FineChargingMethodId)
                    .HasConstraintName("sr_processConfig_ibfk_4");



                //------------[End] Foreign key referencing-----------------

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_processConfig_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_processConfig_created_by");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("sr_processConfig_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_processConfig_updated_by");

                entity.Ignore(e => e.Sabha); //ignore field
            });
            //-----------


            //-----------
            modelBuilder.Entity<ShopAgreementChangeRequest>(entity =>
            {
                entity.ToTable("sr_agreement_change_request");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("sr_agcr_id");
                entity.Property(e => e.ShopId).HasColumnName("sr_agcr_shop_id");
                entity.Property(e => e.Requestedstatus).HasColumnName("sr_agcr_requestedstatus");
                entity.Property(e => e.AgreementCloseDate).HasColumnName("sr_agcr_agreement_close_date");
                entity.Property(e => e.AgreementExtendEndDate).HasColumnName("sr_agcr_agreement_extended_end_date");
                entity.Property(e => e.RequestType).HasColumnName("sr_agcr_requestType");

                entity.Property(e => e.OfficeId).HasColumnName("sr_agcr_office_id");
                entity.Property(e => e.SabhaId).HasColumnName("sr_agcr_sabha_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_agcr_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_agcr_created_by");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("sr_agcr_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_agcr_updated_by");

                entity.Property(e => e.ApprovedAt)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("sr_agcr_approved_at");

                entity.Property(e => e.ApprovedBy).HasColumnName("sr_agcr_approved_by");

                entity.Property(e => e.ApproveComment).HasColumnName("sr_agcr_approve_comment");
                entity.Property(e => e.AgreementChangeReason).HasColumnName("sr_agcr_description");

                entity.Property(e => e.ApproveStatus)
                    .HasColumnName("sr_ob_approve_status")
                    .HasDefaultValueSql("'0'");
                //--------------


                //Foreign key referencing-----------------------------------
                entity.HasOne(w => w.Shop)
                  .WithOne(p => p.ShopAgreementChangeRequest)
                  .HasForeignKey<ShopAgreementChangeRequest>(o => o.ShopId)
                  .HasConstraintName("sr_agcr_shop_FK");
                //----------------------------------------------------------

                entity.HasKey(e => e.Id).HasName("PrimaryKey_agcrId");
            });
            //-----------
            //------
            modelBuilder.Entity<ShopAgreementActivityLog>(entity =>
            {
                entity.ToTable("sr_shop_agr_activity_log");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_shop_agr_activity_log_id");

                entity.Property(e => e.ShopId)
                    .HasMaxLength(45)
                    .HasColumnName("sr_shop_agr_activity_log_shop_id");

                entity.Property(e => e.OfficeId).HasColumnName("sr_shop_agr_activity_log_office_id");
                entity.Property(e => e.CurrentAgreementEnddate).HasColumnName("sr_shop_agr_activity_log_current_agreement_end_date");
                entity.Property(e => e.AgreementExtendEndDate).HasColumnName("sr_shop_agr_activity_log_agreement_extended_date");
                entity.Property(e => e.ApprovedBy).HasColumnName("sr_shop_agr_activity_log_approved_by");
                entity.Property(e => e.ApproveComment).HasColumnName("sr_shop_agr_activity_log_approve_comment");
                entity.Property(e => e.SabhaId).HasColumnName("sr_shop_agr_activity_log_sabha_id");

                entity.Property(e => e.CreatedAt)
                   .HasColumnType("datetime")
                   .HasColumnName("sr_shop_agr_activity_log_created_at")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_shop_agr_activity_log_created_by");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("sr_shop_agr_activity_log_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_shop_agr_activity_log_updated_by");
            });
            //------
            modelBuilder.Entity<FineCalType>(entity =>
            {
                entity.ToTable("sr_fine_cal_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_fine_cal_type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("sr_fine_cal_type_name");

                entity.Property(e => e.Status).HasColumnName("sr_fine_cal_type_status");
            });
            //------

            //-----
            modelBuilder.Entity<FineChargingMethod>(entity =>
            {
                entity.ToTable("sr_fine_charging_method");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_fine_charging_method_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("sr_fine_charging_method_name");

                entity.Property(e => e.Status).HasColumnName("sr_fine_charging_method_status");
            });
            //-----

            //-----
            modelBuilder.Entity<FineRateType>(entity =>
            {
                entity.ToTable("sr_fine_rate_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_fine_rate_type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("sr_fine_rate_type_name");

                entity.Property(e => e.Status).HasColumnName("sr_fine_rate_type_status");
            });
            //-----

            //-----
            modelBuilder.Entity<RentalPaymentDateType>(entity =>
            {
                entity.ToTable("sr_rental_payment_data_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_rental_payment_data_type_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("sr_rental_payment_data_type_name");

                entity.Property(e => e.Status).HasColumnName("sr_rental_payment_data_type_status");
            });
            //-----

            //-----
            modelBuilder.Entity<ProcessConfigurationSettingAssign>(entity =>
            {
                entity.ToTable("sr_process_configuration_setting_assign");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_process_configuration_setting_assign_id");
                entity.Property(e => e.ShopId).HasColumnName("sr_process_configuration_setting_assign_shop_id");
                entity.Property(e => e.ShopRentalProcessConfigarationId).HasColumnName("sr_process_configuration_setting_assign_process_configaration_id");

                entity.Property(e => e.Status).HasColumnName("sr_process_configuration_setting_assign_status");
                entity.Property(e => e.SabhaId).HasColumnName("sr_process_configuration_setting_assign_sabha_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_process_configuration_setting_assign_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_process_configuration_setting_assign_created_by");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("sr_process_configuration_setting_assign_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_process_configuration_setting_assign_updated_by");

                //Foreign key referencing-----------------------------------
                //Mapping 1(Shop): 1(ProcessConfigurationSettingAssign)
                entity.HasOne(s => s.Shop)
                  .WithOne(pca => pca.ProcessConfigurationSettingAssign)
                  .HasForeignKey<ProcessConfigurationSettingAssign>(pca => pca.ShopId)
                  .HasConstraintName("sr_process_configuration_setting_assign_ibfk_1");

                //Mapping 1(ShopRentalProcessConfigaration): Many (ProcessConfigurationSettingAssign)
                entity.HasOne(pc => pc.ShopRentalProcessConfigaration)
                    .WithMany(pca => pca.ProcessConfigurationSettingAssigns)
                    .HasForeignKey(pc => pc.ShopRentalProcessConfigarationId)
                    .HasConstraintName("sr_process_configuration_setting_assign_ibfk_2");
                //Foreign key referencing-----------------------------------
            });
            //-----

            //-----
            modelBuilder.Entity<ShopRentalProcess>(entity =>
            {
                entity.ToTable("shopRental_processes");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_process_id");
                entity.Property(e => e.ActionBy).HasColumnName("sr_process_action_by");
                entity.Property(e => e.Date).HasColumnName("sr_process_date");

                //entity.Property(e => e.Year).HasColumnName("sr_process_year");
                //entity.Property(e => e.Month).HasColumnName("sr_process_month");
                //entity.Property(e => e.Day).HasColumnName("sr_process_day");

                entity.Property(e => e.ShabaId).HasColumnName("sr_process_sbaha_id");
                entity.Property(e => e.ProcessType).HasColumnName("sr_process_type");
                entity.Property(e => e.ProceedDate).HasColumnName("sr_proceed_date").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.BackUpKey).HasColumnName("sr_process_backupkey");
                entity.Property(e => e.ProcessConfigId).HasColumnName("sr_process_config_id");//fing group id

                entity.Property(e => e.IsSkippeed).HasColumnName("sr_process_IsSkipped");
                entity.Property(e => e.Description).HasColumnName("sr_process_description");
                entity.HasOne(pc => pc.ShopRentalProcessConfigaration)
                   .WithMany(pca => pca.ShopRentalProcess)
                   .HasForeignKey(pc => pc.ProcessConfigId)
                   .HasConstraintName("sr_process_configuration_ibfk_2");
            });
            //-----

            modelBuilder.Entity<DailyFineProcessLog>(entity =>
            {
                entity.ToTable("shopRental_dailyfineprocesslog");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                entity.Property(e => e.Id).HasColumnName("sr_dailyfineprocesslog_id");
                entity.Property(e => e.NoOfDates).HasColumnName("sr_dailyfineprocesslog_no_of_dates");
                entity.Property(e => e.DailyFineRate).HasColumnName("sr_dailyfineprocesslog_daily_fine_rate");

                //entity.Property(e => e.Year).HasColumnName("sr_process_year");
                //entity.Property(e => e.Month).HasColumnName("sr_process_month");
                //entity.Property(e => e.Day).HasColumnName("sr_process_day");

                entity.Property(e => e.DailyFixedFineAmount).HasColumnName("sr_dailyfineprocesslog_daily_fixed_amount");
                entity.Property(e => e.TotalFineAmount).HasColumnName("sr_dailyfineprocesslog_total_fine_amount");
                entity.Property(e => e.ShopId).HasColumnName("sr_dailyfineprocesslog_shop_id");
                entity.Property(e => e.ProcessConfigurationId).HasColumnName("sr_dailyfineprocesslog_process_configuration_id");
                entity.Property(e => e.CreatedAt).HasColumnName("sr_dailyfineprocesslog_created_at");
                entity.Property(e => e.CreatedBy).HasColumnName("sr_dailyfineprocesslog_created_by");
                entity.Property(e => e.BalanceId).HasColumnName("sr_dailyfineprocesslog_balance_id");

            });
            //-----




            //-------------------------------------------------------------------
            modelBuilder.Entity<ShopRentalRecievableIncomeVoteAssign>(entity =>
            {
                entity.ToTable("sr_shopRental_receivable_income_vote_assign");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("sr_ri_vote_id");
                entity.Property(e => e.PropertyId).HasColumnName("sr_ri_vote_property_id");
                entity.Property(e => e.ShopId).HasColumnName("sr_ri_vote_shop_id");

                //---------------- [Start - vote detail id] -------------------------
                entity.Property(e => e.PropertyRentalIncomeVoteId).HasColumnName("sr_ri_vote_property_rental_income_id");
                entity.Property(e => e.PropertyServiceChargeIncomeVoteId).HasColumnName("sr_ri_vote_property_service_charge_income_id");
                entity.Property(e => e.PropertyFineIncomeVoteId).HasColumnName("sr_ri_vote_property_fine_income_id");
                //---------------- [End - vote detail id] -------------------------

                //ignore - fields

                //making unique filels
                entity.HasIndex(e => new { e.ShopId }).IsUnique();

                //------------[Start] Foreign key referencing---------------
                entity.HasOne(ri => ri.Property)
                    .WithMany(p => p.ShopRentalRecievableIncomeVoteAssigns)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("sr_ri_vote_FK_property");

                //shop - voteAssign --- 1:1
                entity.HasOne(ri => ri.Shop)
                  .WithOne(s => s.RecievableIncomeVoteAssign)
                  .HasForeignKey<ShopRentalRecievableIncomeVoteAssign>(v => v.ShopId)
                  .HasConstraintName("sr_ri_vote_Fk_shop");
                //------------[End] Foreign key referencing-----------------

                entity.Property(e => e.Status).HasColumnName("sr_ri_vote_status");

                entity.Property(e => e.OfficeId)
                    .HasColumnName("sr_ri_vote_office_id");

                entity.Property(e => e.SabhaId)
                    .HasColumnName("sr_ri_vote_sabha_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_ri_vote_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_ri_vote_created_by");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("sr_ri_vote_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_ri_vote_updated_by");
            });
            //-------------------------------------------------------------------



            //-------------------------------------------------------------------
            modelBuilder.Entity<ShopDeposit>(entity =>
            {
                entity.ToTable("sr_shop_deposits");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_sinhala_ci");

                // add naming conventions
                entity.Property(e => e.Id).HasColumnName("sr_shop_deposit_id");
                entity.Property(e => e.ShopId).HasColumnName("sr_shop_deposit_shop_id");
                entity.Property(e => e.DepositId).HasColumnName("sr_shop_deposit_depositID");
                entity.Property(e => e.DepositDate).HasColumnName("sr_shop_deposit_date");
                entity.Property(e => e.MixOrderId).HasColumnName("sr_shop_deposit_mixinOrder_id");
                entity.Property(e => e.MixOrderLineId).HasColumnName("sr_shop_deposit_mixinOrderLine_id");
                entity.Property(e => e.ReceiptNo).HasColumnName("sr_shop_deposit_receiptNo");
                entity.Property(e => e.DepositAmount).HasColumnName("sr_shop_deposit_amount");
                entity.Property(e => e.SessionId).HasColumnName("sr_shop_deposit_session_id");
                entity.Property(e => e.Type).HasColumnName("sr_shop_deposit_type");
                entity.Property(e => e.IsFullyRefund).HasColumnName("sr_shop_deposit_isFullyRefund");
                entity.Property(e => e.SessionId).HasColumnName("sr_shop_deposit_session_id");

                entity.Property(e => e.Status).HasColumnName("sr_shop_deposit_status");

                entity.Property(e => e.OfficeId)
                    .HasColumnName("sr_shop_deposit_office_id");

                entity.Property(e => e.SabhaId)
                    .HasColumnName("sr_shop_deposit_sabha_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sr_shop_deposit_created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("sr_shop_deposit_created_by");

                entity.Property(e => e.UpdatedAt)
                   .HasColumnType("datetime")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasColumnName("sr_shop_deposit_updated_at");

                entity.Property(e => e.UpdatedBy).HasColumnName("sr_shop_deposit_updated_by");

                entity.Property(e => e.RowVersion).HasColumnName("row_version");
                entity.Property(e => e.RowVersion).IsConcurrencyToken();
            });
            //-------------------------------------------------------------------


            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
