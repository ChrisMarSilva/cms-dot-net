using System;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Reflection;
using System.Text;

namespace CMS.File.Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio");
            try
            {

                var path = @"D:\CMSDotNetTesteFileLarg.xml";

                #region CMS Teste File Write

                //CMSTesteFileWriter(path); // 1,57 GB (1.688.888.898 bytes)

                #endregion

                #region CMS Teste File Reader

                //CMSTesteFileReader01(path); // System.IO.File.ReadAllText                                      ==> 00:00:00.0000000 ==> ERROR OutOfMemoryException
                //CMSTesteFileReader02(path); // System.IO.File.ReadAllLines                                     ==> 00:00:00.0000000 ==> ERROR OutOfMemoryException
                //CMSTesteFileReader03(path); // StreamReader                                                    ==> 00:00:10.6582093 ==> OK                            ******MELHOR******
                //CMSTesteFileReader04(path); // FileStream + StreamReader                                       ==> 00:00:11.4274725 ==> OK
                //CMSTesteFileReader05(path); // FileStream + StreamReader + stringBuilder                       ==> 00:00:00.0000000 ==> ERROR OutOfMemoryException
                //CMSTesteFileReader06(path); // FileStream + File + BufferedStream + StreamReader               ==> 00:00:11.3157696 ==> OK
                //CMSTesteFileReader07(path); // MemoryMappedFile + MemoryMappedViewStream                       ==> 00:05:07.2274984 ==> OK 
                //CMSTesteFileReader08(path); // FileStream + BinaryReader                                       ==> 00:00:00.0000000 ==> ERROR Não é possível ler depois do fim do fluxo.
                //CMSTesteFileReader09(path); // MemoryStream + BinaryReader                                     ==> 00:00:00.0000000 ==> ERROR OutOfMemoryException
                //CMSTesteFileReader10(path); // MemoryMappedFile + MemoryMappedViewStream+ BinaryReader         ==> 00:00:00.0000000 ==> ERROR OutOfMemoryException
                //CMSTesteFileReader11(path); // FileStream + File + MemoryStream + StreamReader + Peek + AsSpan ==> 00:00:00.0000000 ==> ERROR OutOfMemoryException

                #endregion

                GC.Collect();
                GC.WaitForPendingFinalizers();

                //var parser = new IniParser(@"C:\test.ini");
                //parser.GetSetting("appsettings", "msgpart1");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Fim");
                Console.ReadKey();
            }
        }

        static void CMSTesteFileWriter(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();//  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                //if (!System.IO.File.Exists(path))
                //string createText = "Hello and Welcome" + Environment.NewLine;
                //System.IO.File.WriteAllText(path, createText, Encoding.UTF8);
                //System.IO.File.AppendAllText(path, appendText, Encoding.UTF8);

                stopwatch.Reset();
                stopwatch.Start();

                using (StreamWriter outputFile = new StreamWriter(path))
                {
                    for (int i = 1; i <= 100000000; i++) // 100Milhoes
                    {
                        outputFile.WriteLine($"Linha: {i}");
                    }
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    outputFile.Close();
                }

                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static void CMSTesteFileReader01(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();//  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                stopwatch.Reset();
                stopwatch.Start();

                var texto = System.IO.File.ReadAllText(path);

                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static void CMSTesteFileReader02(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();//  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                stopwatch.Reset();
                stopwatch.Start();

                string[] linhas = System.IO.File.ReadAllLines(path); //File.ReadAllLines(path, Encoding.UTF8);
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

                foreach (string linha in linhas)
                {
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    // Console.WriteLine("\t" + linha);
                }

                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static void CMSTesteFileReader03(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();//  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                stopwatch.Reset();
                stopwatch.Start();

                using (StreamReader sr = new StreamReader(path))
                {
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    string linha = String.Empty; //  ""; // sr.ReadToEnd(); await sr.ReadToEndAsync();
                    while ((linha = sr.ReadLine()) != null)
                    {
                        //if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        //  Console.WriteLine(line);
                    }
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    sr.Close();
                }

                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static void CMSTesteFileReader04(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();//  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                stopwatch.Reset();
                stopwatch.Start();

                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    using (var sr2 = new StreamReader(fs, Encoding.UTF8))
                    {
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        string linha = String.Empty; //  "";
                        while ((linha = sr2.ReadLine()) != null)
                        {
                            //if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;//  Console.WriteLine(linha);
                        };
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        fs.Close();
                    }
                    fs.Close();
                }
                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static void CMSTesteFileReader05(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();//  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                stopwatch.Reset();
                stopwatch.Start();

                int _bufferSize = 16384;

                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read)) //, FileShare.Read, bufferSize: 4096, useAsync: true))
                {
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    StringBuilder stringBuilder = new StringBuilder();
                    string linha = String.Empty; //  "";
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        char[] fileContents = new char[_bufferSize];
                        int charsRead = streamReader.Read(fileContents, 0, _bufferSize);
                        while (charsRead > 0)
                        {
                            //string text = Encoding.Unicode.GetString(fileContents, 0, _bufferSize);
                            stringBuilder.Append(fileContents);
                            charsRead = streamReader.Read(fileContents, 0, _bufferSize);
                        }
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        linha = stringBuilder.ToString();
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        streamReader.Close();
                    }
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    fileStream.Close();
                }

                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static void CMSTesteFileReader06(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();//  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                stopwatch.Reset();
                stopwatch.Start();

                using (FileStream fs = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    using (BufferedStream bs = new BufferedStream(fs)) // BufferedStream(fs, System.Text.ASCIIEncoding.Unicode.GetByteCount(g)))
                    {
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        using (StreamReader sr = new StreamReader(bs))
                        {
                            string linha = String.Empty; //  ""; // sr.ReadToEnd();
                            while ((linha = sr.ReadLine()) != null)
                            {
                                // linha
                            }
                            if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                            sr.Close();
                        }
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        bs.Close();
                    }
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    fs.Close();
                }

                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static void CMSTesteFileReader07(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();//  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                stopwatch.Reset();
                stopwatch.Start();

                using (MemoryMappedFile file = MemoryMappedFile.CreateFromFile(path))
                {
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    using (MemoryMappedViewStream stream = file.CreateViewStream())
                    {
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        while (true)
                        {
                            int result = stream.ReadByte();   // Read in byte from the MemoryMappedFile.
                            if (result == 0) // Zero bytes are past the end of the file.
                                break;
                            // Print file data to the console.
                            //Console.WriteLine("NUMBER: " + result);
                            // char letter = (char)result;
                            //Console.WriteLine("LETTER: " + letter);
                        }
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        stream.Close();
                    }
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                }

                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static void CMSTesteFileReader08(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();//  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                stopwatch.Reset();
                stopwatch.Start();
                using (FileStream file = System.IO.File.Open(path, FileMode.Open))
                {
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    using (BinaryReader reader = new BinaryReader(file))
                    {
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        int count = reader.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            string u = reader.ReadString();
                            int len = reader.ReadInt32();
                            byte[] b = reader.ReadBytes(len);
                        }
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        reader.Close();
                    }
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    file.Close();
                }
                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static void CMSTesteFileReader09(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();//  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                stopwatch.Reset();
                stopwatch.Start();

                using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(path)))
                {
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        int count = reader.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            string u = reader.ReadString();
                            int len = reader.ReadInt32();
                            byte[] b = reader.ReadBytes(len);
                        }
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        reader.Close();
                    }
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    stream.Close();
                }

                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static void CMSTesteFileReader10(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers(); //  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                stopwatch.Reset();
                stopwatch.Start();

                using (MemoryMappedFile file = MemoryMappedFile.CreateFromFile(path))
                {
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    using (MemoryMappedViewStream stream = file.CreateViewStream())
                    {
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                            int count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                string u = reader.ReadString();
                                int len = reader.ReadInt32();
                                byte[] b = reader.ReadBytes(len);
                            }
                            if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                            reader.Close();
                        }
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                        stream.Close();
                    }
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                }

                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static void CMSTesteFileReader11(string path)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers(); //  Thread.Sleep(1000);     //give disk hardware time to recover
            var stopwatch = new Stopwatch();
            MethodBase method = MethodBase.GetCurrentMethod();
            long memoryUsedIni = Process.GetCurrentProcess().PrivateMemorySize64;
            long memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
            try
            {

                stopwatch.Reset();
                stopwatch.Start();

                using (var fileStream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    using (var stream = new MemoryStream())
                    {
                        fileStream.CopyTo(stream);
                        stream.Seek(0, SeekOrigin.Begin); // stream.Position = 0;
                        using (var streamReader = new StreamReader(stream))
                        {
                            if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                            while (streamReader.Peek() > 0)
                            {
                                var linha = streamReader.ReadLine().AsSpan();
                                var tipoRegistro = linha.Slice(7, 1);
                                var segmentoRegistro = linha.Slice(13, 1);
                                // tipoRegistro.ToString()

                            }
                            if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                            streamReader.Close();
                        }
                        stream.Close();
                        if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    }
                    if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                    fileStream.Close();
                }

                stopwatch.Stop();

                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;

            }
            catch (Exception ex)
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                if (Process.GetCurrentProcess().PrivateMemorySize64 > memoryUsedFim) memoryUsedFim = Process.GetCurrentProcess().PrivateMemorySize64;
                Console.WriteLine($"{stopwatch.Elapsed}: {method.ReflectedType.Name}.{method.Name} - MemoryIni: {GetMegaByte(memoryUsedIni)} - MemoryFim: {GetMegaByte(memoryUsedFim)}");
            }
        }

        static string GetMegaByte(long numBytes)
        {
            return ((numBytes / 1024f) / 1024f).ToString("0.00");
        }

        static bool IsFileReady(string filename)
        {
            try
            {
                using (FileStream inputStream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        static void WaitForFile(string filename)
        {
            while (!IsFileReady(filename)) { }
        }

    }
}
