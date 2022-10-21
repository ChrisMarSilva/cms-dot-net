using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebApi.OutputCache.V2;
using WebApplication1.Models.Attributes;
using WebApplication1.Models.Context;
using WebApplication1.Models.Entities;

namespace WebApplication1.Controllers
{
    public class AulasController : ApiController
    {

        private BancoContext db = new BancoContext();

        //public AulasController(BancoContext bancoContext)
        //{
        //    this.db = bancoContext;
        //}
        
        [DeflateCompression]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)] //Install-Package Strathweb.CacheOutput.WebApi2
        public IHttpActionResult GetAulas(int idCurso)
        {
            var curso = db.Cursos.Find(idCurso);

            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso.Aulas.OrderBy(a => a.Ordem).ToList());
        }

        public IHttpActionResult GetAula(int idCurso, int ordemAula)
        {
            var curso = db.Cursos.Find(idCurso);

            if (curso == null)
            {
                return NotFound();
            }

            var aula = curso.Aulas.FirstOrDefault(a => a.Ordem == ordemAula);

            if (aula == null)
            {
                return NotFound();
            }

            return Ok(aula);
        }

        public IHttpActionResult PutAula(int idCurso, int ordemAula, Aula aula)
        {
            var curso = db.Cursos.Find(idCurso);

            if (curso == null)
            {
                return NotFound();
            }

            var aulaAtual = curso.Aulas.FirstOrDefault(a => a.Ordem == ordemAula);

            if (aulaAtual == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (aula.Ordem > ordemAula)
            {
                int ultimaAula = curso.Aulas.Max(a => a.Ordem);
                if (aula.Ordem > ultimaAula)
                {
                    aula.Ordem = ultimaAula;
                }
                curso.Aulas.Where(a => a.Ordem > ordemAula && a.Ordem <= aula.Ordem).ToList().ForEach(a => a.Ordem--);
            }
            else if (aula.Ordem < ordemAula)
            {
                curso.Aulas.Where(a => a.Ordem >= ordemAula && a.Ordem < aula.Ordem).ToList().ForEach(a => a.Ordem++);
            }

            aulaAtual.Titulo = aula.Titulo;
            aulaAtual.Ordem = aula.Ordem;

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult PostAula(int idCurso, Aula aula)
        {
            var curso = db.Cursos.Find(idCurso);

            if (curso == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int proximaAula = curso.Aulas.Max(a => a.Ordem) + 1;

            if (aula.Ordem > proximaAula)
            {
                aula.Ordem = proximaAula;
            }
            else if (aula.Ordem < proximaAula)
            {
                curso.Aulas.Where(a => a.Ordem >= aula.Ordem).ToList().ForEach(a => a.Ordem++);
            }

            db.Aulas.Add(aula);
            db.SaveChanges();

            return CreatedAtRoute("Aulas", new { idCurso = idCurso, ordemAula = aula.Ordem }, aula);
        }

        public IHttpActionResult DeleteAula(int idCurso, int ordemAula)
        {
            var curso = db.Cursos.Find(idCurso);

            if (curso == null)
            {
                return NotFound();
            }

            var aula = curso.Aulas.FirstOrDefault(a => a.Ordem == ordemAula);

            if (aula == null)
            {
                return NotFound();
            }

            db.Entry(aula).State = EntityState.Deleted;

            curso.Aulas.Where(a => a.Ordem > ordemAula).ToList().ForEach(a => a.Ordem--);

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
