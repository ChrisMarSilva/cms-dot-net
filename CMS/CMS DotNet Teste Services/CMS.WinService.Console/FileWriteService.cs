using System;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace CMS.WinService.Console
{
    public class FileWriteService : ServiceBase
    {

        private Thread Worker = null;

        public FileWriteService()
        {
            //ServiceName = "MyCoreService";
            ServiceName = "CMSTesteServiceWin";
            this.RegistrarLogArquivo("FileWriteService");
        }

        protected override void OnStart(string[] args)
        {
            this.RegistrarLogArquivo("OnStart");
            ThreadStart start = new ThreadStart(Working);
            this.Worker = new Thread(start);
            this.Worker.Start();
        }

        private void Working()
        {
            this.RegistrarLogArquivo("Working");
            int nSleep = 1; // 1 minute
            try
            {
                while (this.Worker.IsAlive)  // (true) 
                {
                    //Thread.CurrentThread.Name
                    this.RegistrarLogArquivo(".NET Core Windows Service Called on");
                    //Thread.Sleep(1000 * 60 * nSleep);// 1 minute
                    Thread.Sleep(1000 * 10);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void OnStop()
        {
            this.RegistrarLogArquivo("OnStop");
            if ((this.Worker != null) & this.Worker.IsAlive)
            {
                this.RegistrarLogArquivo(".NET Core Windows Service Stopped on");
                this.Worker.Abort();
            }
        }

        protected override void OnContinue()
        {
            this.RegistrarLogArquivo("OnContinue");
        }

        protected override void OnPause()
        {
            this.RegistrarLogArquivo("OnPause");
        }


        protected override void OnShutdown()
        {
            this.RegistrarLogArquivo("OnPause");
        }

        public void onDebug()
        {
            this.OnStart(null);


            //install - Package Microsoft.Windows.Compatibility

            //  (i) To publish as Release version
            //              dotnet publish--configuration Release
            //            na pasta do projeto antes da pasta bin

            //(ii) To Create the Service
            //         sc create YourServiceName binPath = "YourpathName+ServiceName.exe"
            //         sc create CMSTesteServiceWin binPath = "pasta bin + pasta realise+ pasta netcoreapp2 + pasta win7-x64 + ServiceName.exe"

            //(iii) To start/ install the Service
            //         sc start YourServiceName
            //         sc start CMSTesteServiceWin

            //(iv) To delete/ uninstall the Service
            //         sc delete YourServiceName
            //         sc delete CMSTesteServiceWin


        }

        private void RegistrarLogArquivo(string logMessage)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var filePath = String.Format("{0}\\{1}_{2}.txt", path, ServiceName, DateTime.Now.ToString("yyyyMMdd", CultureInfo.CurrentCulture));

            //bool addTimeStamp = true
            //if (addTimeStamp)
            logMessage = String.Format("[{0}] - {1}", DateTime.Now.ToString("HH:mm:ss", CultureInfo.CurrentCulture), logMessage);

            // File.AppendAllText(filePath, logMessage);

            string filename = @"D:\CMSMyCoreService.txt";
            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                writer.WriteLine(string.Format(logMessage + " " + DateTime.Now.ToString("dd /MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }

    }
}
