using FanSoft.Store.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSof.Store.Tests.Controllers
{
    [TestClass]
    [TestCategory("Controller - Home")]
    public class HomeCtrlTest
    {

        [TestMethod]
        public void OMetodoIndexDeveraRetonarUmaView()
        {

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

            //Regra do 3 as -> 01 -> arrage -> Ambiente ( onde vc instancia os objetos)
            //LogInformationvar homeCtrl = new HomeController();

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

            //Regra do 3 as -> 02 -> action -> ação ( é a execução do metodo que vc vai testar)
            //var result = (ViewResult)homeCtrl.Index();
            //var result = homeCtrl.Index() as ViewResult;

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

            //Regra do 3 as -> 03 -> assert -> validação do resultado
            //Assert.IsNotNull(result);
            //Assert.IsNull(result);

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

        }

    }
}
