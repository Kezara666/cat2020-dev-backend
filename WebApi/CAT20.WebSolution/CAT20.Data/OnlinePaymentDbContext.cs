using System.Text.Json;
using CAT20.Core.Models.OnlienePayment;
using CAT20.Core.Models.OnlinePayment;
using CAT20.Core.Models.TradeTax;
using Microsoft.EntityFrameworkCore;

namespace CAT20.Data;

public partial class OnlinePaymentDbContext : DbContext
{
    public OnlinePaymentDbContext()
    {
    }
    public OnlinePaymentDbContext(DbContextOptions<OnlinePaymentDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<PaymentDetails> PaymentDetails { get; set; }
    public virtual DbSet<PaymentGateway> PaymentGateway { get; set; }

    public virtual DbSet<LogInDetails> LogInDetails { get; set; }
    public virtual DbSet<PaymentDetailBackUp> PaymentDetailBackUp { get; set; }
    public virtual DbSet<OtherDescription> OtherDescription { get; set; }
    public virtual DbSet<Dispute> Dispute { get; set; }

    public virtual DbSet<BookingProperty> BookingProperty { get; set; }

    public virtual DbSet<BookingSubProperty> BookingSubProperty { get; set; }
    public virtual DbSet<ChargingScheme> ChargingScheme { get; set; }
    public virtual DbSet<BookingTimeSlot> BookingTimeSlots { get; set; }

    public virtual DbSet<OnlineBooking> OnlineBookings { get; set; }
    public virtual DbSet<BookingDate> BookingDates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentDetails>(entity =>
            {
                entity.ToTable("payment_details");

                entity.HasKey(e => e.PaymentDetailId);

                entity.Property((e => e.PaymentDetailId))
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
                entity.Property(e => e.SessionId).HasColumnName("session_id");
                entity.Property(e => e.ResultIndicator).HasColumnName("result_indicator");
                entity.Property(e => e.Status).HasColumnName("status").HasDefaultValue(0);
                entity.Property(e => e.Error).HasColumnName("error").HasDefaultValue(0);
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.InputAmount).HasColumnName("input_amount").HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Discount).HasColumnName("discount").HasColumnType("decimal(18, 2)");
                entity.Property(e => e.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ServicePercentage).HasColumnName("service_percentage").HasColumnType("decimal(18, 3)");
                entity.Property(e => e.ServiceCharges).HasColumnName("service_charges").HasColumnType("decimal(18, 2)");
                entity.Property(e => e.OrderId).HasColumnName("unique_id");
                entity.Property(e => e.AccountNo).HasColumnName("account_no");
                entity.Property(e => e.PartnerName).HasColumnName("partner_name");
                entity.Property(e => e.PartnerId).HasColumnName("partner_id");
                entity.Property(e => e.PartnerNIC).HasColumnName("partner_nic");
                entity.Property(e => e.PartnerMobileNo).HasColumnName("partner_mobile_no");
                entity.Property(e => e.PartnerEmail).HasColumnName("partner_email_address");
                entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
                entity.Property(e => e.OfficeId).HasColumnName("office_id");
                entity.Property(e => e.OfficeSessionId).HasColumnName("office_session_id");
                entity.Property(e => e.CashierId).HasColumnName("cashier_id").HasDefaultValue(-1);
                entity.Property(e => e.Check).HasColumnName("check").HasDefaultValue(0);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.CashierUpdatedAt).HasColumnName("cashier_updated_at");

                // entity.Property(e => e.MixinId).HasColumnName("mixin_id");
                
                entity.HasOne(e => e.OtherDescription)
                    .WithOne(e => e.PaymentDetails)
                    .HasForeignKey<OtherDescription>(e => e.Id);

                entity.HasOne(pd => pd.Dispute)
                    .WithOne(d => d.PaymentDetails)
                    .HasForeignKey<Dispute>(d => d.PaymentDetailId);


                // entity.Property(e => e.CreatedAt)
                //     .HasColumnType("datetime")
                //     .HasColumnName("payment_time")
                //     .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

