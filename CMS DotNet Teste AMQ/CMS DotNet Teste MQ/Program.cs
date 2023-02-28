using IBM.WMQ;
using JD.MQ.Core;
using System;

namespace TesteMQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {

                string _qmHostName = "localhost";
                int _qmPort = 15522;
                string _qmChannel = "QM.04358798.05";
                string _qmQueueManagerName = "QM.04358798.05";
                string _qmQueueName = "FL_DDA0110R1";
                SendOptions _qmSendOptions = new JD.MQ.Core.SendOptions() { }; ;
                int _qmWait = 1;
                string msgXMLEnvio = "TESTE";
                string msgXMLRetorno = "";

                using (var qm = new QueueManager(new QueueManagerDTO() { HostName = _qmHostName, Port = _qmPort, ChannelName = _qmChannel, QueueManager = _qmQueueManagerName }))
                {
                    qm.Connect(connectAs: ConnectAs.Client);
                    try
                    {
                        using (var fila = new Queue(qm))
                        {
                            fila.OpenQueue(queueName: _qmQueueName, openAs: OpenAs.PutGet); // OnlyPut // OnlyGet
                            try
                            {
                                try
                                {

                                    //------------------------------ 

                                    qm.BeginTransaction();
                                    var sendMessage = fila.SendMessage(message: msgXMLEnvio, sendOptions: _qmSendOptions);
                                    Console.WriteLine($"Enviada com Suesso - MessageId: {sendMessage.MessageId}");
                                    qm.CommitTransaction();

                                    //------------------------------ 

                                    qm.BeginTransaction();
                                    ReceiveMessage receiveMessage = fila.ReceiveMessage(messageId: null, correlationId: null, wait: _qmWait);
                                    if (receiveMessage.NotFound)
                                    {
                                        try { qm.CommitTransaction(); } catch { }
                                        return;
                                    }

                                    msgXMLRetorno = receiveMessage.MessageContentInString;
                                    if (string.IsNullOrEmpty(msgXMLRetorno))
                                    {
                                        try { qm.CommitTransaction(); } catch { }
                                        return;
                                    }

                                    Console.WriteLine($"Recebido com Suesso - XML: {msgXMLRetorno} - {receiveMessage.PutDateTime} - {receiveMessage.AccountingToken}"); // AccountingToken
                                    qm.CommitTransaction();

                                    //------------------------------ 

                                }
                                catch (MQException e)
                                {
                                    try { if (qm.InTransaction) qm.RollbackTransaction(); } catch { }
                                    Console.WriteLine($"FALHA-MQ: {e.ReasonCode} - {e.Message}");
                                }
                                catch (Exception e)
                                {
                                    try { if (qm.InTransaction) qm.RollbackTransaction(); } catch { }
                                    Console.WriteLine($"FALHA: {e.Message}");
                                }
                            }
                            finally
                            {
                                fila.CloseQueue();
                            }
                        } // using (var fila = new Queue())
                    }
                    finally
                    {
                        qm.Disconnect();
                    }
                } // using (var qm = new QueueManager()) 

            }
            catch (MQException e)
            {
                Console.WriteLine($"ERRO-MQ: {e.ReasonCode} - {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERRO: {e.Message}");
            }
            finally
            {
                Console.WriteLine("FIM");
                Console.ReadLine();
            }
        }
    }
}
