using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Entities
{
    [Table("TbAula")]
    public class Aula
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O titulo da aula deve ser preenchido.")]
        [MaxLength(50, ErrorMessage = "O titulo da aula deve ter ate 50 caracteres.")]
        [MinLength(10, ErrorMessage = "O titulo da aula deve ter no minimo 10 caracteres.")]
        public string Titulo { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "A ordem da aula deve ser maior que zero.")]
        public int Ordem { get; set; }

        [JsonIgnore]
        [ForeignKey("Curso")]
        public int IdCurso { get; set; }

        [JsonIgnore]
        public virtual Curso Curso { get; set; }
    }
}