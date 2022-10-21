using CadCli.Models;
using CadCli.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CadCli.Controllers
{
    public class ClientesController : Controller
    {
        private CadCliDataContext _ctx;

        public ClientesController(CadCliDataContext ctx)
        {
            _ctx = ctx;
        }

        public ViewResult Index()
        {

            //var clientes = new List<Cliente>()
            //{
            //    new Cliente(){ Id = 1, Nome = "Pessoa01", Idade = 11, DataCadastro = DateTime.Now },
            //    new Cliente(){ Id = 2, Nome = "Pessoa02", Idade = 12, DataCadastro = DateTime.Now },
            //    new Cliente(){ Id = 3, Nome = "Pessoa03", Idade = 13, DataCadastro = DateTime.Now },
            //    new Cliente(){ Id = 4, Nome = "Pessoa04", Idade = 14, DataCadastro = DateTime.Now },
            //    new Cliente(){ Id = 5, Nome = "Pessoa05", Idade = 15, DataCadastro = DateTime.Now },
            //    new Cliente(){ Id = 6, Nome = "Pessoa06", Idade = 16, DataCadastro = DateTime.Now },
            //    new Cliente(){ Id = 7, Nome = "Pessoa07", Idade = 17, DataCadastro = DateTime.Now },
            //    new Cliente(){ Id = 8, Nome = "Pessoa08", Idade = 18, DataCadastro = DateTime.Now },
            //    new Cliente(){ Id = 9, Nome = "Pessoa09", Idade = 19, DataCadastro = DateTime.Now }
            //};

            // var clientes = ctx.Clientes.ToList();
            // return View(clientes);
            return View(_ctx.Clientes.ToList());
            //return View(new CadCliDataContext().Clientes.ToList());

        }

        public ViewResult Adicionar() => View();

        [HttpPost]
        public IActionResult Salvar(Cliente model)
        {

            if (model.Nome == null || model.Idade < 18)
                return BadRequest("Erro....");

            if (model.Id == 0)
                _ctx.Clientes.Add(model);

            if (model.Id  > 0)
                _ctx.Update(model);

            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }

       // [HttpPost]
        public IActionResult Editar(int id)
        {
            var cliente = _ctx.Clientes.Find(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Excluir(int id)
        {
            var cliente = _ctx.Clientes.Find(id);

            if (cliente == null)
                return NotFound();

            _ctx.Clientes.Remove(cliente);
            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
