//using RGiesecke.DllExport;
//using System;
//using System.Runtime.InteropServices;

//namespace token
//{

//    [Guid("92B0396D-0B66-4A00-A9C6-BEC8C3080A05")]
//    [ComVisible(true)]
//    public class Delphi
//    {

//        [DllExport]
//        public static int teste()
//        {
//            return 1020;
//        }

//        [DllExport]
//        public static int Somar(int A, int B) => A + B;

//        [ComVisible(true)]
//        //[DllExport]
//        [DllExport("token_generate", CallingConvention = CallingConvention.StdCall)]
//        public static string token_generate(string message)
//        {
//            return $"<JDCMSGenerate>{message}</JDCMSGenerate>";
//        }

//        [ComVisible(true)]
//        //[DllExport]
//        [DllExport("token_validate", CallingConvention = CallingConvention.StdCall)]
//        public static string token_validate(string token)
//        {
//            return $"<JDCMSValidate>{token}</JDCMSValidate>";
//        }

//    }
//}


//using RGiesecke.DllExport;
//using System;
//using System.Runtime.InteropServices;

//[Guid("4ebe645f-de9c-41ac-8e95-b9ff6f9f2aed")]
//[ComVisible(true)]
//public class Token
//{
//    [DllExport("token_generate", CallingConvention = CallingConvention.StdCall)]
//    public static string token_generate(string message)
//    {
//        return $"<JDCMSGenerate2>{message}</JDCMSGenerate2>";
//    }

//    [DllExport("token_validate", CallingConvention = CallingConvention.StdCall)]
//    public static string token_validate(string token)
//    {
//        return $"<JDCMSValidate2>{token}</JDCMSValidate2>";
//    }
//}


using Dinamo.Hsm;
using RGiesecke.DllExport;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

[Guid("4ebe645f-de9c-41ac-8e95-b9ff6f9f2aed")]
[ComVisible(true)]
public class Token
{
    private const string DEFS_DEFAULT_HSM = "hsm.conf";
    private const string DEFS_DEFAULT_NETWORK = "network.conf";
    private const string DEFS_DEFAULT_PASSWORD = "password.conf";
    private const string DISASTER_HSM_DEFS = "disaster.hsm.conf";
    private const string DISASTER_NETWORK_DEFS = "disaster.network.conf";
    private const string DISASTER_PASSWORD_DEFS = "disaster.password.conf";

    private static Byte[] AES_KEY = Encoding.ASCII.GetBytes("Jn07NG1146j961z6O9B1T1uC1038t2Ir");
    private static Byte[] IV = Encoding.ASCII.GetBytes("".PadLeft(128 / 8, '0'));

    private static Object thisLock = new Object();

    private static string GetActiveHSMConfiguration()
    {
        return DEFS_DEFAULT_HSM;
    }

    private static string GetActiveNetworkConfiguration()
    {
        return DEFS_DEFAULT_NETWORK;
    }

    private static int GetNumberOfServers()
    {
        int number_of_servers = 0;
        string configuration_file = GetActiveNetworkConfiguration();
        string file_content = null;
        if (File.Exists(configuration_file))
        {
            file_content = File.ReadAllText(configuration_file);
        }

        string[] ip_address_array = file_content.Split("\n".ToCharArray());
        number_of_servers = ip_address_array.Length - 1;
        Console.WriteLine(number_of_servers);

        return number_of_servers;
    }

    private static string[] GetServers()
    {
        string[] server_entries = null;
        string[] servers = null;
        string configuration_file = GetActiveNetworkConfiguration();
        string file_content = null;
        if (!File.Exists(configuration_file))
            throw new FileNotFoundException(configuration_file);

        file_content = File.ReadAllText(configuration_file);
        server_entries = file_content.Split("\n".ToCharArray());
        servers = new string[server_entries.Length - 1];
        for (int i = 0; i < server_entries.Length - 1; i++)
        {
            servers[i] = server_entries[i].Replace("IP=", "");
            servers[i] = servers[i].Replace("\r", "");
        }

        return servers;
    }

    private static string GetIPAddress()
    {
        string[] server_address = GetServers();
        DateTime now = DateTime.Now;
        int i = now.Millisecond % server_address.Length;
        return server_address[i];
    }

