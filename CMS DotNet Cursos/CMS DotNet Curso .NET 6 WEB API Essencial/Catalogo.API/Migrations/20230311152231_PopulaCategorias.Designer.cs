﻿// <auto-generated />
using Catalogo.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.API.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20230311152231_PopulaCategorias")]
partial class PopulaCategorias
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "7.0.3")
            .HasAnnotation("Relational:MaxIdentifierLength", 64);

        modelBuilder.Entity("Catalogo.Domain.Models.Categoria", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("char(36)")
                    .HasColumnName("id");

                b.Property<DateTime?>("DataAlteracao")
                    .HasColumnType("datetime(6)")
                    .HasColumnName("data_alteracao");

                b.Property<DateTime>("DataCadastro")
                    .HasColumnType("datetime(6)")
                    .HasColumnName("data_cadastro");

                b.Property<string>("ImagemUrl")
                    .HasColumnType("varchar(300)")
                    .HasColumnName("imagem_url");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasColumnType("varchar(80)")
                    .HasColumnName("nome");

                b.HasKey("Id");

                b.ToTable("Categoria", (string)null);
            });

        modelBuilder.Entity("Catalogo.Domain.Models.Produto", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("char(36)")
                    .HasColumnName("id");

                b.Property<Guid>("CategoriaId")
                    .HasColumnType("char(36)")
                    .HasColumnName("categoria_id");

                b.Property<DateTime?>("DataAlteracao")
                    .HasColumnType("datetime(6)")
                    .HasColumnName("data_alteracao");

                b.Property<DateTime>("DataCadastro")
                    .HasColumnType("datetime(6)")
                    .HasColumnName("data_cadastro");

                b.Property<string>("Descricao")
                    .HasColumnType("varchar(300)")
                    .HasColumnName("descricao");

                b.Property<float>("Estoque")
                    .HasColumnType("float")
                    .HasColumnName("estoque");

                b.Property<string>("ImagemUrl")
                    .HasColumnType("varchar(300)")
                    .HasColumnName("imagem_url");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasColumnType("varchar(80)")
                    .HasColumnName("nome");

                b.Property<decimal>("Preco")
                    .HasColumnType("decimal(10,2)")
                    .HasColumnName("preco");

                b.HasKey("Id");

                b.HasIndex("CategoriaId");

                b.ToTable("Produto", (string)null);
            });

        modelBuilder.Entity("Catalogo.Domain.Models.Produto", b =>
            {
                b.HasOne("Catalogo.Domain.Models.Categoria", "Categoria")
                    .WithMany("Produtos")
                    .HasForeignKey("CategoriaId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Categoria");
            });

        modelBuilder.Entity("Catalogo.Domain.Models.Categoria", b =>
            {
                b.Navigation("Produtos");
            });
#pragma warning restore 612, 618
    }
}