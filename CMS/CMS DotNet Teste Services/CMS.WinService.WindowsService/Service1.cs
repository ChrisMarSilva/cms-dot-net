using System;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
using System.ServiceProcess;
using System.Configuration;
using System.Timers;

namespace CMS.WinService.WindowsService
{
    [RunInstaller(true)]
    public partial class Service1 : ServiceBase
    {

        private int _intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["ThreadTime"]);
        //private Thread _worker    = null;
        private System.Timers.Timer _timer = null;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //RegistrarMensagem();
            try
            {
                // TESTE COM THREAD
                // ThreadStart start = new ThreadStart(Working);
                // this._worker = new Thread(start);
                // this._worker.Start();

                // TESTE COM TIME
                _timer = new System.Timers.Timer { Interval = _intervalo };
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(OnExecute);
                _timer.Enabled = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        protected override void OnStop()
        {
            try
            {
                // TESTE COM THREAD
                //if ((this._worker != null) & this._worker.IsAlive)
                //{
                //    this._worker.Abort();
                //}

                // TESTE COM TIME
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        // TESTE COM TIME
        private void OnExecute(object sender, ElapsedEventArgs e)
        {
            this._timer.Enabled = false; 
            try
            {
            }
            catch (Exception ex)
            {
            }
            finally
            {
                this._timer.Enabled = true;
            }
        }

        // TESTE COM THREAD
        private void Working()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(1000 * 60 * this._intervalo);// Em Minutos
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        public void onDebug()
        {
            this.OnStart(null);
        }

        public static void RegistrarMensagem()
        {
            try
            {
                var source = "SampleApplicationSource";

                try
                {
                    if (!System.Diagnostics.EventLog.SourceExists(source))
                    {
                        System.Diagnostics.EventLog.CreateEventSource(source, "Application");
                        //System.Diagnostics.EventLog.CreateEventSource(source, "MyNewLog", "MyServer");
                    }
                }
                catch (Exception ex)
                {
                }
                try
                {
                    var eventLog = new System.Diagnostics.EventLog();
                    eventLog.Source = source;
                    eventLog.Log = "MyNewLog";
                    eventLog.WriteEntry("testesssss", EventLogEntryType.Information);
                    //eventLog.WriteEntry("Este serviço esta executando - Error!", EventLogEntryType.Error, _eventId++);
                    //eventLog.WriteEntry("Este serviço esta executando - Warning!", EventLogEntryType.Warning, _eventId++);
                    //eventLog.WriteEntry("Este serviço esta executando - Information!", EventLogEntryType.Information, _eventId++);
                    //eventLog.WriteEntry("Este serviço esta executando - SuccessAudit!", EventLogEntryType.SuccessAudit, _eventId++);
                    //eventLog.WriteEntry("Este serviço esta executando - FailureAudit!", EventLogEntryType.FailureAudit, _eventId++);
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

    }
}
