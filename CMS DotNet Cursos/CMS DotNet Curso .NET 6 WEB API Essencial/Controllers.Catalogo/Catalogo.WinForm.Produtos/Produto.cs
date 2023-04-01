using System;

namespace Consumindo_WebApi_Produtos
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string ImagemUrl { get; set; } = string.Empty;
        public Guid CategoriaId { get; set; }
    }
}
