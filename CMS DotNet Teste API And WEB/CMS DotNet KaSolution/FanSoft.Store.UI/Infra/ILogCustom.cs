using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.UI.Infra
{
    public interface ILogCustom
    {
        void LogError(string msg);
        void LogInformation(string msg);
        void LogWarnning(string msg);
    }
}
