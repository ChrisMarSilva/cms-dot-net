using CMS.API.Helpers;
using CMS.Data.Contexts;
using CMS.Data.Repositories;
using CMS.Domain.Entities;
using CMS.Domain.Validation;
using FluentValidation;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CMS.API.Controllers
{

    [DeflateCompression]
    public class EmpresaController : ApiController
    {

        #region Private
        private IUnitofWork _uow;
        private IRepository<Empresa> _empRepo;
        private BancoDeDadosContext _ctx;
        private EmpresaValidator _validador;
        #endregion

        #region Constructor
        public EmpresaController()  //IRepository<Empresa> empRepo, IUnitofWork uow
        {
            // this._uow = uow;
            // this._empRepo = empRepo;
            this._validador = new EmpresaValidator();
            this._ctx       = new BancoDeDadosContext();
            this._uow       = new UnitOfWork(this._ctx);
            this._empRepo   = new EmpresaRepository(this._ctx);
        }
        #endregion

        #region GetAll
        // GET: api/Empresa
        public async Task<IHttpActionResult> GetEmpresa() //Task<IQueryable<Empresa>>
        {
            var empresas = await this._empRepo.GetAsync();
            return Ok(empresas);
        }
        #endregion

        #region GetId
        // GET: api/Empresa/5
        [ResponseType(typeof(Empresa))]
        public async Task<IHttpActionResult> GetEmpresa(int id)
        {
            var empresa = await this._empRepo.GetAsync(id);
            if (empresa == null)
                return NotFound();
            return Ok(empresa);
        }
        #endregion

        #region Post
        // POST: api/Empresa
        [ResponseType(typeof(Empresa))]
        public async Task<IHttpActionResult> PostEmpresa(Empresa empresa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            this._validador.ValidateAndThrow(empresa);
            this._empRepo.Add(empresa);
            await this._uow.CommitAsync();
            return CreatedAtRoute("DefaultApi", new { id = empresa.Id }, empresa);
        }
        #endregion

        #region Put
        // PUT: api/Empresa/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmpresa(int id, Empresa empresa)
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
            return StatusCode(HttpStatusCode.NoContent);
        }
        #endregion

        #region Delete
        // DELETE: api/Empresas/5
        [ResponseType(typeof(Empresa))]
        public async Task<IHttpActionResult> DeleteEmpresa(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var empresa = await this._empRepo.GetAsync(id);
            if (empresa == null)
                return NotFound();
            this._empRepo.Delete(empresa);
            await this._uow.CommitAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        #endregion

    }
}
