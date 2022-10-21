using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View();

        public string Ping () => "Pong"; // Arrow Function

    }
}