using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Entities
{
    [Table("TBTarefa")]
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }
    }
}