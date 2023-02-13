using Principal;
using System;

namespace ContaTeste
{
    internal class ContaTeste
    {
        static void Main(string[] args)
        {
            testeContaSacar();
            testeContaSacarSemSaldo();

            Console.WriteLine("Fim...");
            Console.ReadLine();
        }

        private static void testeContaSacar()
        {
            // Arrange == Preparar
            var conta = new Conta("0001", 100);
            var resultadoEsperado = true;

            // Act = Executar
            var resultadoObtido = conta.Sacar(50);

            // Assert = Verificar
            if (resultadoObtido == resultadoEsperado)
                Console.WriteLine("testeContaSacar: Ok");
            else
                Console.WriteLine($"testeContaSacar: Falhou - resultadoEsperado={resultadoEsperado} - resultadoObtido={resultadoObtido}");
        }

        private static void testeContaSacarSemSaldo()
        {
            // Arrange == Preparar
            var conta = new Conta("0002", 100);
            var resultadoEsperado = false;

            // Act = Executar
            var resultadoObtido = conta.Sacar(120);

            // Assert = Verificar
            if (resultadoObtido == resultadoEsperado)
                Console.WriteLine("testeContaSacarSemSaldo: Ok");
            else
                Console.WriteLine($"testeContaSacarSemSaldo: Falhou - resultadoEsperado={resultadoEsperado} - resultadoObtido={resultadoObtido}");
        }

    }
}
