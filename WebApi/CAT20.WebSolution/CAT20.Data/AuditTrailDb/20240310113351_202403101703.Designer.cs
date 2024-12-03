﻿// <auto-generated />
using System;
using CAT20.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CAT20.Data.AuditTrailDb
{
    [DbContext(typeof(AuditTrailDbContext))]
    [Migration("20240310113351_202403101703")]
    partial class _202403101703
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("CAT20.Core.Models.AuditTrails.AuditTrail", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Action")
                        .HasColumnType("int")
                        .HasColumnName("Action");

                    b.Property<int?>("ClaimedOfficeID")
                        .HasColumnType("int");

                    b.Property<int?>("ClaimedSabhaID")
                        .HasColumnType("int");

                    b.Property<int?>("ClaimedUserID")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("Date");

                    b.Property<int?>("EntityID")
                        .HasColumnType("int")
                        .HasColumnName("EntityID");

                    b.Property<int>("EntityType")
                        .HasColumnType("int")
                        .HasColumnName("EntityType");

                    b.Property<DateTime?>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("Timestamp")
                        .HasColumnName("TimeStamp");

                    b.HasKey("Id");

                    b.ToTable("AuditTrails", (string)null);
                });

            modelBuilder.Entity("CAT20.Core.Models.AuditTrails.AuditTrailDetail", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Action")
                        .HasColumnType("int")
                        .HasColumnName("Action");

                    b.Property<int?>("AuditTrailID")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("AuditTrailID");

                    b.Property<int?>("EntityID")
                        .HasColumnType("int")
                        .HasColumnName("EntityID");

                    b.Property<int>("EntityType")
                        .HasColumnType("int")
                        .HasColumnName("EntityType");

                    b.Property<string>("NewValue")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("NewValue");

                    b.Property<string>("PreviousValue")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("PreviousValue");

                    b.Property<string>("Property")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Property");

                    b.Property<DateTime?>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("Timestamp")
                        .HasColumnName("TimeStamp");

                    b.Property<int>("UserID")
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("ID");

                    b.HasIndex("AuditTrailID");

                    b.ToTable("AuditTrailDetails", (string)null);
                });

            modelBuilder.Entity("CAT20.Core.Models.AuditTrails.AuditTrailDetail", b =>
                {
                    b.HasOne("CAT20.Core.Models.AuditTrails.AuditTrail", "AuditTrail")
                        .WithMany("DetailList")
                        .HasForeignKey("AuditTrailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuditTrail");
                });

            modelBuilder.Entity("CAT20.Core.Models.AuditTrails.AuditTrail", b =>
                {
                    b.Navigation("DetailList");
                });
#pragma warning restore 612, 618
        }
    }
}