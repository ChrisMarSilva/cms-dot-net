using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Entities
{
    [Table("TbCurso")]
    public class Curso
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O titulo do curso deve ser preenchido.")]
        [MaxLength(100, ErrorMessage = "O titulo do curso so pode conter ate 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A URL do curso deve ser preenchida.")]
        [Url(ErrorMessage = "A URL do curso deve conter um endereco valido.")]
        public string URL { get; set; }

        [Required(ErrorMessage = "O canal do curso deve ser preenchido")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Canal Canal { get; set; }

        [Required(ErrorMessage = "A data de publicacao do curso deve ser preenchida")]
        public DateTime DataPublicacao { get; set; }

        [Required(ErrorMessage = "A carga horaria do curso deve ser preenchida.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "A carga horaria deve ser de pelo menos 1h")]
        public int CargaHoraria { get; set; }

        [JsonIgnore]
        public virtual ICollection<Aula> Aulas { get; set; }

    }
}