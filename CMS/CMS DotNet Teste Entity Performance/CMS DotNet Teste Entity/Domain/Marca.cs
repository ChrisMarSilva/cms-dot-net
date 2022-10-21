using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("TBMarca")]
    public class Marca
    {

        [Key]
        [Column("MrcId")]
        public int Id { get; set; }

        [Required]
        [Column("MrcNome")]
        [MaxLength(200)]
        public string Nome { get; set; }

        //public virtual IEnumerable<Produto> Produtos { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }

        public Marca()
        {
            Produtos = new List<Produto>();
        }
    }
}