        modelBuilder.Entity<LogInDetails>(entity =>
        {
            entity.ToTable("logIn_details");

            entity.HasKey(e => e.logInID).HasName("log_in_id");
            entity.Property(e => e.NIC).HasColumnName("nic");
            entity.Property(e => e.MobileNo).HasColumnName("mobile_no");
            entity.Property(e => e.IpAddress).HasColumnName("ip_address");
            entity.Property(e => e.Device).HasColumnName("device");
            entity.Property(e => e.OperatingSystem).HasColumnName("operating_system");
            entity.Property(e => e.OsVersion).HasColumnName("os_version");
            entity.Property(e => e.Browser).HasColumnName("browser");
            entity.Property(e => e.BrowserVersion).HasColumnName("browser_version");
            entity.Property(e => e.DeviceType).HasColumnName("device_type");
            entity.Property(e => e.Orientation).HasColumnName("orientation");


            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("log_in_time")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<PaymentGateway>(entity =>
        {
            entity.ToTable("payment_gateways");

            entity.HasKey(e => e.Id).HasName("id");
            entity.Property(e => e.SabhaId).HasColumnName("sabha_id");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.BankName).HasColumnName("bank_name");
            entity.Property(e => e.MerchantId).HasColumnName("merchant_id");
            entity.Property(e => e.APIKey).HasColumnName("api_key");
            entity.Property(e => e.ReportAPIKey).HasColumnName("report_api_key");
            entity.Property(e => e.ServicePercentage).HasColumnName("service_percentage").HasColumnType("decimal(18, 3)");
        });

        modelBuilder.Entity<PaymentDetailBackUp>(entity =>
        {
            entity.ToTable("payment_detail_backup");
            entity.HasKey(e => e.Id).HasName("id");
            entity.Property(e => e.PaymentDetailId).HasColumnName("payment_detail_id");
            entity.Property(e => e.Status).HasColumnName("status").HasDefaultValue(0);
        });

        modelBuilder.Entity<OtherDescription>(entity =>
        {
            entity.ToTable("other_description");
            entity.HasKey(e => e.Id).HasName("id");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasOne(o => o.PaymentDetails)
                .WithOne(p => p.OtherDescription)
                .HasForeignKey<OtherDescription>(o => o.Id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OtherDescription_Payment_Detail_Id");
        });


        modelBuilder.Entity<Dispute>(entity =>
        {
            entity.ToTable("dispute");
            entity.HasKey(e => e.Id).HasName("id");
            entity.Property(e => e.PaymentDetailId).HasColumnName("payment_detail_id");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.Property(e => e.Message).HasColumnName("message");


            entity.HasOne(d => d.PaymentDetails)
                .WithOne(p => p.Dispute)
                .HasForeignKey<Dispute>(d => d.PaymentDetailId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Dispute_Payment_Detail_Id");

        });

        modelBuilder.Entity<BookingProperty>(entity =>
        {
            entity.ToTable("booking_main_property_type");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_sinhala_ci");

            entity.Property(e => e.ID).HasColumnName("booking_property_id");

            entity.Property(e => e.PropertyName)
                .HasColumnName("booking_main_property_name");

            entity.Property(e => e.Status).HasColumnName("booking_main_property_status");
            entity.Property(e => e.Code).HasColumnName("booking_main_property_code");

            entity.Property(e => e.SabhaID).HasColumnName("sabha_id");
            entity.Property(e => e.CreatedBy).HasColumnName("booking_main_property_created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("booking_main_property_updated_by");
            entity.Property(e => e.CreatedAt).HasColumnName("booking_main_property_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("booking_main_property_updated_at");
        });


        modelBuilder.Entity<BookingSubProperty>(entity =>
        {
            entity.ToTable("booking_sub_property_type");

            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_sinhala_ci");

            entity.Property(e => e.ID).HasColumnName("booking_sub_property_id");

            //entity.HasIndex(e => e.BusinessNatureID, "fk_business_subnature_business_nature_id");

            entity.Property(e => e.SubPropertyName)
                .HasColumnName("booking_sub_property_name");

            entity.Property(e => e.Status)
                .HasColumnName("booking_sub_property_status");
            entity.Property(e => e.Code)
              .HasColumnName("booking_sub_property_code");

            entity.Property(e => e.Address)
             .HasColumnName("booking_sub_property_address");
            entity.Property(e => e.TelephoneNumber)
            .HasColumnName("booking_sub_property_telephone_number");
            entity.Property(e => e.Latitude)
          .HasColumnName("booking_sub_property_latitude");
            entity.Property(e => e.Longitude)
         .HasColumnName("booking_sub_property_longitude");
            entity.Property(e => e.SabhaID).HasColumnName("sabha_id");
            entity.Property(e => e.PropertyID).HasColumnName("booking_main_property_id");
            entity.Property(e => e.CreatedBy).HasColumnName("booking_sub_property_created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("booking_sub_property_updated_by");
            entity.Property(e => e.CreatedAt).HasColumnName("booking_sub_property_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("booking_sub_property_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();

            entity.HasOne(d => d.bookingProperty)
                .WithMany(p => p.bookingSubProperties)
                .HasForeignKey(d => d.PropertyID)
                .HasConstraintName("fk_booking_sub_property_booking_main_property_id");
        });

        modelBuilder.Entity<BookingTimeSlot>(entity =>
        {
            entity.ToTable("booking_time_slot");
            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_sinhala_ci");

            entity.Property(e => e.Id).HasColumnName("booking_time_slot_id");

            entity.Property(e => e.SubPropertyId)
                .HasColumnName("booking_time_slot_subproperty_id");

            entity.Property(e => e.Description)
                .HasColumnName("booking_time_slot_description");
            entity.Property(e => e.From)
              .HasColumnName("booking_time_slot_from_time");

            entity.Property(e => e.To)
             .HasColumnName("booking_time_slot_to_time");
            entity.Property(e => e.OrderLevel)
            .HasColumnName("booking_time_slot_oreder_level");
      
            entity.Property(e => e.SabhaId).HasColumnName("booking_time_slot_sabha_id");

            entity.Property(e => e.BookingTimeSlotStatus).HasColumnName("booking_time_slot_status");

            entity.Property(e => e.CreatedBy).HasColumnName("booking_time_slot_created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("booking_time_slot_updated_by");
            entity.Property(e => e.CreatedAt).HasColumnName("booking_time_slot_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("booking_time_slot_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();

            entity.HasOne(d => d.bookingSubProperty)
             .WithMany(p => p.BookingTimeSlots)
             .HasForeignKey(d => d.SubPropertyId)
             .HasConstraintName("fk_booking_sub_property_booking_time_slot_id");
        });


        modelBuilder.Entity<OnlineBooking>(entity =>
        {
            entity.ToTable("online_Bookings");
            entity.Property(e => e.Id).HasColumnName("online_booking_id");

            entity.Property(e => e.PropertyId)
                .HasColumnName("online_booking_property_id");

            entity.Property(e => e.SubPropertyId).HasColumnName("online_booking_sub_property_id");
            entity.Property(e => e.CustomerId).HasColumnName("online_booking_customer_id");

            entity.Property(e => e.CreationDate).HasColumnName("online_booking_creation_date");
          

            entity.Property(e => e.BookingTimeSlotIds)
           .HasColumnName("online_booking_booking_time_slot_ids")
           .HasConversion(
               v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
               v => JsonSerializer.Deserialize<string[]>(v, (JsonSerializerOptions)null));
            entity.Property(e => e.BookingStatus).HasColumnName("online_booking_status");
            entity.Property(e => e.SabhaId).HasColumnName("online_booking_sabha_id");
            entity.Property(e => e.TotalAmount).HasColumnName("online_booking_total_amount");
            entity.Property(e => e.PaymentStatus).HasColumnName("online_booking_payament_status");
            entity.Property(e => e.TransactionId).HasColumnName("online_booking_transaction_id");
            entity.Property(e => e.ApprovedBy).HasColumnName("online_booking_approved_by");
            entity.Property(e => e.ApprovedAt).HasColumnName("online_booking_approved_at");
            entity.Property(e => e.RejectionReason).HasColumnName("online_booking_rejected_reason");
            entity.Property(e => e.CancellatioReason).HasColumnName("online_booking_cansellation_reason");
            entity.Property(e => e.BookingNotes).HasColumnName("online_booking_booking_notes");
            // entity.Property(e => e.CreatedBy).HasColumnName("booking_main_property_created_by");
            //entity.Property(e => e.UpdatedBy).HasColumnName("booking_main_property_updated_by");
            entity.Property(e => e.CreatedAt).HasColumnName("booking_time_slot_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("booking_time_slot_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();
        });

        
        modelBuilder.Entity<ChargingScheme>(entity =>
        {
            entity.ToTable("booking_charging_scheme");
            entity.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_sinhala_ci");

            entity.Property(e => e.ID).HasColumnName("booking_cs_id");

            entity.Property(e => e.SubPropertyId)
                .HasColumnName("booking_cs_subproperty_id");

            entity.Property(e => e.ChargingType).HasColumnName("booking_cs_charging_type");

            entity.Property(e => e.Amount).HasColumnName("booking_cs_amount");


            entity.Property(e => e.Status).HasColumnName("booking_cs_status").HasDefaultValue(1); ;
            entity.Property(e => e.SabhaID).HasColumnName("booking_cs_sabha_id");
            entity.Property(e => e.CreatedBy).HasColumnName("booking_cs_created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("booking_cs_updated_by");
            entity.Property(e => e.CreatedAt).HasColumnName("booking_cs_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime").ValueGeneratedOnAddOrUpdate().HasColumnName("booking_cs_updated_at");

            entity.HasOne(d => d.BookingSubProperty)
                .WithMany(p => p.chargingSchemes)
                .HasForeignKey(d => d.SubPropertyId);
        });

        modelBuilder.Entity<BookingDate>(entity =>
        {
            entity.ToTable("booking_date");

            entity.HasKey(e => e.Id).HasName("id");

            entity.Property(e => e.PropertyId).HasColumnName("booking_dates_property_id");
            entity.Property(e => e.SubPropertyId).HasColumnName("booking_dates_sub_property_id");
            entity.Property(e => e.OnlineBookingId).HasColumnName("booking_dates_online_booking_id");

            entity.Property(e => e.BookingStatus).HasColumnName("booking_dates_booking_status");
            entity.Property(e => e.StartDate).HasColumnName("booking_dates_start_date");
            entity.Property(e => e.EndDate).HasColumnName("booking_dates_end_date");
            entity.Property(e => e.CreatedBy).HasColumnName("booking_dates_created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("booking_dates_updated_by");
            entity.Property(e => e.CreatedAt).HasColumnName("booking_dates_created_at").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("booking_dates_updated_at").HasColumnType("datetime").ValueGeneratedOnAddOrUpdate();

            

            // If using a JSON array or custom storage for BookingTimeSlotIds
            entity.Property(e => e.BookingTimeSlotIds)
                  .HasColumnName("booking_time_slot_ids")
                  .HasConversion(
                      v => string.Join(",", v),
                      v => v.Split(",", StringSplitOptions.RemoveEmptyEntries));
        });


        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}