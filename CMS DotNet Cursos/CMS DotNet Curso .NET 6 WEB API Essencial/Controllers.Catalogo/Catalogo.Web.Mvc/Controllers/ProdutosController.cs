using Catalogo.Web.Mvc.Models;
using Catalogo.Web.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Catalogo.Web.Mvc.Controllers;

public class ProdutosController : Controller
{
    private readonly ILogger<ProdutosController> _logger;
    private readonly IProdutoService _prodService;
    private readonly ICategoriaService _categService;
    private string _token = string.Empty;

    public ProdutosController(ILogger<ProdutosController> logger, IProdutoService prodService, ICategoriaService categService)
    {
        _logger = logger;
        _prodService = prodService;
        _categService = categService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
    {
        var token = ObtemTokenJwt();
        var result = await _prodService.GetAllAsync(token: token);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CriarNovoProduto()
    {
        var items = await _categService.GetAllAsync();
        ViewBag.CategoriaId = new SelectList(items: items, dataValueField: "CategoriaId", dataTextField: "Nome");

        return View();
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoViewModel>> CriarNovoProduto(ProdutoViewModel model)
    {
        if (ModelState.IsValid)
        {
            var token = ObtemTokenJwt();
            var result = await _prodService.CreateAsync(model: model, token: token);

            if (result != null)
                return RedirectToAction(nameof(Index));
        }
        else
        {
            var items = await _categService.GetAllAsync();
            ViewBag.CategoriaId = new SelectList(items: items, dataValueField: "CategoriaId", dataTextField: "Nome");
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> DetalhesProduto(int id)
    {
        var token = ObtemTokenJwt();
        var produto = await _prodService.GetByIdAsync(id: id, token: token);

        if (produto is null)
            return View("Error");

        return View(produto);
    }

    [HttpGet]
    public async Task<IActionResult> AtualizarProduto(int id)
    {
        var token = ObtemTokenJwt();
        var result = await _prodService.GetByIdAsync(id: id, token: token);

        if (result is null)
            return View("Error");

        var items = await _categService.GetAllAsync();
        ViewBag.CategoriaId = new SelectList(items: items, dataValueField: "CategoriaId", dataTextField: "Nome");

        return View(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoViewModel>> AtualizarProduto(int id, ProdutoViewModel model)
    {
        if (ModelState.IsValid)
        {
            var token = ObtemTokenJwt();
            var result = await _prodService.UpdateAsync(id: id, model: model, token: token);

            if (result)
                return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    [HttpGet]
    public async Task<ActionResult> DeletarProduto(int id)
    {
        var token = ObtemTokenJwt();
        var result = await _prodService.GetByIdAsync(id: id, token: token);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost(), ActionName("DeletarProduto")]
    public async Task<IActionResult> DeletaConfirmado(int id)
    {
        var token = ObtemTokenJwt();
        var result = await _prodService.DeleteAsync(id: id, token: token);

        if (result)
            return RedirectToAction("Index");

        return View("Error");
    }

    private string ObtemTokenJwt()
    {
        if (HttpContext.Request.Cookies.ContainsKey(key: "X-Access-Token"))
            _token = HttpContext.Request.Cookies["X-Access-Token"]!.ToString();

        return _token;
    }
}
