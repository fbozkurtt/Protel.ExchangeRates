﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Protel.ExchangeRates.Data;

namespace Protel.ExchangeRates.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210922131319_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Protel.ExchangeRates.Core.Domain.ExchangeRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("BanknoteBuying")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BanknoteSelling")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("CrossOrder")
                        .HasColumnType("int");

                    b.Property<decimal>("CrossRateOther")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CrossRateUSD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CurrencyCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrencyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ForexBuying")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ForexSelling")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Kod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("Unit")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("ExchangeRate");
                });
#pragma warning restore 612, 618
        }
    }
}
