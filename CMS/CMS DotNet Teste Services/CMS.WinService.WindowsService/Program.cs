#define MYTEST

using System;
using System.ServiceProcess;
using System.Threading;

namespace CMS.WinService.WindowsService
{
    static class Program
    {
        //installutil.exe -i path mais exe
        //installutil.exe -u path mais exe

        public static void Main()
        {
            
            Thread.CurrentThread.CurrentCulture   = new System.Globalization.CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");

#if (DEBUG && !MYTEST)
                    C // Consoleonsole.WriteLine("DEBUG is defined");
#elif (!DEBUG && MYTEST)
                     // Console.WriteLine("MYTEST is defined");
#elif (DEBUG && MYTEST)
            // Console.WriteLine("DEBUG and MYTEST are defined");
#else
                     // Console.WriteLine("DEBUG and MYTEST are not defined");
#endif

#if DEBUG
                var ServicesToRun = new Service1();
                ServicesToRun.onDebug();
#else
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new Service1() };
                ServiceBase.Run(ServicesToRun);
#endif

        }
    }
}
