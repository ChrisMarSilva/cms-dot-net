using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FanSoft.Sotre.Domain.Entities
{
    [Table("TbCategoria")]
    //[Table(nameof(Categoria))]
    public class Categoria : Entity
    {
        [Column("Nm", TypeName = "varchar(100)"), Required]
        public string Nome { get; set; }

        public IEnumerable<Produto> Produtos { get; set; }
    }
}
