using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entities
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Total { get; set; }
        public TipoVenda Tipo { get; set; }
        public List<ItemVenda> Itens { get; set; }

    }
}