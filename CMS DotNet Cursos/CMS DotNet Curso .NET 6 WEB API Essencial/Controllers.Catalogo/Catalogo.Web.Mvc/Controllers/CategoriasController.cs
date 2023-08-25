using Catalogo.Web.Mvc.Models;
using Catalogo.Web.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Web.Mvc.Controllers;

public class CategoriasController : Controller
{
    private readonly ILogger<CategoriasController> _logger;
    private readonly ICategoriaService _categService;

    public CategoriasController(ILogger<CategoriasController> logger, ICategoriaService categService)
    {
        _logger = logger;
        _categService = categService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> Index()
    {
        var result = await _categService.GetAllAsync();

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public IActionResult CriarNovaCategoria()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>> CriarNovaCategoria(CategoriaViewModel categoriaVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _categService.CreateAsync(categoriaVM);

            if (result != null)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.Erro = "Erro ao criar Categoria";

        return View(categoriaVM);
    }

    [HttpGet]
    public async Task<IActionResult> AtualizarCategoria(int id)
    {
        var result = await _categService.GetByIdAsync(id);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>> AtualizarCategoria(int id, CategoriaViewModel categoriaVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _categService.UpdateAsync(id, categoriaVM);

            if (result)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.Erro = "Erro ao atualizar Categoria";

        return View(categoriaVM);
    }

    [HttpGet]
    public async Task<ActionResult> DeletarCategoria(int id)
    {
        var result = await _categService.GetByIdAsync(id);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost(), ActionName("DeletarCategoria")]
    public async Task<IActionResult> DeletaConfirmado(int id)
    {
        var result = await _categService.DeleteAsync(id);

        if (result)
            return RedirectToAction("Index");

        return View("Error");
    }
}

