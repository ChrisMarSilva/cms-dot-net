using FanSoft.Sotre.Domain.Entities;
using FanSoft.Sotre.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.Data.EF
{
    public static class DbInitializer
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario() { Id = 1, Nome = "Pessoa01", Email = "Pessoa01@gmail.com", Senha = "123".Encrypt() },
                new Usuario() { Id = 2, Nome = "Pessoa02", Email = "Pessoa02@gmail.com", Senha = "123".Encrypt() },
                new Usuario() { Id = 3, Nome = "Pessoa03", Email = "Pessoa03@gmail.com", Senha = "123".Encrypt() }
            );

            modelBuilder.Entity<Categoria>().HasData(
                    new Categoria() { Id = 1, Nome = "Categoria01" },
                    new Categoria() { Id = 2, Nome = "Categoria02" },
                    new Categoria() { Id = 3, Nome = "Categoria03" }
                );

            modelBuilder.Entity<Produto>().HasData(
                new Produto() { Id = 1, Nome = "Produto01", Descricao = "DescricaoProduto01", CategoriaId = 1, PrecoUnitario = 10.11M },
                new Produto() { Id = 2, Nome = "Produto02", Descricao = "DescricaoProduto02", CategoriaId = 2, PrecoUnitario = 20.22M },
                new Produto() { Id = 3, Nome = "Produto03", Descricao = "DescricaoProduto03", CategoriaId = 2, PrecoUnitario = 30.33M },
                new Produto() { Id = 4, Nome = "Produto04", Descricao = "DescricaoProduto04", CategoriaId = 3, PrecoUnitario = 40.44M },
                new Produto() { Id = 5, Nome = "Produto05", Descricao = "DescricaoProduto04", CategoriaId = 3, PrecoUnitario = 50.55M },
                new Produto() { Id = 6, Nome = "Produto06", Descricao = "DescricaoProduto04", CategoriaId = 3, PrecoUnitario = 60.66M }
            );

        }

    }
}
