using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{

    public static class SimulacaoBalanco2012
    {
        public static List<BalancoComercialPorPais> ObterBalancoPaises()
        {
            List<BalancoComercialPorPais> dados = new List<BalancoComercialPorPais>();
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2012, Pais = "Alemanha", Sigla = "DE", ValorExportado = 38.6, ValorImportado = 20.2 });
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2012, Pais = "Canadá", Sigla = "CA", ValorExportado = 17.2, ValorImportado = 3.4 });
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2012, Pais = "China", Sigla = "CN", ValorExportado = 49.8, ValorImportado = 36.2 });
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2012, Pais = "Estados Unidos", Sigla = "US", ValorExportado = 55.4, ValorImportado = 27.8 });
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2012, Pais = "Japão", Sigla = "JP", ValorExportado = 44.2, ValorImportado = 18.4 });
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2012, Pais = "Reino Unido", Sigla = "GB", ValorExportado = 34, ValorImportado = 7.8 });
            return dados;
        }

        public static List<BalancoComercialPorContinente> ObterBalancoContinentes()
        {
            List<BalancoComercialPorContinente> dados = new List<BalancoComercialPorContinente>();
            dados.Add(new BalancoComercialPorContinente() { AnoBase = 2012, Continente = "América", ValorExportado = 72.6, ValorImportado = 31.2 });
            dados.Add(new BalancoComercialPorContinente() { AnoBase = 2012, Continente = "Ásia", ValorExportado = 94, ValorImportado = 54.6 });
            dados.Add(new BalancoComercialPorContinente() { AnoBase = 2012, Continente = "Europa", ValorExportado = 72.6, ValorImportado = 28 });
            return dados;
        }
    }
}