using System;

namespace WebApplication6.Controllers
{
    public class Teste1 : ITeste1
    {
        private static int numero = 0;

        public Teste1()
        {
        }

        public string GetNumero()
        {
            if (numero == 0)
                numero = new Random().Next();
            return numero.ToString();
        }
    }
}
