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
    [Migration("20231207220605_AddTablesInitOnDataTablesDb08")]
    partial class AddTablesInitOnDataTablesDb08
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

            modelBuilder.Entity("CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models.MarketModel", b =>
                {
                    b.Property<string>("symbol")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("symbol");

                    b.Property<string>("base_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("base_name");

                    b.Property<decimal>("base_precision")
                        .HasColumnType("numeric(3,0)")
                        .HasColumnName("base_precision");

                    b.Property<string>("base_symbol")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("base_symbol");

                    b.Property<string>("base_type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("base_type");

                    b.Property<decimal>("price_increment")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("price_increment");

                    b.Property<decimal>("price_min")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("price_min");

                    b.Property<decimal>("quantity_increment")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("quantity_increment");

                    b.Property<decimal>("quantity_min")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("quantity_min");

                    b.Property<string>("quote_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("quote_name");

                    b.Property<decimal>("quote_precision")
                        .HasColumnType("numeric(3,0)")
                        .HasColumnName("quote_precision");

                    b.Property<string>("quote_symbol")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("quote_symbol");

                    b.Property<string>("quote_type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("quote_type");

                    b.HasKey("symbol")
                        .HasName("PkTnBFoxbit_Markets");

                    b.ToTable("TbTnBFoxbit_Markets", (string)null);
                });

            modelBuilder.Entity("CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models.MarketQuoteModel", b =>
                {
                    b.Property<string>("market_symbol")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("market_symbol");

                    b.Property<decimal>("base_amount")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("base_amount");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("price");

                    b.Property<decimal>("quote_amount")
                        .HasColumnType("numeric(3,0)")
                        .HasColumnName("quote_amount");

                    b.Property<string>("side")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("side");

                    b.HasKey("market_symbol")
                        .HasName("PkTnBFoxbit_MarketQuotes");

                    b.ToTable("TbTnBFoxbit_MarketQuotes", (string)null);
                });

            modelBuilder.Entity("CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models.MemberInfoModel", b =>
                {
                    b.Property<string>("sn")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("sn");

                    b.Property<DateTime>("created_at")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("created_at");

                    b.Property<bool>("disabled")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("disabled");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)")
                        .HasColumnName("email");

                    b.Property<decimal>("level")
                        .HasColumnType("numeric(3,0)")
                        .HasColumnName("level");

                    b.HasKey("sn")
                        .HasName("PkTnBFoxbit_MemberInfos");

                    b.ToTable("TbTnBFoxbit_MemberInfos", (string)null);
                });

            modelBuilder.Entity("CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models.SystemTimeModel", b =>
                {
                    b.Property<string>("type")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("type");

                    b.Property<DateTime>("iso")
                        .HasMaxLength(20)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("iso");

                    b.Property<long>("timestamp")
                        .HasPrecision(3)
                        .HasColumnType("bigint")
                        .HasColumnName("timestamp");

                    b.HasKey("type", "iso")
                        .HasName("PkTnBFoxbit_SystemTime");

                    b.ToTable("TbTnBFoxbit_SystemTime", (string)null);
                });

            modelBuilder.Entity("CMS_DotNet_Teste_Resquest_Http_FoxBit_Api.Models.TradeModel", b =>
                {
                    b.Property<decimal>("id")
                        .HasColumnType("numeric(15,0)")
                        .HasColumnName("id");

                    b.Property<DateTime>("created_at")
                        .HasPrecision(3)
                        .HasColumnType("datetime(3)")
                        .HasColumnName("created_at");

                    b.Property<decimal>("fee")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("fee");

                    b.Property<string>("fee_currency_symbol")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("fee_currency_symbol");

                    b.Property<string>("market_symbol")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("market_symbol");

                    b.Property<decimal>("order_id")
                        .HasColumnType("numeric(15,0)")
                        .HasColumnName("order_id");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("price");

                    b.Property<decimal>("quantity")
                        .HasColumnType("decimal(21,8)")
                        .HasColumnName("quantity");

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

                    b.HasKey("id")
                        .HasName("PkTnBFoxbit_Trades");

                    b.ToTable("TbTnBFoxbit_Trades", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
