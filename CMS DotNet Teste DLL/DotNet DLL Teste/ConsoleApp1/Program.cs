using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {

        private const string _dllLocation = "JDSPB_IntegraLegado.dll";

        // https://learn.microsoft.com/pt-br/dotnet/framework/interop/default-marshalling-for-strings


        /*
          [DllImport(_dllLocation, EntryPoint = "EnviaMensagemURL", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, CharSet = CharSet.Ansi)]		
        public static extern int EnviaMensagemURL(int ISPB,
                                                String TpAmbiente,
                                                String DominioURL,
                                                String CdLegado,
                                                String CdUsuario,
                                                String Senha,
                                                int ISPBOrigem,
                                                int ISPBDestino,
                                                String ConteudoXML,
                                                StringBuilder NumCabSeq,
                                                StringBuilder CdRetorno,
                                                StringBuilder DscRetorno);

        [DllImport(_dllLocation, EntryPoint = "RecebeMensagemURL", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, CharSet = CharSet.Ansi)]
        public static extern int RecebeMensagemURL(int ISPB,
                                                    String TpAmbiente,
                                                    String DominioURL,
                                                    String CdLegado,
                                                    String CdUsuario,
                                                    String Senha,
                                                    StringBuilder NumCabSeq,
                                                    [MarshalAs(UnmanagedType.LPArray)] Int32[] ISPBOrigem,
                                                    [MarshalAs(UnmanagedType.LPArray)] Int32[] ISPBDestino,
                                                    StringBuilder NumCtrlIF,
                                                    StringBuilder CdMensagem,
                                                    StringBuilder DtHrRegistro,
                                                    StringBuilder TpRetorno,
                                                    StringBuilder ConteudoXML,
                                                    StringBuilder CdRetorno,
                                                    StringBuilder DscRetorno);

        [DllImport(_dllLocation, EntryPoint = "EnviaMensagem", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, CharSet = CharSet.Ansi)]
        public static extern int EnviaMensagem(int ISPB,
                                                String TpAmbiente,
                                                String CdLegado,
                                                String CdUsuario,
                                                String Senha,
                                                int ISPBOrigem,
                                                int ISPBDestino,
                                                String ConteudoXML,
                                                StringBuilder NumCabSeq,
                                                StringBuilder CdRetorno,
                                                StringBuilder DscRetorno);

        [DllImport(_dllLocation, EntryPoint = "RecebeMensagem", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, CharSet = CharSet.Ansi)]
        public static extern int RecebeMensagem(int ISPB,
                                                    String TpAmbiente,
                                                    String CdLegado,
                                                    String CdUsuario,
                                                    String Senha,
                                                    StringBuilder NumCabSeq,
                                                    [MarshalAs(UnmanagedType.LPArray)] Int32[] ISPBOrigem,
                                                    [MarshalAs(UnmanagedType.LPArray)] Int32[] ISPBDestino,
                                                    StringBuilder NumCtrlIF,
                                                    StringBuilder CdMensagem,
                                                    StringBuilder DtHrRegistro,
                                                    StringBuilder TpRetorno,
                                                    StringBuilder ConteudoXML,
                                                    StringBuilder CdRetorno,
                                                    StringBuilder DscRetorno);

         */

        ////[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        ////[DllImport("ProjTesteDelphiDLL.dll", EntryPoint = "EnviaMensagemURL", ExactSpelling = false, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
        //[DllImport(_dllLocation, EntryPoint = "EnviaMensagemURL",  ExactSpelling = false, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        //public static extern int EnviaMensagemURL(
        //      int ISPB
        //    , [MarshalAs(UnmanagedType.LPStr)] string TpAmbiente
        //    , [MarshalAs(UnmanagedType.LPStr)] string DominioURL
        //    , [MarshalAs(UnmanagedType.LPStr)] string CdLegado
        //    , [MarshalAs(UnmanagedType.LPStr)] string CdUsuario
        //    , [MarshalAs(UnmanagedType.LPStr)] string Senha
        //    , int ISPBOrigem
        //    , int ISPBDestino
        //    , [MarshalAs(UnmanagedType.LPStr)] string ConteudoXML
        //    , [MarshalAs(UnmanagedType.LPStr)] ref string NumCabSeq  //,   // out // ref
        //    , [MarshalAs(UnmanagedType.LPStr)] ref string CdRetorno 
        //    , [MarshalAs(UnmanagedType.LPStr)] ref string DscRetorno 
        //  );


        [DllImport(_dllLocation, EntryPoint = "EnviaMensagemURL", CallingConvention = CallingConvention.StdCall, ExactSpelling = false, CharSet = CharSet.Ansi)]
        public static extern int EnviaMensagemURL(int ISPB,
                                       String TpAmbiente,
                                       String DominioURL,
                                       String CdLegado,
                                       String CdUsuario,
                                       String Senha,
                                       int ISPBOrigem,
                                       int ISPBDestino,
                                       String ConteudoXML,
                                       StringBuilder NumCabSeq,
                                       StringBuilder CdRetorno,
                                       StringBuilder DscRetorno);

        static void Main(string[] args)
        {
            Console.WriteLine("Inicio...");
            Console.WriteLine("");
            try
			{

                int ispb = (int)04358798;
                string tpAmbiente = "HOML";
                string dominioURL = "http://localhost/JDSPB/JDCabine/dll/JDSPB_WS_IntegraLegadoQM.dll/soap/IJDSPBCAB"; 
                string cdLegado = "INT";
                string cdUsuario = "PILOTO";
                string senha = "123";
                int ispbOrigem = (int)04358798;
                int ispbDestino = (int)00038166;
                string conteudoXML = "<SISMSG><SEGCAB><CD_LEGADO>INT</CD_LEGADO><TP_MANUT>I</TP_MANUT><CD_STATUS>D</CD_STATUS><NR_OPERACAO>000001</NR_OPERACAO><FL_DEB_CRED>D</FL_DB_CRED_FIN></SEGCAB><STR0008><CodMsg>STR0008</CodMsg><NumCtrlIF>JDCCMM000001</NumCtrlIF><ISPBIFDebtd>04358798</ISPBIFDebtd><AgDebtd>1324</AgDebtd><TpCtDebtd>CC</TpCtDebtd><CtDebtd>322</CtDebtd><TpPessoaDebtd>F</TpPessoaDebtd><CNPJ_CPFCliDebtd>11111111111</CNPJ_CPFCliDebtd><NomCliDebtd>deb</NomCliDebtd><ISPBIFCredtd>92874270</ISPBIFCredtd><AgCredtd>555</AgCredtd><TpCtCredtd>CC</TpCtCredtd><CtPgtoCredtd>666</CtPgtoCredtd><TpPessoaCredtd>F</TpPessoaCredtd><CNPJ_CPFCliCredtd>22222222222</CNPJ_CPFCliCredtd><NomCliCredtd>credito</NomCliCredtd><VlrLanc>0.01</VlrLanc><FinlddCli>201</FinlddCli><DtMovto>2020-02-13</DtMovto></STR0008></SISMSG>";

                const int bufferSize = 1024;
                //var numCabSeq = new String('\x00', bufferSize);
                //var cdRetorno = new String('\x00', bufferSize);
                //var dscRetorno = new String('\x00', bufferSize);

                // new string('\x00', bufferSize);  
                // new string('\0', bufferSize);
                // 
                // new String('\x00', bufferSize);
                // new String('\0', bufferSize);

                // new StringBuilder();
                // new StringBuilder(bufferSize);

                var numCabSeq = new StringBuilder(bufferSize);
                var cdRetorno = new StringBuilder(bufferSize);
                var dscRetorno = new StringBuilder(bufferSize);

                //const int bufferSize = 1024;
                //var str1 = new IntPtr(bufferSize);
                //var str2 = new IntPtr(bufferSize);
                //var str3 = new IntPtr(bufferSize);
                //var numCabSeq = Marshal.PtrToStringAnsi(str1);
                //var cdRetorno = Marshal.PtrToStringAnsi(str2);
                //var dscRetorno = Marshal.PtrToStringAnsi(str3);

                //var retorno = EnviaMensagemURL(
                //    ISPB: ispb,
                //    TpAmbiente: tpAmbiente,
                //    DominioURL: dominioURL,
                //    CdLegado: cdLegado,
                //    CdUsuario: cdUsuario,
                //    Senha: senha,
                //    ISPBOrigem: ispbOrigem,
                //    ISPBDestino: ispbDestino,
                //    ConteudoXML: conteudoXML,
                //    NumCabSeq: ref numCabSeq,
                //    CdRetorno: ref cdRetorno,
                //    DscRetorno: ref dscRetorno);

                var retorno = EnviaMensagemURL(
                    ISPB: ispb,
                    TpAmbiente: tpAmbiente,
                    DominioURL: dominioURL,
                    CdLegado: cdLegado,
                    CdUsuario: cdUsuario,
                    Senha: senha,
                    ISPBOrigem: ispbOrigem,
                    ISPBDestino: ispbDestino,
                    ConteudoXML: conteudoXML,
                    NumCabSeq: numCabSeq,
                    CdRetorno: cdRetorno,
                    DscRetorno: dscRetorno);

                // System.AccessViolationException 'Tentativa de leitura ou gravação em memória protegida. Normalmente, isso é uma indicação de que outra memória está danificada.'

                Console.WriteLine($"Retorno: {retorno}");
                Console.WriteLine("");

                Console.WriteLine($"NumCabSeq: {numCabSeq?.ToString()} ");
                Console.WriteLine($"CdRetorno: {cdRetorno?.ToString()} ");
                Console.WriteLine($"DscRetorno: {dscRetorno?.ToString()} ");

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
