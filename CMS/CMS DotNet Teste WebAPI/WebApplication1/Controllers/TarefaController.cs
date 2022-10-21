using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models.Context;
using WebApplication1.Models.Entities;

namespace WebApplication1.Controllers
{
    public class TarefaController : ApiController
    {

        private BancoContext db = new BancoContext();

        //public TarefaController(BancoContext bancoContext)
        //{
        //    this.db = bancoContext;
        //}

        // GET: api/Tarefa
        public IQueryable<Tarefa> GetTarefas()
        {
            return db.Tarefas;
        }

        // GET: api/Tarefa/5
        [ResponseType(typeof(Tarefa))]
        public IHttpActionResult GetTarefa(int id)
        {
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        // PUT: api/Tarefa/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTarefa(int id, Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarefa.Id)
            {
                return BadRequest();
            }

            db.Entry(tarefa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tarefa
        [ResponseType(typeof(Tarefa))]
        public IHttpActionResult PostTarefa(Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tarefas.Add(tarefa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tarefa.Id }, tarefa);
        }

        // DELETE: api/Tarefa/5
        [ResponseType(typeof(Tarefa))]
        public IHttpActionResult DeleteTarefa(int id)
        {
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            db.Tarefas.Remove(tarefa);
            db.SaveChanges();

            return Ok(tarefa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TarefaExists(int id)
        {
            return db.Tarefas.Count(e => e.Id == id) > 0;
        }
    }
}