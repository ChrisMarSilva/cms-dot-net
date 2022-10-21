using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using System.Text;

namespace ConsoleApp1
{
    class JDSPBPasta
    {
        public string PastaOrigem { get; set; }
        public string PastaDestino { get; set; }

        public JDSPBPasta(string pastaOrigem, string pastaDestino)
        {
            this.PastaOrigem = pastaOrigem;
            this.PastaDestino = pastaDestino;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            var ArquivoConfiguracao = @"D:\CMS Proj Teste\CMS DotNet\CMS DotNet File\ConsoleApp1\bin\Debug\JDConexao.txt";

            //if (!File.Exists(ArquivoConfiguracao)) return false;
            var parser = new FileIniDataParser();
            IniData iniFile = parser.ReadFile(ArquivoConfiguracao); //parser.ReadFile(ArquivoConfiguracao, Encoding.Default);
            var srv01 = iniFile["JDSPBParams"]["PARAMETRO"];
            var srv02 = iniFile["JDSPBParams"]["PARAMETRO 01"];
            Console.WriteLine($"srv01: {srv01}");
            Console.WriteLine($"srv02: {srv02}");

            //foreach (SectionData section in iniFile.Sections)
            //{
            //    Console.WriteLine("[" + section.SectionName + "]");
            //    foreach (KeyData key in section.Keys)
            //        Console.WriteLine(key.KeyName + " = " + key.Value);
            //    Console.WriteLine("");
            //}

            //iniFile["UI"]["fullscreen"] = "true";

            //iniFile.Sections.AddSection("newSection");
            //iniFile["Remote"].AddKey("FTPUsername", "anonymous");

            //iniFile["newSection"].RemoveKey("newKey2");
            //iniFile.Sections.RemoveSection("Users");

            //parser.WriteFile(ArquivoConfiguracao, iniFile);

            //var listaPasta = new List<JDSPBPasta>();

            //PASTA DO JSSPB
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JD SPB\Bibliotecas", @"D:\JD Midias\Fontes N4 20191111\JD SPB\Bibliotecas") );
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JD SPB\Cabine Acesso", @"D:\JD Midias\Fontes N4 20191111\JD SPB\cabine acesso") );
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JD SPB\Cabine Atualiza Catalogo", @"D:\JD Midias\Fontes N4 20191111\JD SPB\cabine atualiza catalogo"));
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JD SPB\Cabine Compulsorio", @"D:\JD Midias\Fontes N4 20191111\JD SPB\cabine compulsorio"));
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JD SPB\Cabine Configuracao", @"D:\JD Midias\Fontes N4 20191111\JD SPB\cabine configuracao"));
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JD SPB\Cabine Integração Citibank", @"D:\JD Midias\Fontes N4 20191111\JD SPB\cabine integração citibank"));
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JD SPB\Cabine Interface Servico", @"D:\JD Midias\Fontes N4 20191111\JD SPB\cabine interface servico"));
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JD SPB\Cabine Monitor", @"D:\JD Midias\Fontes N4 20191111\JD SPB\cabine monitor"));
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JD SPB\Cabine Parametros", @"D:\JD Midias\Fontes N4 20191111\JD SPB\cabine parametros"));
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JD SPB\JDPadrao", @"D:\JD Midias\Fontes N4 20191111\JD SPB\jdpadrao"));
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JD SPB\Padroes", @"D:\JD Midias\Fontes N4 20191111\JD SPB\padroes"));

            //PASTA DO JDCOMPONENTE
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JDComponentes\06.Implementacao\01.Delphi", @"D:\JD Midias\Fontes N4 20191111\JDComponentes\06.Implementacao\01.Delphi") );
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JDComponentes\06.Implementacao\03.Terceiros\Delphi2007", @"D:\JD Midias\Fontes N4 20191111\JDComponentes\06.Implementacao\03.Terceiros\Delphi2007") );
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JDComponentes\06.Implementacao\03.Terceiros\DelphiXE",@"D:\JD Midias\Fontes N4 20191111\JDComponentes\06.Implementacao\03.Terceiros\DelphiXE") );
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JDComponentes\06.Implementacao\03.Terceiros\DelphiXE10", @"D:\JD Midias\Fontes N4 20191111\JDComponentes\06.Implementacao\03.Terceiros\DelphiXE10") );
            //listaPasta.Add(new JDSPBPasta(@"D:\ST_Produtos\JDComponentes\06.Implementacao\03.Terceiros\VCL Skin", @"D:\JD Midias\Fontes N4 20191111\JDComponentes\06.Implementacao\03.Terceiros\VCL Skin") );

            //var tasks = new Task[listaPasta.Count];
            //int index = -1;

            //foreach (JDSPBPasta item in listaPasta)
            //{
            //   index++;
            //   tasks[index] = Task.Run(() => {
            //        string pastaOrigem  = item.PastaOrigem;
            //        string pastaDestino = item.PastaDestino;
            //        DirectoryCopy(pastaOrigem, pastaDestino);
            //       //Console.WriteLine($"Movendo Pasta: {pastaOrigem}");
            //   });
            //}

            //Task.WaitAll(tasks);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }


        public static void DirectoryCopy(string pastaOrigem, string pastaDestino)
        {
            try
            {

                if (Directory.Exists(pastaDestino) == false)
                {
                    Directory.CreateDirectory(pastaDestino);
                }

                DirectoryInfo dirInfo = new DirectoryInfo(pastaOrigem);
                DirectoryInfo[] directories = dirInfo.GetDirectories();
                FileInfo[] files = dirInfo.GetFiles();

                foreach (DirectoryInfo tempdir in directories)
                {
                    Directory.CreateDirectory(pastaDestino + "/" + tempdir.Name);
                    var ext = System.IO.Path.GetExtension(tempdir.Name);
                    if (System.IO.Path.HasExtension(ext))
                    {
                        foreach (FileInfo tempfile in files)
                        {
                            tempfile.CopyTo(Path.Combine(pastaOrigem + "/" + tempfile.Name, pastaDestino + "/" + tempfile.Name), true);

                        }
                    }
                    DirectoryCopy(pastaOrigem + "/" + tempdir.Name, pastaDestino + "/" + tempdir.Name);
                }

                FileInfo[] files1 = dirInfo.GetFiles();
                foreach (FileInfo tempfile in files1)
                {
                    tempfile.CopyTo(Path.Combine(pastaDestino, tempfile.Name), true);
                }

            }
            catch (Exception)
            {

                //throw;
            }
        }

    }
}
