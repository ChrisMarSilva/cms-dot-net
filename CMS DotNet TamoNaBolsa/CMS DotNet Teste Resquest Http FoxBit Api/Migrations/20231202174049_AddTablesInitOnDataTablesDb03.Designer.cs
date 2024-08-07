﻿// <auto-generated />
using System;
using CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231202174049_AddTablesInitOnDataTablesDb03")]
    partial class AddTablesInitOnDataTablesDb03
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models.CurrencyModel", b =>
                {
                    b.Property<string>("symbol")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("symbol");

                    b.Property<string>("category_code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("category_code");

                    b.Property<string>("category_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("category_name");

                    b.Property<decimal>("deposit_min_amount")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("deposit_min_amount");

                    b.Property<decimal>("deposit_min_to_confirm")
                        .HasColumnType("numeric(3,0)")
                        .HasColumnName("deposit_min_to_confirm");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<decimal?>("precision")
                        .HasColumnType("numeric(3,0)")
                        .HasColumnName("precision");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("type");

                    b.Property<decimal?>("withdraw_enabled")
                        .HasColumnType("numeric(1,0)")
                        .HasColumnName("withdraw_enabled");

                    b.Property<decimal>("withdraw_fee")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("withdraw_fee");

                    b.Property<decimal>("withdraw_min_amount")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("withdraw_min_amount");

                    b.HasKey("symbol")
                        .HasName("PkTnBFoxbit_Currencies");

                    b.ToTable("TbTnBFoxbit_Currencies", (string)null);
                });

            modelBuilder.Entity("CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models.TradeModel", b =>
                {
                    b.Property<long>("id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<decimal>("client_order_id")
                        .HasColumnType("numeric(15,0)")
                        .HasColumnName("client_order_id");

                    b.Property<DateTime>("created_at")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("created_at");

                    b.Property<decimal>("funds_received")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("funds_received");

                    b.Property<decimal>("instant_amount")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("instant_amount");

                    b.Property<decimal>("instant_amount_executed")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("instant_amount_executed");

                    b.Property<string>("market_symbol")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("market_symbol");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("price");

                    b.Property<decimal>("price_avg")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("price_avg");

                    b.Property<decimal>("quantity")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("quantity");

                    b.Property<decimal>("quantity_executed")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("quantity_executed");

                    b.Property<string>("remark")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)")
                        .HasColumnName("remark");

                    b.Property<string>("side")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("side");

                    b.Property<string>("sn")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("sn");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("state");

                    b.Property<decimal>("trades_count")
                        .HasColumnType("numeric(9,0)")
                        .HasColumnName("trades_count");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("type");

                    b.HasKey("id")
                        .HasName("PkTnBFoxbit_Trades");

                    b.ToTable("TbTnBFoxbit_Trades", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
