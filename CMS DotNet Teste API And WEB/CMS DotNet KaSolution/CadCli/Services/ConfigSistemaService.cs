using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Services
{
    public class ConfigSistemaService : IConfigSistemaService
    {

        private IConfiguration _config;
        private ConfigSistemaModel _dados;

        public ConfigSistemaService(IConfiguration config)
        {
            this._config = config;

            this._dados = new ConfigSistemaModel()
            {
                ConnString = this._config.GetConnectionString("CadCli"),
                VersaoSistema = this._config["DadosSistema:Versao"],
                Descricao = this._config["DadosSistema:Descricao"]
            };

        }

        public ConfigSistemaModel Dados => this._dados;
            
    }
}
