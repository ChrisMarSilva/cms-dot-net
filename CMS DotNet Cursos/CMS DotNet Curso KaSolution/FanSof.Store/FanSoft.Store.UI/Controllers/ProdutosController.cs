using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FanSoft.Store.Domain.ViewModels;
using FanSoft.Store.Data.EF;
using FanSoft.Store.Data.EF.Repositories;
using FanSoft.Sotre.Domain.Contracts.Repositories;
using FanSoft.Sotre.Domain.Contracts.Data;
using FanSoft.Sotre.Domain.Entities;

namespace FanSoft.Store.UI.Controllers
{

    [Authorize]
    public class ProdutosController : Controller
    {

        private IProdutoRepository _produtoRepo;
        private ICategoriaRepository _categoriaRepo;
        private IUnitofWork _uow;

        public ProdutosController(IProdutoRepository produtoRepo, ICategoriaRepository categoriaRepo, IUnitofWork uow)
        {
            _produtoRepo = produtoRepo;
            _categoriaRepo = categoriaRepo;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Gestão de Produtos";
            var data = await _produtoRepo.GetWithCategoriaAsync();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id)
        {
            ViewBag.Title = "Novo Produto";
            ProdutosAddEditVM model = null;

            await GetCategoriaSelect();

            if (id != null)
            {
                ViewBag.Title = "Editar Produto";
                var prod = await _produtoRepo.GetAsync(id);
                if (prod == null) return NotFound();
                model = prod.ToVM();
            }
            return View(model);
        }

        private async Task GetCategoriaSelect()
        {
            var categ = await _categoriaRepo.GetAsync();
            ViewBag.Categorias = categ.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome });
        }

        public async Task<IActionResult> AddEdit(int id, ProdutosAddEditVM model)
        {

            await this.GetCategoriaSelect();

            if (id == 0) ViewBag.Title = "Novo Produto";
            if (id > 0) ViewBag.Title = "Editar Produto";

            if (!ModelState.IsValid)
                return View(model);

            var data = model.ToData(id);

            if (id == 0) _produtoRepo.Add(data);
            if (id > 0) _produtoRepo.Update(data);
            await _uow.CommitAsync();

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Excluir(int id)
        {
            var data = await _produtoRepo.GetAsync(id);
            if (data == null) return BadRequest(); // return NotFound();
            _produtoRepo.Deletee(data);
            await _uow.CommitAsync();
            return Ok(); // RedirectToAction("Index");
        }

    }
}
