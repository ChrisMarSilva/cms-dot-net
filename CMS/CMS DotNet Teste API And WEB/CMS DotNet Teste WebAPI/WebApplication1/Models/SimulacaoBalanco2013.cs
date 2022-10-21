using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class SimulacaoBalanco2013
    {

        public static List<BalancoComercialPorPais> ObterBalancoPaises()
        {
            List<BalancoComercialPorPais> dados = new List<BalancoComercialPorPais>();
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2013, Pais = "Alemanha", Sigla = "DE", ValorExportado = 39.1, ValorImportado = 20.9 });
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2013, Pais = "Canadá", Sigla = "CA", ValorExportado = 17.8, ValorImportado = 4 });
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2013, Pais = "China", Sigla = "CN", ValorExportado = 50.5, ValorImportado = 36.7 });
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2013, Pais = "Estados Unidos", Sigla = "US", ValorExportado = 56.2, ValorImportado = 28.2 });
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2013, Pais = "Japão", Sigla = "JP", ValorExportado = 45.1, ValorImportado = 18.7 });
            dados.Add(new BalancoComercialPorPais() { AnoBase = 2013, Pais = "Reino Unido", Sigla = "GB", ValorExportado = 35, ValorImportado = 8 });
            return dados;
        }

        public static List<BalancoComercialPorContinente> ObterBalancoContinentes()
        {
            List<BalancoComercialPorContinente> dados = new List<BalancoComercialPorContinente>();
            dados.Add(new BalancoComercialPorContinente() { AnoBase = 2013, Continente = "América", ValorExportado = 74, ValorImportado = 32.2 });
            dados.Add(new BalancoComercialPorContinente() { AnoBase = 2013, Continente = "Ásia", ValorExportado = 95.6, ValorImportado = 55.4 });
            dados.Add(new BalancoComercialPorContinente() { AnoBase = 2013, Continente = "Europa", ValorExportado = 74.1, ValorImportado = 28.9 });
            return dados;
        }

    }
}