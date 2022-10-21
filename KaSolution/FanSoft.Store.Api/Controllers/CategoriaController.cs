using FanSoft.Sotre.Domain.Contracts.Data;
using FanSoft.Sotre.Domain.Contracts.Repositories;
using FanSoft.Store.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class CategoriaController : ControllerBase
    {

        private ICategoriaRepository _catRepo;
        private IUnitofWork _uow;

        public CategoriaController(ICategoriaRepository catRepo, IUnitofWork uow)
        {
            _catRepo = catRepo;
            _uow = uow;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll() => Ok(await _catRepo.GetAsync());

        [HttpGet("{id}", Name = "GetCategoriaById")]
        public async Task<IActionResult> GetById(int id) => Ok(await _catRepo.GetAsync(id));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CategoriasAddEditVM model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = model.ToData();
            _catRepo.Add(data);
            await _uow.CommitAsync();
            return CreatedAtRoute("GetCategoriaById", new { id = data.Id}, data); //return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CategoriasAddEditVM model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = model.ToData(id);
            if (data == null) return BadRequest(new { erro = "Erro, Otario..." });
            _catRepo.Update(data);
            await _uow.CommitAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = await _catRepo.GetAsync(id);
            if (data == null) return BadRequest(new { erro = "Erro, Otario..."});
            _catRepo.Deletee(data);
            await _uow.CommitAsync();
            return NoContent();
        }

    }
}
