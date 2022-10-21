using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Teste.Console
{
    class Mensagem
    {
        private readonly ILogger<Mensagem> _logger;
        //private ILogger<Mensagem> _logger = new Logger<Mensagem>(new NullLoggerFactory());

        public Mensagem(ILogger<Mensagem> logger = null)
        {
            this._logger = logger;
            //this._logger = logger ?? NullLogger<Mensagem>.Instance;
        }

        public void Processar()
        {
            if (this._logger != null)
                this._logger.LogInformation($"Mensagem.Processar...");
            System.Console.WriteLine("ok");
        }

    }
}
