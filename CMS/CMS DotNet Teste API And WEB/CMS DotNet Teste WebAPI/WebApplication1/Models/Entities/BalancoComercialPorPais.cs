using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Entities
{
    public class BalancoComercialPorPais
    {
        public int AnoBase { get; set; }
        public string Pais { get; set; }
        public string Sigla { get; set; }
        public double ValorExportado { get; set; }
        public double ValorImportado { get; set; }
    }
}