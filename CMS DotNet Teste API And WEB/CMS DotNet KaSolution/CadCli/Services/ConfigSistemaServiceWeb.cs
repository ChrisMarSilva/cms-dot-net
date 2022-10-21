using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Services
{
    public class ConfigSistemaServiceWeb : IConfigSistemaService
    {

        private ConfigSistemaModel _dados;

        public ConfigSistemaServiceWeb()
        {

            this._dados = new ConfigSistemaModel()
            {
                ConnString    = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=cadclidb_homol;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                VersaoSistema = "2.0.0",
                Descricao     = "abc"
            };

        }

        public ConfigSistemaModel Dados => this._dados;

    }
}
