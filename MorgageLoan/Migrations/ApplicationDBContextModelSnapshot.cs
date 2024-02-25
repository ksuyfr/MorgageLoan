﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MorgageLoan.Data;

#nullable disable

namespace MorgageLoan.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MorgageLoan.Models.Credit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreditName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreditTerm")
                        .HasColumnType("int");

                    b.Property<decimal>("FirstFloor")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<double?>("FirstPercent")
                        .HasColumnType("float");

                    b.Property<decimal>("FullCoast")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<double>("InterestRate")
                        .HasColumnType("float");

                    b.Property<decimal?>("MonthlyPayment")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.ToTable("Credit");
                });
#pragma warning restore 612, 618
        }
    }
}
