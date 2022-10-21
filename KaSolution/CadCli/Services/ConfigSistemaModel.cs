using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadCli.Services
{
    public class ConfigSistemaModel
    {
        public string ConnString    { get; set; }
        public string VersaoSistema { get; set; }
        public string Descricao     { get; set; }
    }
}
