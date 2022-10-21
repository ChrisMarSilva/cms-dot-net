using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Sotre.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
