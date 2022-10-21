using Dapper;
using JDSVRCabineWeb.Data;
using JDSVRCabineWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace JDSVRCabineWeb.Controllers
{
    public class UsuarioController : Controller
    {

        private DataContext _ctx;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger, DataContext ctx)
        {
            _ctx = ctx;
            _logger = logger;
        }

        // ActionResult default // ViewResult KaSolution
        public async Task<IActionResult> Index()
        {
            var con = _ctx.Database.GetDbConnection();
            await con.OpenAsync();
            var users = await con.QueryAsync<Usuario>(@"SELECT * FROM TBJDSVRUsuario ORDER BY ID DESC");
            return View(users); // GET: UsuarioController
        }

        // _ctx.Usuarios.ToList()
        // _ctx.Database.ExecuteSqlCommand("SELECT ID, NOME, SITUACAO FROM TBJDSVRUsuario.ToList()
        // _ctx.Usuarios.FromSqlRaw("SELECT * FROM TBJDSVRUsuario ORDER BY ID DESC").ToList()

        [HttpGet]
        public async Task<string> ListaTed(string Data, string HrIni, string HrFim)
        {
            var con = _ctx.Database.GetDbConnection();

            await con.OpenAsync();

            var users = await con.QueryAsync<Usuario>(@"SELECT * FROM TBJDSVRUsuario ORDER BY ID DESC");

            var data = new
            {
                data = users.Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.Situacao,
                })
            };

            return JsonConvert.SerializeObject(data);
        }

        //// Lendo todos os registros
        //var customers = context.Customers.ToList();
        // var products = context.Products.Include(x => x.Category).ToList()

        //// Lendo um registro
        //var customers = context.Customers.FirstOrDefault(x => x.Id == id);

        //// Criando um registro
        //context.Customers.Add(customer);
        //context.SaveChanges();

        //// Lendo todos os registros
        //context.Customers.Update(customer);
        //context.SaveChanges();

        //// Lendo todos os registros
        //context.Customers.Remove(customer);
        //context.SaveChanges();

        public ViewResult Details(int id) => View(); // GET: UsuarioController/Details/5
        public ViewResult Create() => View(); // GET: UsuarioController/Create
        public ViewResult Edit(int id) => View(); // GET: UsuarioController/Edit/5
        public ViewResult Delete(int id) => View();  // GET: UsuarioController/Delete/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));  // POST: UsuarioController/Create
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));  // POST: UsuarioController/Edit/5
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));  // POST: UsuarioController/Delete/5
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}




        //public ViewResult Adicionar() => View();

        //[HttpPost]
        //public IActionResult Salvar(Cliente model)
        //{
        //    if (model.Nome == null || model.Idade < 18)
        //        return BadRequest("Erro....");
        //    if (model.Id == 0)
        //        _ctx.Clientes.Add(model);
        //    if (model.Id > 0)
        //        _ctx.Update(model);
        //    _ctx.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //// [HttpPost]
        //public IActionResult Editar(int id)
        //{
        //    var cliente = _ctx.Clientes.Find(id);
        //    if (cliente == null)
        //        return NotFound();
        //    return View(cliente);
        //}

        //[HttpPost]
        //public IActionResult Excluir(int id)
        //{
        //    var cliente = _ctx.Clientes.Find(id);
        //    if (cliente == null)
        //        return NotFound()
        //    _ctx.Clientes.Remove(cliente);
        //    _ctx.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}
