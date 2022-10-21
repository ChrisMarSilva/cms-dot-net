
using FanSoft.Sotre.Domain.Contracts.Data;
using FanSoft.Sotre.Domain.Contracts.Repositories;
using FanSoft.Store.Data.EF;
using FanSoft.Store.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.UI.Controllers
{

    [Authorize]
    public class UsuarioController : Controller
    {

        private IUsuarioRepository _usuarioRepo;
        private IUnitofWork _uow;

        public UsuarioController(IUsuarioRepository usuarioRepo, IUnitofWork uow)
        {
            _usuarioRepo = usuarioRepo;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Gestão de Usuários";
            var data = await _usuarioRepo.GetAsync();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id)
        {
            ViewBag.Title = "Novo Usuário";
            UsuariosAddEditVM model = null;
            if (id != null)
            {
                ViewBag.Title = "Editar Usuário";
                var data = await _usuarioRepo.GetAsync(id);
                if (data == null) return NotFound();
                model = UsuariosModelExtensions.ToVM(data);
            }
            return View(model);
        }

        public async Task<IActionResult> AddEdit(int id, UsuariosAddEditVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var data = UsuariosModelExtensions.ToData(model, id);
            if (id == 0) _usuarioRepo.Add(data);
            if (id > 0) _usuarioRepo.Update(data);
            await _uow.CommitAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var data = await _usuarioRepo.GetAsync(id);
            if (data == null) return NotFound();
            _usuarioRepo.Deletee(data);
            await _uow.CommitAsync();
            return RedirectToAction("Index");
        }

    }
}