    private static string GetUserID()
    {
        lock (thisLock)
        {
            string user_id = null;
            string configuration_file = GetActiveHSMConfiguration();
            string file_content = null;
            if (!File.Exists(configuration_file))
                throw new FileNotFoundException(configuration_file);

            file_content = File.ReadAllText(configuration_file);
            string[] entries = file_content.Split("\n".ToCharArray());
            foreach (string entry in entries)
            {
                if (entry.IndexOf("UserID") != -1)
                {
                    user_id = entry.Split("=".ToCharArray())[1];
                    user_id = user_id.Replace("\r", "");
                    break;
                }
            }
            return user_id;
        }
    }

    private static string GetPassword()
    {
        lock (thisLock)
        {
            string[] password = new string[2];
            string path = null;
            string configuration_file = GetActiveHSMConfiguration();
            string file_content = null;
            if (!File.Exists(configuration_file))
                throw new FileNotFoundException(configuration_file);

            file_content = File.ReadAllText(configuration_file);
            string[] entries = file_content.Split("\n".ToCharArray());
            foreach (string entry in entries)
            {
                if (entry.IndexOf("Password") != -1)
                {
                    // AES Encryption - Begin
                    string plain_text = null;
                    password[0] = entry.Replace("\r", "");
                    password[0] = password[0].Replace("Password=", "");
                    byte[] encrypted_password = Convert.FromBase64String(password[0]);
                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.Key = AES_KEY;
                        aesAlg.IV = IV;
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                        using (MemoryStream msDecrypt = new MemoryStream(encrypted_password))
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                                {
                                    plain_text = srDecrypt.ReadToEnd();
                                    plain_text = plain_text.Replace("\0", "");
                                }
                            }
                        }
                    }
                    password[0] = plain_text;
                    // AES Encryption - End
                }
                else if (entry.IndexOf("Path") != -1)
                {
                    path = entry.Split("=".ToCharArray())[1];
                    path = path.Replace("\r", "");
                }
            }
            string password_file = path + "\\" + "password.conf";
            if (!File.Exists(password_file))
                throw new FileNotFoundException(configuration_file);

            file_content = File.ReadAllText(password_file);
            entries = file_content.Split("\n".ToCharArray());
            foreach (string entry in entries)
            {
                if (entry.IndexOf("Password") != -1)
                {
                    // AES Encryption - Begin
                    string plain_text = null;
                    password[1] = entry.Replace("\r", "");
                    password[1] = password[1].Replace("Password=", "");
                    byte[] encrypted_password = Convert.FromBase64String(password[1]);
                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.Key = AES_KEY;
                        aesAlg.IV = IV;
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                        using (MemoryStream msDecrypt = new MemoryStream(encrypted_password))
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                                {
                                    plain_text = srDecrypt.ReadToEnd();
                                    plain_text = plain_text.Replace("\0", "");
                                }
                            }
                        }
                    }
                    password[1] = plain_text;
                    // AES Encryption - End
                    break;
                }
            }
            return password[0] + password[1];
        }
    }

    private static string GetPrivateKeyLabel()
    {
        lock (thisLock)
        {
            string user_id = null;
            string configuration_file = GetActiveHSMConfiguration();
            string file_content = null;
            if (!File.Exists(configuration_file))
                throw new FileNotFoundException(configuration_file);

            file_content = File.ReadAllText(configuration_file);
            string[] entries = file_content.Split("\n".ToCharArray());
            foreach (string entry in entries)
            {
                if (entry.IndexOf("PrivateKeyLabel") != -1)
                {
                    user_id = entry.Split("=".ToCharArray())[1];
                    user_id = user_id.Replace("\r", "");
                    break;
                }
            }
            return user_id;
        }
    }

    private static string GetCertificateLabel()
    {
        lock (thisLock)
        {
            string user_id = null;
            string configuration_file = GetActiveHSMConfiguration();
            string file_content = null;
            if (!File.Exists(configuration_file))
                throw new FileNotFoundException(configuration_file);

            file_content = File.ReadAllText(configuration_file);
            string[] entries = file_content.Split("\n".ToCharArray());
            foreach (string entry in entries)
            {
                if (entry.IndexOf("CertificateLabel") != -1)
                {
                    user_id = entry.Split("=".ToCharArray())[1];
                    user_id = user_id.Replace("\r", "");
                    break;
                }
            }
            return user_id;
        }
    }

    private static string GetLogFile()
    {
        lock (thisLock)
        {
            string log_file = null;
            string configuration_file = GetActiveHSMConfiguration();
            string file_content = null;
            if (!File.Exists(configuration_file))
                throw new FileNotFoundException(configuration_file);

            file_content = File.ReadAllText(configuration_file);
            string[] entries = file_content.Split("\n".ToCharArray());
            foreach (string entry in entries)
            {
                if (entry.IndexOf("LogFile") != -1)
                {
                    log_file = entry.Split("=".ToCharArray())[1];
                    log_file = log_file.Replace("\r", "");
                    break;
                }
            }
            return log_file;
        }
    }

    private enum LogEvent
    {
        STARTED = 0,
        INPUT = 1,
        OUTPUT = 2,
        ERROR = 3,
        ENDED = 4
    };

    private static void log(string method, LogEvent log_event, string message)
    {
        string log_file = GetLogFile();
        if (log_file == null)
            return;
        else if (log_file.Length == 0)
            return;
        StreamWriter writer = new StreamWriter(log_file, true);
        String line = "";
        CultureInfo cult = new CultureInfo("pt-BR");
        line += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff", cult);
        if (log_event.Equals(LogEvent.STARTED) || log_event.Equals(LogEvent.ENDED))
        {
            line += " ";
            line += method;
            line += " ";
            if (log_event.Equals(LogEvent.STARTED))
                line += "Iniciado.";
            else if (log_event.Equals(LogEvent.ENDED))
                line += "Finalizado.";
        }
        else
        {
            line += "\t\t";
            if (log_event.Equals(LogEvent.INPUT))
                line += "input = '";
            else if (log_event.Equals(LogEvent.OUTPUT))
                line += "output = '";
            else if (log_event.Equals(LogEvent.ERROR))
                line += "error = '";
            line += message;
            line += "'";
        }
        writer.WriteLine(line);
        writer.Close();
    }

    [DllExport("token_generate", CallingConvention = CallingConvention.StdCall)]
    public static string token_generate(string message)
    {
        lock (thisLock)
        {
            log("token_generate", LogEvent.STARTED, null);
            DinamoClient client = new DinamoClient();
            try
            {
                log("token_generate", LogEvent.INPUT, message);
                if (message == null)
                    throw new ArgumentException("Message required to generate token.");
                else if (message.Length == 0)
                    throw new ArgumentException("Message required to generate token.");

                string addrString = GetIPAddress();
                IPAddress address;
                if (!IPAddress.TryParse(addrString, out address))
                    throw new ArgumentException("Invalid IP address.");

                string user = GetUserID();
                string pass = GetPassword();
                string keyId = GetPrivateKeyLabel();
                string certId = GetCertificateLabel();

                string unsignedXML = message;
                client.Connect(address.ToString(), user, pass);
                byte[] signedXML = client.SignXML(keyId, DinamoClient.HASH_ALG.ALG_SHA1, certId, unsignedXML, "");
                string token = Encoding.ASCII.GetString(signedXML);
                log("token_generate", LogEvent.OUTPUT, token);
                log("token_generate", LogEvent.ENDED, null);
                return token;
            }
            catch (ArgumentNullException e)
            {
                client.Disconnect();
                log("token_generate", LogEvent.ERROR, e.Message);
                return "<Error><Code>0x000017</Code><Message>Authentication error: Invalid signature (" + e.Message + ")</Message></Error>";
            }
            catch (ArgumentException e)
            {
                client.Disconnect();
                log("token_generate", LogEvent.ERROR, e.Message);
                return "<Error><Code>35 (0x00023)</Code><Message>" + e.Message + "</Message></Error>";
            }
            catch (DllNotFoundException e)
            {
                client.Disconnect();
                log("token_generate", LogEvent.ERROR, e.Message);
                return "<Error><Code>0x00001C</Code><Message>" + e.Message + "</Message></Error>";
            }
            catch (DinamoException e)
            {
                client.Disconnect();
                log("token_generate", LogEvent.ERROR, e.Message);
                return "<Error><Code>0x00001D</Code><Message>Problemas no acesso ao HSM (" + e.Message + ")</Message></Error>";
            }
            catch (FileNotFoundException e)
            {
                client.Disconnect();
                log("token_generate", LogEvent.ERROR, e.Message);
                return "<Error><Code>0x00001E</Code><Message>" + e.Message + "</Message></Error>";
            }
            catch (Exception e)
            {
                client.Disconnect();
                log("token_generate", LogEvent.ERROR, e.Message);
                return "<Error><Code>0x000000</Code><Message>" + e.Message + "</Message></Error>";
            }
            finally
            {
                client.Disconnect();
                log("token_generate", LogEvent.ENDED, null);
            }
        }
    }

    [DllExport("token_validate", CallingConvention = CallingConvention.StdCall)]
    public static string token_validate(string token)
    {
        lock (thisLock)
        {
            log("token_validate", LogEvent.STARTED, null);
            DinamoClient client = new DinamoClient();
            try
            {
                log("token_validate", LogEvent.INPUT, token);
                if (token == null)
                    throw new ArgumentException("Message required to generate token.");
                else if (token.Length == 0)
                    throw new ArgumentException("Message required to generate token.");

                string addrString = GetIPAddress();
                IPAddress address;
                if (!IPAddress.TryParse(addrString, out address))
                    throw new ArgumentException("Invalid IP address.");

                string user = GetUserID();
                string pass = GetPassword();
                string keyId = GetPrivateKeyLabel();
                string certId = GetCertificateLabel();
                string signedXML = token;
                client.Connect(address.ToString(), user, pass);

                bool verified = client.VerifySignedXML(certId, signedXML, "");

                if (verified)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(signedXML);
                    XmlNodeList nodeList = doc.GetElementsByTagName("Signature");
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        XmlNode node = nodeList.Item(i);
                        XmlElement el = (XmlElement)node;
                        if (el != null)
                        {
                            el.ParentNode.RemoveChild(el);
                        }
                        Console.WriteLine(el.InnerText);
                    }
                    using (var stringWriter = new StringWriter())
                    using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                    {
                        doc.WriteTo(xmlTextWriter);
                        xmlTextWriter.Flush();
                        string message = stringWriter.GetStringBuilder().ToString();
                        log("token_validate", LogEvent.OUTPUT, message);
                        log("token_validate", LogEvent.ENDED, null);
                        return message;
                    }
                }
                else
                {
                    log("token_validate", LogEvent.ERROR, "Assinatura inválida.");
                    return "<Error><Code>0x000017</Code><Message>Authentication error: Invalid signature</Message></Error>";
                }
            }
            catch (DllNotFoundException e)
            {
                client.Disconnect();
                log("token_validate", LogEvent.ERROR, e.Message);
                return "<Error><Code>0x00001C</Code><Message>" + e.Message + "</Message></Error>";
            }
            catch (FileNotFoundException e)
            {
                client.Disconnect();
                log("token_validate", LogEvent.ERROR, e.Message);
                return "<Error><Code>0x00001E</Code><Message>" + e.Message + "</Message></Error>";
            }
            catch (BadImageFormatException e)
            {
                client.Disconnect();
                log("token_validate", LogEvent.ERROR, e.Message);
                return "<Error><Code>0x000022</Code><Message>" + e.Message + "</Message></Error>";
            }
            catch (ArgumentException e)
            {
                client.Disconnect();
                log("token_validate", LogEvent.ERROR, e.Message);
                return "<Error><Code>36 (0x00024)</Code><Message>Token required to generate message.</Message></Error>" + e.Message + "</Message></Error>";
            }
            catch (Exception e)
            {
                client.Disconnect();
                log("token_validate", LogEvent.ERROR, e.Message);
                return "<Error><Code>0x000000</Code><Message>" + e.Message + "</Message></Error>";
            }
            finally
            {
                client.Disconnect();
                log("token_validate", LogEvent.ENDED, null);
            }
        }
    }
}
