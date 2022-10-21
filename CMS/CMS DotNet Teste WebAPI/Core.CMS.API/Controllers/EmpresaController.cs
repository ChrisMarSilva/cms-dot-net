using Core.CMS.Data.Contexts;
using Core.CMS.Data.Repositories;
using Core.CMS.Domain.Entities;
using Core.CMS.Domain.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.CMS.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {

        private IRepository<Empresa> _empRepo;
        private IUnitofWork _uow;
       // private BancoDeDadosContext _ctx;
        private EmpresaValidator _validador;

        public EmpresaController(IRepository<Empresa> catRepo, IUnitofWork uow) 
        {
            _empRepo = catRepo;
            _uow     = uow;
            this._validador = new EmpresaValidator();
           // this._ctx = new BancoDeDadosContext();
           // this._uow = new UnitOfWork(this._ctx);
           // this._empRepo = new EmpresaRepository(this._ctx);
        }


        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var empresas = await this._empRepo.GetAsync();
            return Ok(empresas);
        }

        [HttpGet("{id}", Name = "GetEmpresaById")]
        public async Task<IActionResult> GetById(int id)
        {
            var empresa = await this._empRepo.GetAsync(id);
            if (empresa == null)
                return NotFound();
            return Ok(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Empresa empresa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            this._validador.ValidateAndThrow(empresa);
            this._empRepo.Add(empresa);
            await this._uow.CommitAsync();
            return CreatedAtRoute("DefaultApi", new { id = empresa.Id }, empresa); // return CreatedAtRoute("GetEmpresaById", new { id = empresa.Id }, empresa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Empresa empresa)
        {
            if (id <= 0)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != empresa.Id)
                return BadRequest();
            this._validador.ValidateAndThrow(empresa);
            this._empRepo.Update(empresa);
            await this._uow.CommitAsync();
            return NoContent();  //return StatusCode(HttpStatusCode.NoContent); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var empresa = await this._empRepo.GetAsync(id);
            if (empresa == null)
                return NotFound();
            this._empRepo.Delete(empresa);
            await this._uow.CommitAsync();
            return NoContent();  //return StatusCode(HttpStatusCode.NoContent); 
        }
    }
}