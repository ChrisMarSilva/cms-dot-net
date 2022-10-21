using FanSoft.Sotre.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.Domain.ViewModels
{

    public class ProdutosAddEditVM
    {

        [Required(ErrorMessage = "Campo é Obrigatorio")]
        [StringLength(100, ErrorMessage = "Tamanho da Nome invalido")]
        public string Nome { get; set; }

        [StringLength(300, ErrorMessage = "Tamanho da Nome invalido")]
        public string Descricao { get; set; }

        [Display(Name = "Categoria")]
        public int? CategoriaId { get; set; }

        [Display(Name = "Preço")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Valor do Preço invalido")]
        public decimal? PrecoUnitario { get; set; }
    }

    public static class ProdutosModelExtensions
    {
        static public ProdutosAddEditVM ToVM(this Produto data) => new ProdutosAddEditVM { Nome = data.Nome, Descricao = data.Descricao, CategoriaId = data.CategoriaId, PrecoUnitario = data.PrecoUnitario };
        static public Produto ToData(this ProdutosAddEditVM data, int id = 0) => new Produto { Id = id, Nome = data.Nome, Descricao = data.Descricao, CategoriaId = (int)data.CategoriaId, PrecoUnitario = (decimal)data.PrecoUnitario };
    }

}
