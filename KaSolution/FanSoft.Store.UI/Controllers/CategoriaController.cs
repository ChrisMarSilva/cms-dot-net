using FanSoft.Sotre.Domain.Contracts.Data;
using FanSoft.Sotre.Domain.Contracts.Repositories;
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
    public class CategoriaController : Controller
    {

        private ICategoriaRepository _categoriaRepo;
        private IUnitofWork _uow;

        public CategoriaController(ICategoriaRepository categoriaRepo, IUnitofWork uow)
        {
            _categoriaRepo = categoriaRepo;
            _uow = uow;
        }

        public async Task<IActionResult> Index() => View(await _categoriaRepo.GetAsync());

    }
}
