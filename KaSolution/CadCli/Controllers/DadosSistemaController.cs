using CadCli.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Controllers
{
    public class DadosSistemaController : Controller 
    {
        private IConfigSistemaService _dadosSisService;

        public DadosSistemaController(IConfigSistemaService dadosSisService)
        {
            this._dadosSisService = dadosSisService;

        }

        public IActionResult Index()
        {
            var dados = this._dadosSisService.Dados;
            return View(dados);
        }
    }
}
