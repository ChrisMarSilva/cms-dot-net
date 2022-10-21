using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Sotre.Domain.Entities
{
    [Table("TbProduto")]
    public class Produto : Entity
    {

        // [Required]
        // [StringLength(100)]
        [Column("Nm", TypeName = "varchar(100)"), Required]
        public string Nome { get; set; }

        [Required, Column("Descr"), StringLength(150)]
        public string Descricao { get; set; }

        [Column("IdCateg")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [Column("Money")]
        public decimal PrecoUnitario { get; set; }

    }
}
