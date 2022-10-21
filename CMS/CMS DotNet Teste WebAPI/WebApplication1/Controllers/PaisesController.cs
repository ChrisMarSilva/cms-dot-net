using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApplication1.Models.Entities;

namespace WebApplication1.Controllers
{
    public class PaisesController : ApiController
    {
        public IEnumerable<BalancoComercialPorPais> Get(int ano)
        {
            if (ano == 2012)
                return SimulacaoBalanco2012.ObterBalancoPaises();
            else if (ano == 2013)
                return SimulacaoBalanco2013.ObterBalancoPaises();
            else
                throw new ArgumentException( "O ano-base informado é inválido.");
        }

    }
}
