using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class VendasController : ApiController
    {

        private BancoContext db = new BancoContext();
        private VendaValidator validadorVenda = new VendaValidator();
        private ItemVendaValidator validadorItemVenda = new ItemVendaValidator();

        //public VendasController(BancoContext bancoContext)
        //{
        //    this.db = bancoContext;
        //}

        // GET: api/Vagas
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Select | AllowedQueryOptions.Skip | AllowedQueryOptions.Top, MaxTop = 10, PageSize = 10)]
        public IQueryable GetVendas()
        {
            return db.Vendas.Include("ItemVenda");
        }

        // GET: api/Vagas/5
        public IHttpActionResult GetVenda(int id)
        {
            //if (id <= 0)
            //    return BadRequest("O id informado na URL deve ser maior que zero.");

            //Vaga vaga = db.Vagas.Find(id);

            //if (vaga == null)
            //    return NotFound();

            //return Ok(vaga);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Vagas/5
        [BasicAuhtentication]
        public IHttpActionResult PutVenda(int id, Vaga vaga)
        {
            //if (id <= 0)
            //    return BadRequest("O id informado na URL deve ser maior que zero.");

            //if (id != vaga.Id)
            //    return BadRequest("O id informado na URL deve ser igual ao id informado no corpo da requisicao.");

            //if (db.Vagas.Count(v => v.Id == id) == 0)
            //    return NotFound();

            //validador.ValidateAndThrow(vaga);

            //var idsRequisitosEditados = vaga.Requisitos.Where(r => r.Id > 0).Select(r => r.Id);

            //var requisitosExcluidos = db.Requisitos.Where(r => r.Vaga.Id == id && !idsRequisitosEditados.Contains(r.Id));

            //db.Requisitos.RemoveRange(requisitosExcluidos);

            //foreach (var requisito in vaga.Requisitos)
            //{
            //    if (requisito.Id > 0)
            //        db.Entry(requisito).State = EntityState.Modified;
            //    else
            //        db.Entry(requisito).State = EntityState.Added;
            //}

            //db.Entry(vaga).State = EntityState.Modified;
            //db.SaveChanges();

            //return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Vagas
        [BasicAuhtentication]
        public IHttpActionResult PostVenda(Vaga vaga)
        {
            //validador.ValidateAndThrow(vaga);

            //vaga.Ativa = true;

            //db.Vagas.Add(vaga);
            //db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = vaga.Id }, vaga);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Vagas/5
        //[BasicAuhtentication]
        public IHttpActionResult DeletVenda(int id)
        {
            //if (id <= 0)
            //    return BadRequest("O id informado na URL deve ser maior que zero.");

            //Vaga vaga = db.Vagas.Find(id);

            //if (vaga == null)
            //    return NotFound();

            //db.Vagas.Remove(vaga);
            //db.SaveChanges();

            //-----------------------------------------------------

            ItemVenda item1 = new ItemVenda() { Descricao = "Cabo USB 2.5m", Preco = 35, Quantidade = 1 };
            ItemVenda item2 = new ItemVenda() { Descricao = "", Preco = 0, Quantidade = 0 };
            Venda venda = new Venda() { Data = DateTime.Today.AddDays(10), Tipo = TipoVenda.Padrao, Itens = new List<ItemVenda>(new[] { item1, item2 }) };

            //validadorVenda
            //validadorItemVenda
            //VendaValidator validador = new VendaValidator();
            //ValidationResult resultado = validador.Validate(venda);
           // validadorVenda.ValidateAndThrow(venda);
                //Console.WriteLine("Venda inválida.");
                //excecao.Errors.ToList().ForEach(e => Console.WriteLine($"{e.PropertyName} : {e.ErrorMessage}"));
            //-----------------------------------------------------

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}