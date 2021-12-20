﻿// <auto-generated />
using System;
using CarRentalAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRentalAPI.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarRentalAPI.Models.Car", b =>
                {
                    b.Property<string>("CarId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CarName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarId");

                    b.ToTable("Tb_M_Car");
                });

            modelBuilder.Entity("CarRentalAPI.Models.Customer", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NIK");

                    b.ToTable("Tb_M_Customer");
                });

            modelBuilder.Entity("CarRentalAPI.Models.Employee", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("NIK");

                    b.HasIndex("RoleId")
                        .IsUnique();

                    b.ToTable("Tb_M_Employee");
                });

            modelBuilder.Entity("CarRentalAPI.Models.Rental", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("CarId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Tb_T_Rental");
                });

            modelBuilder.Entity("CarRentalAPI.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Tb_M_Role");
                });

            modelBuilder.Entity("CarRentalAPI.Repository.Data.LogCustomer", b =>
                {
                    b.Property<int>("LogCustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LogCustomerId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Tb_M_LogCustomer");
                });

            modelBuilder.Entity("CarRentalAPI.Models.Employee", b =>
                {
                    b.HasOne("CarRentalAPI.Models.Role", "Role")
                        .WithOne("Employee")
                        .HasForeignKey("CarRentalAPI.Models.Employee", "RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CarRentalAPI.Models.Rental", b =>
                {
                    b.HasOne("CarRentalAPI.Models.Car", "Car")
                        .WithMany("Rental")
                        .HasForeignKey("CarId");

                    b.HasOne("CarRentalAPI.Models.Customer", "Customer")
                        .WithMany("Rental")
                        .HasForeignKey("CustomerId");

                    b.HasOne("CarRentalAPI.Models.Employee", "Employee")
                        .WithMany("Rental")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Car");

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("CarRentalAPI.Repository.Data.LogCustomer", b =>
                {
                    b.HasOne("CarRentalAPI.Models.Rental", "Rental")
                        .WithOne("LogCustomer")
                        .HasForeignKey("CarRentalAPI.Repository.Data.LogCustomer", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rental");
                });

            modelBuilder.Entity("CarRentalAPI.Models.Car", b =>
                {
                    b.Navigation("Rental");
                });

            modelBuilder.Entity("CarRentalAPI.Models.Customer", b =>
                {
                    b.Navigation("Rental");
                });

            modelBuilder.Entity("CarRentalAPI.Models.Employee", b =>
                {
                    b.Navigation("Rental");
                });

            modelBuilder.Entity("CarRentalAPI.Models.Rental", b =>
                {
                    b.Navigation("LogCustomer");
                });

            modelBuilder.Entity("CarRentalAPI.Models.Role", b =>
                {
                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}