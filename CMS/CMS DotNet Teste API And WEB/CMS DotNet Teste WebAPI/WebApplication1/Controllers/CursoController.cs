using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using WebApplication1.Models.Context;
using WebApplication1.Models.Entities;

namespace WebApplication1.Controllers
{
    public class CursoController : ApiController
    {

        private BancoContext db = new BancoContext();

        //public CursoController(BancoContext bancoContext)
        //{
        //    this.db = bancoContext;
        //}

        // GET: api/Curso
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Select | AllowedQueryOptions.Skip | AllowedQueryOptions.Top, MaxTop = 10, PageSize = 10)]
        public IQueryable GetCurso()
        {
            return db.Cursos;
        }

        public IHttpActionResult GetCurso(int pagina = 1, int tamanhoPagina = 10) // SELECT ALL
        {
            if (pagina <= 0 || tamanhoPagina <= 0)
            {
                return BadRequest("Os parametros pagina e tamanhoPagina devem ser maiores que zero.");
            }

            if (tamanhoPagina > 10)
            {
                return BadRequest("O tamanho maximo de pagina permitido e 10.");
            }

            int totalPaginas = (int)Math.Ceiling(db.Cursos.Count() / Convert.ToDecimal(tamanhoPagina));

            if (pagina > totalPaginas)
            {
                return BadRequest("A pagina solicitada nao existe.");
            }

            System.Web.HttpContext.Current.Response.AddHeader("X-Pagination-TotalPages", totalPaginas.ToString());

            if (pagina > 1)
            {
                System.Web.HttpContext.Current.Response.AddHeader("X-Pagination-PreviousPage", Url.Link("DefaultApi", new { pagina = pagina - 1, tamanhoPagina = tamanhoPagina }));
            }

            if (pagina < totalPaginas)
            {
                System.Web.HttpContext.Current.Response.AddHeader("X-Pagination-NextPage", Url.Link("DefaultApi", new { pagina = pagina + 1, tamanhoPagina = tamanhoPagina }));
            }

            var cursos = db.Cursos.OrderBy(c => c.DataPublicacao).Skip(tamanhoPagina * (pagina - 1)).Take(tamanhoPagina);

            return Ok(cursos);
        }

        public IHttpActionResult GetCurso(int id) // SELECT WHERE
        {
            if (id <= 0)
            {
                return BadRequest("O id deve ser um numero maior que zero.");
            }

            var curso = db.Cursos.Find(id);

            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso);
        }

        public IHttpActionResult PostCurso(Curso curso) // INSERT
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cursos.Add(curso);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = curso.Id }, curso);
        }

        public IHttpActionResult PutCurso(int id, Curso curso) // UPDATE
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != curso.Id)
            {
                return BadRequest("O id informado na URL e diferente do id informado no corpo da requisicao.");
            }

            if (db.Cursos.Count(c => c.Id == id) == 0)
            {
                return NotFound();
            }

            db.Entry(curso).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteCurso(int id) // DELETE 
        {
            if (id <= 0)
            {
                return BadRequest("O id deve ser um numero maior que zero.");
            }

            var curso = db.Cursos.Find(id);

            if (curso == null)
            {
                return NotFound();
            }

            db.Cursos.Remove(curso);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
