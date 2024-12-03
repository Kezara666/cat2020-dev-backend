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
using CAT20.Core.Models.AuditTrails;
using System.ComponentModel.DataAnnotations.Schema;
using CAT20.Core.Models.AssessmentAuditActivity;


namespace CAT20.Data
{
    public partial class AuditTrailDbContext : DbContext
    {
        public AuditTrailDbContext()
        {
        }

        public AuditTrailDbContext(DbContextOptions<AuditTrailDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditTrail> AuditTrails { get; set; }
        public virtual DbSet<AuditTrailDetail> AuditTrailDetails { get; set; }
        public virtual DbSet<AssessmentUserActivity> AssessmentUserActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");


            modelBuilder.Entity<AuditTrail>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.ToTable("AuditTrails");

                entity.Property(t => t.EntityType).HasColumnName("EntityType");
                entity.Property(t => t.EntityID).HasColumnName("EntityID").HasColumnType("int");
                entity.Property(t => t.ClaimedUserID).HasColumnName("UserID").HasColumnType("int");
                entity.Property(t => t.Date).HasColumnName("Date").HasColumnType("datetime");
                entity.Property(t => t.Action).HasColumnName("Action").HasColumnType("int");
                entity.Property(t => t.ClaimedOfficeID).HasColumnName("OfficeID").HasColumnType("int");
                entity.Property(t => t.ClaimedSabhaID).HasColumnName("SabhaID").HasColumnType("int");
                entity.Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).ValueGeneratedOnAddOrUpdate();

                entity.Ignore(t => t.AuditReference);
                entity.Ignore(t => t.State);
                entity.Ignore(t => t.Message);
                entity.Ignore(t => t.ServiceStatus);
                entity.Ignore(t => t.UpdatedAt);
                entity.Ignore(t => t.CreatedAt);
                entity.Ignore(t => t.CreatedBy);
                entity.Ignore(t => t.UpdatedBy);
            });


            modelBuilder.Entity<AuditTrailDetail>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.ToTable("AuditTrailDetails");

                entity.Property(e => e.AuditTrailID).HasColumnName("AuditTrailID").HasColumnType("int").IsRequired();
                entity.Property(e => e.EntityType).HasColumnName("EntityType");
                entity.Property(e => e.EntityID).HasColumnName("EntityID").HasColumnType("int");
                entity.Property(e => e.Property).HasColumnName("Property").HasColumnType("nvarchar").HasMaxLength(200);
                entity.Property(e => e.PreviousValue).HasColumnName("PreviousValue").HasColumnType("nvarchar").HasMaxLength(500);
                entity.Property(e => e.NewValue).HasColumnName("NewValue").HasColumnType("nvarchar").HasMaxLength(500);
                entity.Property(e => e.Action).HasColumnName("Action").HasColumnType("int");

                entity.Property(t => t.UserID).HasColumnName("UserID").HasColumnType("int").HasMaxLength(50);
                entity.Property(t => t.TimeStamp).HasColumnName("TimeStamp").HasColumnType("Timestamp").IsConcurrencyToken(true).ValueGeneratedOnAddOrUpdate();
                entity.Property(t => t.CreatedAt).HasColumnName("CreatedAt").HasColumnType("datetime");
                entity.Property(t => t.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("datetime");

                entity.Ignore(e => e.AuditReference);
                entity.Ignore(e => e.State);
                entity.Ignore(t => t.UpdatedAt);
                entity.Ignore(t => t.CreatedAt);
            });




            modelBuilder.Entity<AssessmentUserActivity>(entity => { 
            
                entity.HasKey(e => e.Id);
                entity.ToTable("assessment_User_Activity");


                entity.Property(e => e.key).HasColumnName("key").HasColumnType("int").IsRequired();
                entity.Property(e => e.Entity).HasColumnName("entity").HasColumnType("int");
                entity.Property(e => e.serviceNo).HasColumnName("service_no").HasColumnType("int");
                entity.Property(e => e.Action).HasColumnName("action").HasColumnType("int");
                entity.Property(e => e.Timestamp).HasColumnName("time_stamp").HasColumnType("datetime");
                entity.Property(e => e.ActionBy).HasColumnName("action_by").HasColumnType("int");


            
            });


            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}