using Principal;
using System;

namespace CalculadoraTeste
{
    internal class CalculadoraTeste
    {
        static void Main(string[] args)
        {
            testeSomar();
            testeSomarNumerosNegativos();

            Console.WriteLine("Fim...");
            Console.ReadLine();
        }

        private static void testeSomar()
        {
            // Arrange == Preparar
            var calculadora = new Calculadora();
            var resultadoEsperado = 350;

            // Act = Executar
            var resultadoObtido = calculadora.Somar(100, 250);

            if (resultadoObtido == resultadoEsperado)
                Console.WriteLine("testeSomar: Ok");
            else
                Console.WriteLine($"testeSomar: Falhou - resultadoEsperado={resultadoEsperado} - resultadoObtido={resultadoObtido}");
        }

        private static void testeSomarNumerosNegativos()
        {
            // Arrange == Preparar
            var calculadora = new Calculadora();
            var resultadoEsperado = -100;

            // Act = Executar
            var resultadoObtido = calculadora.Somar(-20, -80);

            // Assert = Verificar
            if (resultadoObtido == resultadoEsperado)
                Console.WriteLine("testeSomarNumerosNegativos: Ok");
            else
                Console.WriteLine($"testeSomarNumerosNegativos: Falhou - resultadoEsperado={resultadoEsperado} - resultadoObtido={resultadoObtido}");
        }

    }
}
