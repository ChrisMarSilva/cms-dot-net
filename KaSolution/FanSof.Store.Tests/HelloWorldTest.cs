using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSof.Store.Tests
{

    [TestClass]
    [TestCategory("Controller - Hello World")]
    public class HelloWorldTest
    {

        [TestMethod]
        public void ExecutandoUmTesteDeOlharMundo()
        {

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

            //Regra do 3 as -> 01 -> arrage -> Ambiente ( onde vc instancia os objetos)
            var helloWorld = "HelloWorld";

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

            //Regra do 3 as -> 02 -> action -> ação ( é a execução do metodo que vc vai testar)
            var troca = new Random().Next(0, 2) == 0;
            if (troca) helloWorld = "OutraCoisa";

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

            //Regra do 3 as -> 03 -> assert -> validação do resultado
            Assert.AreEqual("HelloWorld", helloWorld);

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

        }

    }

}
