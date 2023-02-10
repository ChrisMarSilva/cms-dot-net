using Moq;
using NUnit.Framework;
using Principal;
using System;

namespace ContaTeste.Mock
{
    [TestFixture]
    public class ContaTeste
    {

        [Test]
        public void testeSolicitarEmprestimo()
        {
            // Arrange == Preparar

            var mock = new Mock<IValidadorCredito>();
            // mock.Setup(x => x.Validar("0001", 5000)).Returns(true);
            // mock.Setup(x => x.Validar(It.IsAny<string>(), 5000)).Returns(true);
            // mock.Setup(x => x.Validar(It.IsAny<string>(), It.IsAny<decimal>())).Returns(true);
            mock.Setup(x => x.Validar(It.IsAny<string>(), It.Is<decimal>(d => d <= 5000))).Returns(true);

            var conta = new Conta("0001", 100, mock.Object);
            // var conta = new Conta("0001", 100);
            // conta.SetValidadorCredito(mock.Object);
            decimal resultadoEsperado = 600;

            // Act = Executar
            conta.SolicitarEmprestimo(500);

            // Assert = Verificar
            Assert.IsTrue(conta.GetSaldo() == resultadoEsperado);
        }

        [Test]
        public void testeSolicitarEmprestimoComException()
        {
            var mock = new Mock<IValidadorCredito>();
            mock.Setup(x => x.Validar(It.IsAny<string>(), It.IsAny<decimal>())).Throws<InvalidOperationException>();

            var conta = new Conta("0015", 100, mock.Object);
            conta.SolicitarEmprestimo(5000);

            decimal resultadoEsperado = 100;

            Assert.IsTrue(conta.GetSaldo() == resultadoEsperado);
        }

        [Test]
        public void testeSolicitarEmprestimoAcimaLimite()
        {
            var mock = new Mock<IValidadorCredito>();

            var conta = new Conta("0001", 100, mock.Object);
            conta.SolicitarEmprestimo(1200);

            mock.Verify(x => x.Validar(It.IsAny<string>(), It.IsAny<decimal>()), Times.Never()); // Times.Never() = verifica se esse metodo nao foi executado nenhuma vez
        }

    }
}

// run all tests
// ctrl + r + a
