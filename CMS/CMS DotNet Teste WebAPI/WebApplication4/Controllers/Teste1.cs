using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Controllers
{
    public class Teste1
    {
        private int numero = 0;

        public Teste1()
        {
            numero++;
        }

        public int GetNumero() 
        {
            var rng = new Random();
            return numero;
        }
    }
}
