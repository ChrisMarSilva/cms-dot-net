using FluentValidation;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using WebApplication1.Filters;
using WebApplication1.Models.Context;
using WebApplication1.Models.Entities;
using WebApplication1.Models.Validation;

namespace WebApplication1.Controllers
{

    [Route("api/v1/[controller]")]
    //[Route("api/v1/empr")]
    public class EmpresasController : ApiController
    {

        private BancoContext db = new BancoContext();
        private EmpresaValidator validador = new EmpresaValidator();

        //public EmpresasController(BancoContext bancoContext)
        //{
        //    this.db = bancoContext;
        //}

        // GET: api/Empresas
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.InlineCount, MaxTop = 10, PageSize = 10)]
        public IQueryable<Empresa> GetEmpresas()
        {
            return db.Empresas;
        }

        // GET: api/Empresas/5
        public IHttpActionResult GetEmpresa(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Empresa empresa = db.Empresas.Find(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        [Route("api/empresas/{id}/vagas")]
        public IHttpActionResult GetVagas(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Empresa empresa = db.Empresas.Find(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa.Vagas);
        }

        // PUT: api/Empresas/5
        [BasicAuhtentication]
        public IHttpActionResult PutEmpresa(int id, Empresa empresa)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            if (id != empresa.Id)
                return BadRequest("O id informado na URL deve ser igual ao id informado no corpo da requisição.");

            if (db.Empresas.Count(e => e.Id == id) == 0)
                return NotFound();

            validador.ValidateAndThrow(empresa);

            db.Entry(empresa).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Empresas
       // [BasicAuhtentication]
        public IHttpActionResult PostEmpresa(Empresa empresa)
        {
            validador.ValidateAndThrow(empresa);

            db.Empresas.Add(empresa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = empresa.Id }, empresa);
        }

        // DELETE: api/Empresas/5
        [BasicAuhtentication]
        public IHttpActionResult DeleteEmpresa(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Empresa empresa = db.Empresas.Find(id);

            if (empresa == null)
                return NotFound();

            if (empresa.Vagas.Count(v => v.Ativa) > 0)
                return Content(HttpStatusCode.Forbidden, "Essa empresa não pode ser excluída, pois há vagas ativas relacionadas a ela.");

            db.Empresas.Remove(empresa);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}