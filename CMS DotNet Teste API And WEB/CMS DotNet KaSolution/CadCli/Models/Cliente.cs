using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Models
{
    [Table("TbCliente")]
    public class Cliente
    {
        [Key]
        [Column("CodCliente")]
        public int Id { get; set; }

        [Required]
        [Column("NomeCliente", TypeName = "varchar(110)")]
        public  string      Nome            { get; set; }

        [Column("IdadeCliente")]
        public  int         Idade           { get; set; }

        [Column("DtCadCliente")]
        public  DateTime    DataCadastro    { get; set; } = DateTime.Now;

    }
}
