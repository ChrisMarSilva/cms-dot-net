using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.Api.Controllers
{
    public class TesteController : ControllerBase
    {
        [HttpGet("Ping")]
        public IActionResult Ping() => Ok(new { msg = "Pong" });
    }
}
