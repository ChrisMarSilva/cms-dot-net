using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldEmpty.Controllers
{
    //Exemplo 01
    //[Controller]
    // public class Teste

    //Exemplo 02
    // public class TesteController

    //Exemplo 03
    //public class Teste : Controller

    public class TesteController : Controller
    {

        public string Ping() {
            return "Pong";
        }

    }
}
