﻿using FluentValidation;
using System;
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
    public class VagasController : ApiController
    {
        private BancoContext db = new BancoContext();
        private VagaValidator validador = new VagaValidator();

        //public VagasController(BancoContext bancoContext)
        //{
        //    this.db = bancoContext;
        //}

        // GET: api/Vagas
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Select | AllowedQueryOptions.Skip | AllowedQueryOptions.Top, MaxTop = 10, PageSize = 10)]
        public IQueryable GetVagas()
        {
            return db.Vagas.Where(v => v.Ativa);
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Select)]
        public IHttpActionResult GetVagas(int pagina = 1, int tamanhoPagina = 10)
        {
            if (pagina <= 0 || tamanhoPagina <= 0)
                return BadRequest("Os parâmetros pagina e tamanhoPagina devem ser maiores que zero.");

            if (tamanhoPagina > 10)
                return BadRequest("O tamanho máximo de página permitido é 10.");

            int totalPaginas = (int)Math.Ceiling(db.Vagas.Count() / Convert.ToDecimal(tamanhoPagina));

            if (totalPaginas > 0 && pagina > totalPaginas)
                return BadRequest("A página solicitada não existe.");

            System.Web.HttpContext.Current.Response.AddHeader("X-Pagination-TotalPages",
               totalPaginas.ToString());

            if (pagina > 1)
                System.Web.HttpContext.Current.Response.AddHeader("X-Pagination-PreviousPage", Url.Link("DefaultApi", new { pagina = pagina - 1, tamanhoPagina = tamanhoPagina }));

            if (pagina < totalPaginas)
                System.Web.HttpContext.Current.Response.AddHeader("X-Pagination-NextPage", Url.Link("DefaultApi", new { pagina = pagina + 1, tamanhoPagina = tamanhoPagina }));

            var vagas = db.Vagas.Where(v => v.Ativa).OrderBy(v => v.Id).Skip(tamanhoPagina * (pagina - 1)).Take(tamanhoPagina);

            return Ok(vagas.AsQueryable());
        }

        // GET: api/Vagas/5
        public IHttpActionResult GetVaga(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Vaga vaga = db.Vagas.Find(id);

            if (vaga == null)
                return NotFound();

            return Ok(vaga);
        }

        // PUT: api/Vagas/5
        [BasicAuhtentication]
        public IHttpActionResult PutVaga(int id, Vaga vaga)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            if (id != vaga.Id)
                return BadRequest("O id informado na URL deve ser igual ao id informado no corpo da requisicao.");

            if (db.Vagas.Count(v => v.Id == id) == 0)
                return NotFound();

            validador.ValidateAndThrow(vaga);

            var idsRequisitosEditados = vaga.Requisitos.Where(r => r.Id > 0).Select(r => r.Id);

            var requisitosExcluidos = db.Requisitos.Where(r => r.Vaga.Id == id && !idsRequisitosEditados.Contains(r.Id));

            db.Requisitos.RemoveRange(requisitosExcluidos);

            foreach (var requisito in vaga.Requisitos)
            {
                if (requisito.Id > 0)
                    db.Entry(requisito).State = EntityState.Modified;
                else
                    db.Entry(requisito).State = EntityState.Added;
            }

            db.Entry(vaga).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Vagas
        [BasicAuhtentication]
        public IHttpActionResult PostVaga(Vaga vaga)
        {
            validador.ValidateAndThrow(vaga);

            vaga.Ativa = true;

            db.Vagas.Add(vaga);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vaga.Id }, vaga);
        }

        // DELETE: api/Vagas/5
        //[BasicAuhtentication]
        public IHttpActionResult DeleteVaga(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            Vaga vaga = db.Vagas.Find(id);

            if (vaga == null)
                return NotFound();

            db.Vagas.Remove(vaga);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
