using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.UI.Infra
{
    public class SeriLogFile : ILogCustom
    {

        private readonly Logger _logger;

        public SeriLogFile()
        {
            _logger = new LoggerConfiguration()
                .WriteTo
                .File($"logs/{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}.txt")
                .CreateLogger();
        }

        public void LogError(string msg)
        {
            _logger.Error("CMS Error Msg: " + msg);
        }

        public void LogInformation(string msg)
        {
            _logger.Information("CMS Info Msg: " + msg);
        }

        public void LogWarnning(string msg)
        {
            _logger.Warning("CMS Warnning Msg: " + msg);
        }
    }
}
