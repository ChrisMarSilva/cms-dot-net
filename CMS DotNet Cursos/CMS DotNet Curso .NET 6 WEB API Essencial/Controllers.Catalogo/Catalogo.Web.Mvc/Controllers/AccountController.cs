using Catalogo.Web.Mvc.Models;
using Catalogo.Web.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Web.Mvc.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAutenticacaoService _authService;

    public AccountController(ILogger<AccountController> logger, IAutenticacaoService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(UsuarioViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Login Inválido....");
            return View(model);
        }

        var result = await _authService.AutenticaUsuarioAsync(model);

        if (result is null || string.IsNullOrEmpty(result.Token))
        {
            if (result is null)
                ModelState.AddModelError(string.Empty, "Login Inválido....");
            if (string.IsNullOrEmpty(result?.Token))
                ModelState.AddModelError(string.Empty, "Token Inválido....");
            return View(model);
        }

        Response.Cookies.Append(
            key: "X-Access-Token",
            value: result.Token,
            options: new CookieOptions()
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

        return Redirect("/");
    }
}
