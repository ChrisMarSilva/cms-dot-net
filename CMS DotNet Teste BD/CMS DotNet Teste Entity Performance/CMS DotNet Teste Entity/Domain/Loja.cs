using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("TBLoja")]
    public class Loja
    {

        [Key]
        [Column("LjId")]
        public int Id { get; set; }

        [Required]
        [Column("LjNome")]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Column("LjDescricao")]
        [MaxLength(2000)]
        public string Descricao { get; set; }

        //public virtual IEnumerable<Produto> Produtos { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
        
        public Loja()
        {
            Produtos = new List<Produto>();
        }

    }
}
