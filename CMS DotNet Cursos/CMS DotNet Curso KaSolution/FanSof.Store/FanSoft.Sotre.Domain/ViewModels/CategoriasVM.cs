using System.ComponentModel.DataAnnotations;
using FanSoft.Sotre.Domain.Entities;

namespace FanSoft.Store.Domain.ViewModels
{
    public class CategoriasAddEditVM
    {
        [Required(ErrorMessage = "campo obrigatório")]
        [StringLength(100, ErrorMessage = "limite de {1} excedido")]
        public string Nome { get; set; }
    }
       
    public static class CategoriasModelExtensions
    {
        public static CategoriasAddEditVM ToVM(this Categoria data) => new CategoriasAddEditVM { Nome = data.Nome, };
        public static Categoria ToData(this CategoriasAddEditVM model, int id = 0) => new Categoria { Id = id, Nome = model.Nome };
    }
}
