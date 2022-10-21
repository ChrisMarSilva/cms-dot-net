using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("TBProduto")]
    public class Produto
    {

        [Key]
        [Column("ProdId")]
        public int Id { get; set; }

        [Required]
        [Column("ProdNome")]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Column("ProdDescricao")]
        [MaxLength(2000)]
        public string Descricao { get; set; }

        [Required]
        [Column("ProdValor")]
        [Range(-999999999999.99, 999999999999.99)]
        public decimal Valor { get; set; }

        [Column("ProdIdLoja")]
        public int LojaId { get; set; }

        [ForeignKey("LojaId")]
        public virtual Loja Loja { get; set; }

        [Column("ProdIdMarca")]
        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")]
        public virtual Marca Marca { get; set; }

    }
}
