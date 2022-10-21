using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        //[DllImport("JDSPB_IntegraLegado.dll", CallingConvention = CallingConvention.StdCall)]
        //public static extern int EnviaMensagemCMS(
        //    int ISPB,
        //    string TpAmbiente,
        //    string CdLegado,
        //    string CdUsuario,
        //    string Senha,
        //    int ISPBOrigem,
        //    int ISPBDestino,
        //    string ConteudoXML,
        //    ref string NumCabSeq,
        //    ref string CdRetorno,
        //    ref string DscRetorno
        //  );

        //unsafe
        // EnviaMensagemCMS
        // EnviaMensagem
        // EnviaMensagemURL
        [DllImport("JDSPB_IntegraLegado.dll", EntryPoint = "EnviaMensagem", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int EnviaMensagemURL(
            int ISPB,
            string TpAmbiente,
            string DominioURL,
            string CdLegado,
            string CdUsuario,
            string Senha,
            int ISPBOrigem,
            int ISPBDestino,
            string ConteudoXML,
            ref string NumCabSeq,
            ref string CdRetorno,
            ref string DscRetorno
          );

        static void Main(string[] args)
        {
            Console.WriteLine("Inicio...");
            Console.WriteLine("");
            try
			{

                int ISPB           = (int)04358798;
                string TpAmbiente  = "HOML";
                string DominioURL  = "http://localhost/JDSPB/JDCabine/dll/JDSPB_WS_IntegraLegado.dll/soap/IJDSPBCAB";
                string CdLegado    = "INT";
                string CdUsuario   = "PILOTO";
                string Senha       = "123";
                int ISPBOrigem     = (int)04358798;
                int ISPBDestino    = (int)00038166;
                string ConteudoXML = "<SISMSG><SEGCAB><CD_LEGADO>INT</CD_LEGADO><TP_MANUT>I</TP_MANUT><CD_STATUS>D</CD_STATUS><NR_OPERACAO>000001</NR_OPERACAO><FL_DEB_CRED>D</FL_DB_CRED_FIN></SEGCAB><STR0008><CodMsg>STR0008</CodMsg><NumCtrlIF>JDCCMM000001</NumCtrlIF><ISPBIFDebtd>04358798</ISPBIFDebtd><AgDebtd>1324</AgDebtd><TpCtDebtd>CC</TpCtDebtd><CtDebtd>322</CtDebtd><TpPessoaDebtd>F</TpPessoaDebtd><CNPJ_CPFCliDebtd>11111111111</CNPJ_CPFCliDebtd><NomCliDebtd>deb</NomCliDebtd><ISPBIFCredtd>92874270</ISPBIFCredtd><AgCredtd>555</AgCredtd><TpCtCredtd>CC</TpCtCredtd><CtPgtoCredtd>666</CtPgtoCredtd><TpPessoaCredtd>F</TpPessoaCredtd><CNPJ_CPFCliCredtd>22222222222</CNPJ_CPFCliCredtd><NomCliCredtd>credito</NomCliCredtd><VlrLanc>0.01</VlrLanc><FinlddCli>201</FinlddCli><DtMovto>2020-02-13</DtMovto></STR0008></SISMSG>";

                const int stringBufferSize = 1024;
                var NumCabSeq  = new String('\x00', stringBufferSize); 
                var CdRetorno  = new String('\x00', stringBufferSize);
                var DscRetorno = new String('\x00', stringBufferSize);

                var retorno = EnviaMensagemURL(ISPB, TpAmbiente, DominioURL, CdLegado, CdUsuario, Senha, ISPBOrigem, ISPBDestino, ConteudoXML, ref NumCabSeq, ref CdRetorno, ref DscRetorno);

                Console.WriteLine($"Retorno: {retorno.ToString()}");
                Console.WriteLine("");

                Console.WriteLine($"NumCabSeq: {NumCabSeq} ");
                Console.WriteLine($"CdRetorno: {CdRetorno} ");
                Console.WriteLine($"DscRetorno: {DscRetorno} ");

            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine($"Erro: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("");
                Console.WriteLine("Fim...");
                Console.ReadKey();
            }
        }

    }
}
