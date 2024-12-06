﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entity.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CurrentProvince")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("Domain.Entity.EmailVerification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("VerificationCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("EmailVerifications");
                });

            modelBuilder.Entity("Domain.Entity.FishHealth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Behavior")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CheckDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HealthStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("OrderFishId")
                        .HasColumnType("integer");

                    b.Property<string>("Temperature")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WaterQuality")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderFishId");

                    b.ToTable("FishHealths");
                });

            modelBuilder.Entity("Domain.Entity.FishQualification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrderFishId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderFishId");

                    b.ToTable("FishQualifications");
                });

            modelBuilder.Entity("Domain.Entity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double?>("Distance")
                        .HasColumnType("double precision");

                    b.Property<string>("FeedbackContent")
                        .HasColumnType("text");

                    b.Property<int?>("FeedbackStars")
                        .HasColumnType("integer");

                    b.Property<string>("FromAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("integer");

                    b.Property<string>("ReasonToCancel")
                        .HasColumnType("text");

                    b.Property<string>("ReceiverName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ReceiverPhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ToAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<int?>("TransportServiceId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserAccountId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TransportServiceId");

                    b.HasIndex("UserAccountId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entity.OrderFish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FishImgURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Length")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserAccountId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserAccountId");

                    b.ToTable("OrderFishs");
                });

            modelBuilder.Entity("Domain.Entity.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusPayment")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Domain.Entity.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DeliveryStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DriverId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RouteStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Domain.Entity.RouteStop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("RouteId")
                        .HasColumnType("integer");

                    b.Property<int>("RouteStatus")
                        .HasColumnType("integer");

                    b.Property<int>("StopOrder")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("RouteId");

                    b.ToTable("RouteStops");
                });

            modelBuilder.Entity("Domain.Entity.TransportService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FromProvince")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PricePerAmount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PricePerKg")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("PricePerKm")
                        .HasColumnType("numeric");

                    b.Property<string>("ToProvince")
                        .HasColumnType("text");

                    b.Property<decimal?>("TransportPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("TransportType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TransportServices");
                });

            modelBuilder.Entity("Domain.Entity.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DriverId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsEmailVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DriverId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entity.EmailVerification", b =>
                {
                    b.HasOne("Domain.Entity.UserAccount", "User")
                        .WithMany("EmailVerifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.FishHealth", b =>
                {
                    b.HasOne("Domain.Entity.OrderFish", "OrderFish")
                        .WithMany("FishHealths")
                        .HasForeignKey("OrderFishId");

                    b.Navigation("OrderFish");
                });

            modelBuilder.Entity("Domain.Entity.FishQualification", b =>
                {
                    b.HasOne("Domain.Entity.OrderFish", "OrderFish")
                        .WithMany("FishQualifications")
                        .HasForeignKey("OrderFishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderFish");
                });

            modelBuilder.Entity("Domain.Entity.Order", b =>
                {
                    b.HasOne("Domain.Entity.TransportService", "TransportService")
                        .WithMany("Orders")
                        .HasForeignKey("TransportServiceId");

                    b.HasOne("Domain.Entity.UserAccount", "UserAccount")
                        .WithMany("Orders")
                        .HasForeignKey("UserAccountId");

                    b.Navigation("TransportService");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("Domain.Entity.OrderFish", b =>
                {
                    b.HasOne("Domain.Entity.Order", "Order")
                        .WithMany("OrderFishs")
                        .HasForeignKey("OrderId");

                    b.HasOne("Domain.Entity.UserAccount", null)
                        .WithMany("OrderFishes")
                        .HasForeignKey("UserAccountId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Entity.Payment", b =>
                {
                    b.HasOne("Domain.Entity.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Entity.Route", b =>
                {
                    b.HasOne("Domain.Entity.Driver", "Driver")
                        .WithMany("Routes")
                        .HasForeignKey("DriverId");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("Domain.Entity.RouteStop", b =>
                {
                    b.HasOne("Domain.Entity.Order", "Order")
                        .WithMany("RouteStops")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Route", "Route")
                        .WithMany("RouteStops")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("Domain.Entity.UserAccount", b =>
                {
                    b.HasOne("Domain.Entity.Driver", "Driver")
                        .WithOne("UserAccount")
                        .HasForeignKey("Domain.Entity.UserAccount", "DriverId");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("Domain.Entity.Driver", b =>
                {
                    b.Navigation("Routes");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("Domain.Entity.Order", b =>
                {
                    b.Navigation("OrderFishs");

                    b.Navigation("Payments");

                    b.Navigation("RouteStops");
                });

            modelBuilder.Entity("Domain.Entity.OrderFish", b =>
                {
                    b.Navigation("FishHealths");

                    b.Navigation("FishQualifications");
                });

            modelBuilder.Entity("Domain.Entity.Route", b =>
                {
                    b.Navigation("RouteStops");
                });

            modelBuilder.Entity("Domain.Entity.TransportService", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Entity.UserAccount", b =>
                {
                    b.Navigation("EmailVerifications");

                    b.Navigation("OrderFishes");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
