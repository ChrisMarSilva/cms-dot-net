using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Controllers
{
    public class ErrorsController : Controller
    {

        public IActionResult E404 () => View();

    }
}
