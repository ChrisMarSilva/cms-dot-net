using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.WinService.Console
{
    public static class JDTraceApp
    {

        private static void Log(string tipo, string texto, string param)
        {
            System.Console.WriteLine($"{tipo} - {texto} - {param}");
            //  System.Diagnostics.Debug.WriteLine("Somente em modo de Debug");
        }

        public static void LogInicio(string texto, string param = "")
        {
            JDTraceApp.Log("I", texto, param);
        }

        public static void LogInfo(string texto, string param = "")
        {
            JDTraceApp.Log("L", texto, param);
        }

        public static void LogErro(string texto, string param = "")
        {
            JDTraceApp.Log("E", texto, param);
        }

        public static void LogFim(string texto, string param = "")
        {
            JDTraceApp.Log("F", texto, param);
        }

    }
}
