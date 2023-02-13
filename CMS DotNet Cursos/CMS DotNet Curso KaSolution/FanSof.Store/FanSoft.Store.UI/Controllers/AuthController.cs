using FanSoft.Sotre.Domain.Contracts.Repositories;
using FanSoft.Sotre.Domain.Helpers;
using FanSoft.Store.Data.EF;
using FanSoft.Store.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.UI.Controllers
{
    public class AuthController : Controller
    {

        private IUsuarioRepository _usuarioRepo;

        public AuthController(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        [HttpGet]
        public IActionResult SignIn() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string returnUrl, SignInVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var usuario = await _usuarioRepo.AuthenticateAsync(model.Email, model.Senha);

            if (usuario == null)
            {
                ModelState.AddModelError("", "E-mail e/ou senha invalios.");
                //return Unauthorized();
                return View(model);
            }

            var claims = new List<Claim>()
            {
                new Claim("id",    usuario.Id.ToString() ),
                new Claim("nome",  usuario.Nome ),
                new Claim("email", usuario.Email ),
                new Claim("roles", "admin,ti,estagiario" )
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "nome", "roles");

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = model.Lembrar,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(100)
                });

            return Redirect(returnUrl ?? "/");
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }

    }
}
