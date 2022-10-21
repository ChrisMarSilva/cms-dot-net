using System;

namespace WebApplication6.Controllers
{
    public class Teste2 : ITeste2
    {
        private static int numero = 0;

        public Teste2()
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
