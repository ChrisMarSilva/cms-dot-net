using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Sotre.Domain.Entities
{
    [Table("TbUsuario")]
    public class Usuario : Entity
    {
        [Column("Nm", TypeName = "varchar(100)"), Required]
        public string Nome  { get; set; }

        [Column("Mail", TypeName = "varchar(100)"), Required]
        public string Email { get; set; }

        [Column("Pass", TypeName = "char(88)"), Required]
        public string Senha { get; set; }
    }
}
