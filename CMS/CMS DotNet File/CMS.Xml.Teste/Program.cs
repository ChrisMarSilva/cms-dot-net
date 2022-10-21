using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CMS.Xml.Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            var exibir = false;
            var stopwatch = new Stopwatch();
            var pathXML = @"D:\CMS Projetos DotNet\CMS DotNet Teste Entity Performance\CMS.EF.Performance.STR0014\bin\Debug\STR0014R1.xml";
            MSgSTR0014R1 msgSTR0014R1 = null;
            var serializer = new XmlSerializer(typeof(MSgSTR0014R1));

            //--------------------------------------------------------------------------------  
            //--------------------------------------------------------------------------------  

            stopwatch.Reset();
            stopwatch.Start();
            using (XmlReader reader = XmlReader.Create(pathXML))
            {
                //reader.MoveToContent();
                while (reader.Read())
                {
                    //switch (reader.NodeType)
                    //{
                    //    case XmlNodeType.Element:
                    //        Console.Write("<" + reader.Name+">");
                    //        break;
                    //    case XmlNodeType.Text: //Display the text in each element.
                    //        Console.Write(reader.Value);
                    //        break;
                    //    case XmlNodeType.EndElement: //Display the end of the element.
                    //        Console.WriteLine("</" + reader.Name+">");
                    //        break;
                    //}

                    // if (reader.IsStartElement())
                    // {
                    //switch (reader.Name.ToString())
                    //{
                    //    case "Name":
                    //        Console.WriteLine("Name of the Element is : " + reader.ReadString());
                    //        break;
                    //    case "Location":
                    //        Console.WriteLine("Your Location is : " + reader.ReadString());
                    //        break;
                    //}
                    //}
                    //Console.WriteLine("");
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed} => XmlReader");

            //--------------------------------------------------------------------------------  
            //--------------------------------------------------------------------------------  

            stopwatch.Reset();
            stopwatch.Start();
            using (StreamReader sr = new StreamReader(pathXML)) // using (Stream sr = new FileStream(pathXML, FileMode.Open))
            {
                msgSTR0014R1 = (MSgSTR0014R1)serializer.Deserialize(sr);
                sr.Close();
            };
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed} => StreamReader");

            //--------------------------------------------------------------------------------  
            //--------------------------------------------------------------------------------  

            stopwatch.Reset();
            stopwatch.Start();
            using (FileStream fs = new FileStream(pathXML, FileMode.Open, FileAccess.Read))
            {
                using (TextReader sr = new StreamReader(fs))
                {
                    msgSTR0014R1 = (MSgSTR0014R1)serializer.Deserialize(sr);
                    sr.Close();
                };
                fs.Close();
            };
            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed} => FileStream");

            //--------------------------------------------------------------------------------  
            //--------------------------------------------------------------------------------  

            stopwatch.Reset();
            stopwatch.Start();

            if (exibir)
            {
                Console.WriteLine("");
                Console.WriteLine($"STR0014R1.CodMsg: {msgSTR0014R1.CodMsg}");
                Console.WriteLine($"STR0014R1.NumCtrlIF: {msgSTR0014R1.NumCtrlIF}");
                Console.WriteLine($"STR0014R1.ISPBIF: {msgSTR0014R1.ISPBIF}");
                Console.WriteLine($"STR0014R1.DtHrIni: {msgSTR0014R1.DtHrIni}");
                Console.WriteLine($"STR0014R1.SldInial: {msgSTR0014R1.SldInial}");
            }

            if (msgSTR0014R1.Grupos != null)
            {
                foreach (var grupo in msgSTR0014R1.Grupos)
                {
                    if (exibir)
                    {
                        Console.WriteLine("");
                        Console.WriteLine($"STR0014R1.Grupo_STR0014R1_Lanc.CodMsgOr: {grupo.CodMsgOr}");
                        Console.WriteLine($"STR0014R1.Grupo_STR0014R1_Lanc.NumCtrlIFOr: {grupo.NumCtrlIFOr}");
                        Console.WriteLine($"STR0014R1.Grupo_STR0014R1_Lanc.NumCtrlSTROr: {grupo.NumCtrlSTROr}");
                        Console.WriteLine($"STR0014R1.Grupo_STR0014R1_Lanc.DtHrSit: {grupo.DtHrSit}");
                        Console.WriteLine($"STR0014R1.Grupo_STR0014R1_Lanc.TpDeb_Cred: {grupo.TpDeb_Cred}");
                        Console.WriteLine($"STR0014R1.Grupo_STR0014R1_Lanc.VlrLanc: {grupo.VlrLanc}");
                    }
                }
            }

            if (exibir)
            {
                Console.WriteLine("");
                Console.WriteLine($"STR0014R1.SldFinl: {msgSTR0014R1.SldFinl}");
                Console.WriteLine($"STR0014R1.DtHrBC: {msgSTR0014R1.DtHrBC}");
                Console.WriteLine($"STR0014R1.DtMovto: {msgSTR0014R1.DtMovto}");
            }

            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed} => STR0014R1");

            //--------------------------------------------------------------------------------  
            //--------------------------------------------------------------------------------  

            Console.WriteLine("");
            Console.ReadKey();
        }

        [Serializable]
        [XmlType("STR0014R1")]
        //[XmlRoot(ElementName = "STR0014R1")]
        public class MSgSTR0014R1
        {

            [XmlElement("CodMsg")]
            public string CodMsg { get; set; }

            [XmlElement("NumCtrlIF")]
            public string NumCtrlIF { get; set; }

            [XmlElement("ISPBIF")]
            public string ISPBIF { get; set; }

            [XmlElement("DtHrIni")]
            public string DtHrIni { get; set; }

            [XmlElement("SldInial")]
            public string SldInial { get; set; }

            [XmlElement("Grupo_STR0014R1_Lanc")]
            public List<MsgSTR0014R1Grupo> Grupos { get; set; }

            [XmlElement("SldFinl")]
            public string SldFinl { get; set; }

            [XmlElement("DtHrBC")]
            public string DtHrBC { get; set; }

            [XmlElement("DtMovto")]
            public string DtMovto { get; set; }

            public MSgSTR0014R1()
            {
                Grupos = new List<MsgSTR0014R1Grupo>();
            }
        }

        [Serializable]
        [XmlType("Grupo_STR0014R1_Lanc")]
        public class MsgSTR0014R1Grupo
        {

            [XmlElement("CodMsgOr")]
            public string CodMsgOr { get; set; }

            [XmlElement("NumCtrlIFOr")]
            public string NumCtrlIFOr { get; set; }

            [XmlElement("NumCtrlSTROr")]
            public string NumCtrlSTROr { get; set; }

            [XmlElement("DtHrSit")]
            public string DtHrSit { get; set; }

            [XmlElement("TpDeb_Cred")]
            public string TpDeb_Cred { get; set; }

            [XmlElement("VlrLanc")]
            public string VlrLanc { get; set; }

            public MsgSTR0014R1Grupo()
            {
            }
        }

    }
}
