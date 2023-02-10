using NUnit.Framework;
using Principal;
using System;

namespace ContaTeste.NUnit
{
    [TestFixture]
    [Ignore("Pendencias")]
    public class ContaTeste
    {
        Conta conta;

        [OneTimeSetUp]  
        public void OneTimeSetUp()
        {
            // vai executar apensa uma vez antes do primeiro teste
        }

        [SetUp]  
        public void SetUp()
        {
            // vai executar antes de cada chamada de teste unitario
            conta = new Conta("0001", 200);
        }

        [TearDown]
        public void TearDown()
        {
            // vai executar depois de cada chamada de teste unitario
            conta = null;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // vai executar apensa uma vez depois do ultimo teste
        }

        [Test]
        [Category("Teste Valores Ok")]
        [TestCase(120, true)]
        [TestCase(-120, false)]
        public void testeSacar(int valor, bool resultadoEsperado)
        {
            // Arrange == Preparar
            // var conta = new Conta("0009", 200);

            // Act = Executar
            var resultado = conta.Sacar(valor);

            // Assert = Verificar
            Assert.IsTrue(resultado == resultadoEsperado);
        }

        [Test]
        [Category("Teste Valores Invalidos")]
        public void testeSacarSemSaldo()
        {
            //var conta = new Conta("0010", 200);
            var resultado = conta.Sacar(250);
            Assert.IsFalse(resultado);
        }

        [Test]
        [Category("Teste Valores Invalidos")]
        [TestCase(-100)]
        [TestCase(-200)]
        [TestCase(-300)]
        [TestCase(-400)]
        [TestCase(-500)]
        [TestCase(-600)]
        [TestCase(-700)]
        // [Ignore("Pendencia de implementação da RN02")]
        public void testeSacarValorNegativo(int valor)
        {
            //var conta = new Conta("0011", 200);
            var resultado = conta.Sacar(valor);
            Assert.IsFalse(resultado);
        }
              
        [Test]
        [Category("Teste Valores Invalidos")]
        public void testeSacarValorZerado()
        {
            Assert.Throws<ArgumentOutOfRangeException>(delegate { conta.Sacar(0.00M); });
            Assert.Catch<Exception>(delegate { conta.Sacar(0.00M); });
        }

        [Test]
        [Category("Teste Valores Invalidos")]
        [Timeout(3000)] // 3mil miliseg // 3Seg
        public void testeSacarTimeout()
        {
            //var conta = new Conta("0011", 200);
            var resultado = conta.Sacar(99);
            Assert.IsTrue(resultado);
        }

        [Test]
        [Category("Teste Assert")]
        public void testeAssertWithString()
        {
            string resultado;

            resultado = string.Empty;
            Assert.IsEmpty(resultado);

            resultado = "x";
            Assert.IsNotEmpty(resultado);
        }

        [Test]
        [Category("Teste Assert")]
        public void testeAssertWithDecimal()
        {
            int a = 20, b = 10;
            Assert.Greater(a, b);
            Assert.GreaterOrEqual(a, b);
        }

        [Test]
        [Category("Teste Assert")]
        public void testeAssertWithConta()
        {
            var c1 = new Conta("0012", 200);
            var c2 = c1; 
            Assert.AreSame(c1, c2);
        }

        [Test]
        [Category("Teste Assert")]
        public void testeAssertWithContaAtributes()
        {
            var c1 = new Conta("0012", 200);
            var c2 = new Conta("0012", 200);

            // Teste 01: ok
            Assert.AreEqual(c1.Cpf, c2.Cpf);
            Assert.AreEqual(c1.Saldo, c2.Saldo);

            // Teste 02: ok
            //foreach (PropertyInfo property in c1.GetType().GetProperties())
            //{
            //    Assert.AreEqual(property.GetValue(c1), property.GetValue(c2));
            //}

            // Teste 03: ok
            // Assert.AreEqual(JsonConvert.SerializeObject(c1), JsonConvert.SerializeObject(c2));

            // Teste 04: NDA
            //var js = new JavaScriptSerializer();
            // Assert.AreEqual(js.Serialize(c1), js.Serialize(c2));
        }

        [Test]
        public void testeSolicitarEmprestimo()
        {
            // Arrange == Preparar
            // var conta = new Conta("0001", 100);
            conta.SetValidadorCredito(new ValidadorCreditoFake());

            // Act = Executar
            var resultado = conta.SolicitarEmprestimo(5000);

            // Assert = Verificar
            Assert.IsTrue(resultado);
        }

    }
}

// run all tests
// ctrl + r + a
