using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApplication1.Models.Entities
{
    public class Empresa
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        
        public string Descr { get; set; }

        [JsonIgnore]
        public virtual ICollection<Vaga> Vagas { get; set; }

    }
}