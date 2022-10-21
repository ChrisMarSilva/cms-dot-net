using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var ArquivoConfiguracao = @"D:\CMS Proj Teste\CMS DotNet\CMS DotNet File\ConsoleApp1\bin\Debug\JDConexao.txt";

            var parser  = new FileIniDataParser(); // INIFileParser
            var iniFile = parser.ReadFile(ArquivoConfiguracao, Encoding.Default);

            Console.WriteLine("mmm");
            Console.ReadKey();
        }
    }
}
