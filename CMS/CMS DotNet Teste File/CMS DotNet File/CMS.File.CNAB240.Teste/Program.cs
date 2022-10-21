using CMS.EF.Performance.Console.CNAB240.Entity;
using FileHelpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Z.BulkOperations;

namespace CMS.EF.Performance.CNAB240
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Inicio");
            try
            {

                //var pathDir = @"D:\CMS Projetos DotNet\CMS DotNet Teste Entity Performance\Docs\"; //Note   
                var pathDir = @"D:\CMS Proj Teste\CMS DotNet\CMS DotNet Teste Entity Performance\Docs\"; //JD
                var pathFile = pathDir + "PAGSEGURO_20191105_999_REM.txt";  //"PAGSEGURO_20191105_001_REM.txt";  // "PAGSEGURO_20191105_999_REM.txt"; 
                var stopwatch = new Stopwatch();
                var qtdeLinha = 0;

                // TesteWriterCNAB240(pathFile, 1000000);
                // TestFileHelpersCNAB240(pathDir);      
                //var listRegistros = new List<JDRegistro>();

                var DbConnJDSPB = @"Data Source=JDSP108;Initial Catalog=JDSPB;User ID=jddesenv;Password=jddesenv;Persist Security Info=True;";
                // var DbConnJD = @"Data Source=JDSP108;Initial Catalog=CMS_DOTNET;User ID=jddesenv;Password=jddesenv;Persist Security Info=True;";
                // var DbConnNote = @"Data Source=CMS-NOTE\SQLEXPRESS;Initial Catalog=CMS_DOTNET;User ID=sa;Password=sa;Persist Security Info=True;";

                // if (!string.IsNullOrEmpty(DbConnJDSPB))
                using (var conn = new SqlConnection(DbConnJDSPB)) // DbConnJDSPB // DbConnJD //DbConnNote
                {

                    conn.Open();

                    #region SqlCommandDeletes
                    SqlCommand commandDel01 = new SqlCommand("DELETE FROM TBJDSPBCAB_CNAB240_REGISTRO", conn);
                    commandDel01.ExecuteNonQuery();
                    SqlCommand commandDel02 = new SqlCommand("DELETE FROM TBJDSPBCAB_CNAB240_ARQUIVO", conn);
                    commandDel02.ExecuteNonQuery();
                    #endregion

                    #region DbContextDeletes
                    // var context = new BancoDeDadosContext();
                    //context.Database.ExecuteSqlCommand("DELETE FROM TBJDSPBCAB_CNAB240_REGISTRO");
                    //context.Database.ExecuteSqlCommand("DELETE FROM TBJDSPBCAB_CNAB240_ARQUIVO");
                    //context.SaveChanges();
                    #endregion


                    #region FileHeppersReadFile
                    //var engine = new MultiRecordEngine(typeof(RegistroHeaderArquivo), typeof(RegistroHeaderLote), typeof(RegistroDetalheSegmentoA), typeof(RegistroDetalheSegmentoB), typeof(RegistroTrailerLote), typeof(RegistroTrailerArquivo));
                    //engine.RecordSelector = new RecordTypeSelector(CustomSelector);

                    //stopwatch.Reset();
                    //stopwatch.Start();
                    //var result = engine.ReadFile(pathFile);
                    //object[] result = engine.ReadFile(pathFile);
                    //engine.BeginReadFile(pathFile);
                    //stopwatch.Stop();
                    //System.Console.WriteLine($"{stopwatch.Elapsed} => ReadFile( qtdeLinha: " + qtdeLinha.ToString() + " )");
                    #endregion

                    #region DbContextSelArquivos
                    //var arquivos = context.Arquivos.ToList();
                    //foreach ( var item in arquivos)
                    //    System.Console.WriteLine($" => {item.Id.ToString()} - {item.Nome}");
                    #endregion

                    #region objArquivo
                    var objArquivo = new JDArquivo();
                    objArquivo.Id = 100;
                    objArquivo.IdRet = 123;
                    objArquivo.CodLegado = "PAGFOR";
                    objArquivo.Nome = Path.GetFileName(pathFile);
                    objArquivo.Tipo = "REM";
                    objArquivo.Data = DateTime.Now.ToString("yyyyMMdd");
                    objArquivo.Seq = 100;
                    objArquivo.DthrGeracao = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    objArquivo.DthrRegistro = decimal.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    objArquivo.QtdeProc = 0;
                    objArquivo.Situacao = "RR0"; // RR0	Arquivo REM Recebido com sucesso. Pendente carregar.
                    #endregion

                    #region DbContextInsArquivos
                    //context.Arquivos.Add(objArquivo);
                    //context.SaveChanges();
                    #endregion

                    #region SqlCommandInsArquivos
                    //SqlCommand commandInsArqv = new SqlCommand("INSERT INTO TBJDSPBCAB_CNAB240_ARQUIVO (IDARQV, IDARQVRET, CDLEGADO, NMARQV, TPARQV, DTARQV, SEQARQV, DTHRGERACAO, DTHRREGISTRO, QTDPROC, STARQV) VALUES(@IDARQV, @IDARQVRET, @CDLEGADO, @NMARQV, @TPARQV, @DTARQV, @SEQARQV, @DTHRGERACAO, @DTHRREGISTRO, @QTDPROC, @STARQV) ", connection); //transaction
                    //commandInsArqv.Parameters.Add("@IDARQV", SqlDbType.Decimal);
                    //commandInsArqv.Parameters.Add("@IDARQVRET", SqlDbType.Decimal);
                    //commandInsArqv.Parameters.Add("@CDLEGADO", SqlDbType.VarChar);
                    //commandInsArqv.Parameters.Add("@NMARQV", SqlDbType.VarChar);
                    //commandInsArqv.Parameters.Add("@TPARQV", SqlDbType.VarChar);
                    //commandInsArqv.Parameters.Add("@DTARQV", SqlDbType.VarChar);
                    //commandInsArqv.Parameters.Add("@SEQARQV", SqlDbType.Decimal);
                    //commandInsArqv.Parameters.Add("@DTHRGERACAO", SqlDbType.Decimal);
                    //commandInsArqv.Parameters.Add("@DTHRREGISTRO", SqlDbType.Decimal);
                    //commandInsArqv.Parameters.Add("@QTDPROC", SqlDbType.Decimal);
                    //commandInsArqv.Parameters.Add("@STARQV", SqlDbType.VarChar);
                    //commandInsArqv.Parameters["@IDARQV"].Value = objArquivo.Id; 
                    //commandInsArqv.Parameters["@IDARQVRET"].Value = objArquivo.IdRet; 
                    //commandInsArqv.Parameters["@CDLEGADO"].Value = objArquivo.CodLegado; 
                    //commandInsArqv.Parameters["@NMARQV"].Value = objArquivo.Nome; 
                    //commandInsArqv.Parameters["@TPARQV"].Value = objArquivo.Tipo; 
                    //commandInsArqv.Parameters["@DTARQV"].Value = objArquivo.Data; 
                    //commandInsArqv.Parameters["@SEQARQV"].Value = objArquivo.Seq; 
                    //commandInsArqv.Parameters["@DTHRGERACAO"].Value = objArquivo.DthrGeracao; 
                    //commandInsArqv.Parameters["@DTHRREGISTRO"].Value = objArquivo.DthrRegistro; 
                    //commandInsArqv.Parameters["@QTDPROC"].Value = objArquivo.QtdeProc; 
                    //commandInsArqv.Parameters["@STARQV"].Value = objArquivo.Situacao; 
                    //commandInsArqv.ExecuteNonQuery(); // Int32 rowsAffected = commandInsArqv.ExecuteNonQuery();
                    #endregion

                    #region DataTableInsRegistros
                    DataTable newProducts = new DataTable("TBJDSPBCAB_CNAB240_REGISTRO");

                    DataColumn productIDARQV = new DataColumn();
                    productIDARQV.DataType = System.Type.GetType("System.Decimal");
                    productIDARQV.ColumnName = "IDARQV";
                    newProducts.Columns.Add(productIDARQV);

                    DataColumn productSEQREG = new DataColumn();
                    productSEQREG.DataType = System.Type.GetType("System.Decimal");
                    productSEQREG.ColumnName = "SEQREG";
                    newProducts.Columns.Add(productSEQREG);

                    DataColumn productTPAREG = new DataColumn();
                    productTPAREG.DataType = System.Type.GetType("System.String");
                    productTPAREG.ColumnName = "TPAREG";
                    newProducts.Columns.Add(productTPAREG);

                    DataColumn productNUMCTRLIF = new DataColumn();
                    productNUMCTRLIF.DataType = System.Type.GetType("System.String");
                    productNUMCTRLIF.ColumnName = "NUMCTRLIF";
                    newProducts.Columns.Add(productNUMCTRLIF);

                    DataColumn productLINHA_SEGMENTO_A = new DataColumn();
                    productLINHA_SEGMENTO_A.DataType = System.Type.GetType("System.String");
                    productLINHA_SEGMENTO_A.ColumnName = "LINHA_SEGMENTO_A";
                    newProducts.Columns.Add(productLINHA_SEGMENTO_A);

                    DataColumn productLINHA_SEGMENTO_B = new DataColumn();
                    productLINHA_SEGMENTO_B.DataType = System.Type.GetType("System.String");
                    productLINHA_SEGMENTO_B.ColumnName = "LINHA_SEGMENTO_B";
                    newProducts.Columns.Add(productLINHA_SEGMENTO_B);

                    DataColumn productISPBIFCRED = new DataColumn();
                    productISPBIFCRED.DataType = System.Type.GetType("System.String");
                    productISPBIFCRED.ColumnName = "ISPBIFCRED";
                    newProducts.Columns.Add(productISPBIFCRED);

                    DataColumn productSTAREG = new DataColumn();
                    productSTAREG.DataType = System.Type.GetType("System.String");
                    productSTAREG.ColumnName = "STAREG";
                    newProducts.Columns.Add(productSTAREG);

                    DataColumn[] keys = null;
                    keys = new DataColumn[1];
                    keys[0] = productIDARQV;
                    newProducts.PrimaryKey = keys;

                    keys = new DataColumn[2];
                    keys[1] = productSEQREG;
                    newProducts.PrimaryKey = keys;
                    #endregion

                    //objArquivo.Situacao = "RR1"; // RR1	Arquivo REM Iniciado Carregamento.

                    #region DbContextAtuArquivos
                    //context.Entry(objArquivo).State = System.Data.Entity.EntityState.Modified;
                    //context.SaveChanges();
                    #endregion

                    //using (SqlCommand cmd = connection.CreateCommand())
                    //{
                    //    cmd.CommandText = @"UPDATE TBJDSPBCAB_CNAB240_ARQUIVO SET STARQV = @STARQV  WHERE IDARQV = @IDARQV";
                    //    cmd.Parameters.AddWithValue("STARQV", objArquivo.Situacao);
                    //    cmd.Parameters.AddWithValue("IDARQV", objArquivo.Id);
                    //    cmd.ExecuteNonQuery();
                    //}

                    qtdeLinha = 0;
                    var linhaSegA = "";
                    var linhaSegB = "";
                    var numCtrlIF = "";
                    var ispbIFCred = "";
                    var seq = 0;

                    //#region MemoryStream
                    //stopwatch.Reset();
                    //stopwatch.Start();
                    //using (var fileStream = File.Open(pathFile, FileMode.Open, FileAccess.Read, FileShare.None))
                    //{
                    //    var stream = new MemoryStream();
                    //    fileStream.CopyTo(stream); //await fileStream.CopyToAsync(stream);
                    //    stream.Seek(0, SeekOrigin.Begin);
                    //    using (var streamReader = new StreamReader(stream))
                    //    {
                    //        while (streamReader.Peek() > 0)
                    //        {
                    //            qtdeLinha++;
                    //            var linha = streamReader.ReadLine().AsSpan();
                    //            var tipoRegistro = linha.Slice(7, 1);
                    //            var segmentoRegistro = linha.Slice(13, 1);

                    //            if (tipoRegistro.ToString().Equals("0"))  // 0 - HeaderArquivo 
                    //            {
                    //                //System.Console.WriteLine($"tipoRegistro: {tipoRegistro.ToString()} - segmentoRegistro: {segmentoRegistro.ToString()} - linha: {linha.ToString()}");

                    //                #region objRegHeaderArquivo
                    //                seq++;
                    //                linhaSegA = linha.ToString();
                    //                linhaSegB = "";
                    //                numCtrlIF = "";
                    //                ispbIFCred = "";
                    //                var objRegistro = new JDRegistro();
                    //                objRegistro.Id = objArquivo.Id;
                    //                objRegistro.Seq = seq;
                    //                objRegistro.Tipo = "0";
                    //                objRegistro.NumCtrlIF = numCtrlIF;
                    //                objRegistro.LinhaSegA = linhaSegA;
                    //                objRegistro.LinhaSegB = linhaSegB;
                    //                objRegistro.ISPBIFCred = ispbIFCred;
                    //                objRegistro.Situacao = "01";
                    //                #endregion

                    //                #region SqlBulkCopyRowsAddInsRegistros
                    //                DataRow row = newProducts.NewRow();
                    //                row["IDARQV"] = objRegistro.Id;
                    //                row["SEQREG"] = objRegistro.Seq;
                    //                row["TPAREG"] = objRegistro.Tipo;
                    //                row["NUMCTRLIF"] = objRegistro.NumCtrlIF;
                    //                row["LINHA_SEGMENTO_A"] = objRegistro.LinhaSegA;
                    //                row["LINHA_SEGMENTO_B"] = objRegistro.LinhaSegB;
                    //                row["ISPBIFCRED"] = objRegistro.ISPBIFCred;
                    //                row["STAREG"] = objRegistro.Situacao;
                    //                newProducts.Rows.Add(row);
                    //                #endregion
                    //            }
                    //            else if (tipoRegistro.ToString().Equals("0")) // 1 - HeaderLote
                    //            {
                    //                // System.Console.WriteLine($"tipoRegistro: {tipoRegistro.ToString()} - segmentoRegistro: {segmentoRegistro.ToString()} - linha: {linha.ToString()}");

                    //                #region RegistroHeaderLote
                    //                seq++;
                    //                linhaSegA = linha.ToString();
                    //                linhaSegB = "";
                    //                numCtrlIF = "";
                    //                ispbIFCred = "";
                    //                var objRegistro = new JDRegistro();
                    //                objRegistro.Id = objArquivo.Id;
                    //                objRegistro.Seq = seq;
                    //                objRegistro.Tipo = "1";
                    //                objRegistro.NumCtrlIF = numCtrlIF;
                    //                objRegistro.LinhaSegA = linhaSegA;
                    //                objRegistro.LinhaSegB = linhaSegB;
                    //                objRegistro.ISPBIFCred = ispbIFCred;
                    //                objRegistro.Situacao = "01";
                    //                #endregion

                    //                #region SqlBulkCopyRowsAddInsRegistros
                    //                DataRow row = newProducts.NewRow();
                    //                row["IDARQV"] = objRegistro.Id;
                    //                row["SEQREG"] = objRegistro.Seq;
                    //                row["TPAREG"] = objRegistro.Tipo;
                    //                row["NUMCTRLIF"] = objRegistro.NumCtrlIF;
                    //                row["LINHA_SEGMENTO_A"] = objRegistro.LinhaSegA;
                    //                row["LINHA_SEGMENTO_B"] = objRegistro.LinhaSegB;
                    //                row["ISPBIFCRED"] = objRegistro.ISPBIFCred;
                    //                row["STAREG"] = objRegistro.Situacao;
                    //                newProducts.Rows.Add(row);
                    //                #endregion
                    //            }
                    //            else if (tipoRegistro.ToString().Equals("3") && segmentoRegistro.ToString().Equals("A")) // 3 - DetalheSegmentoA
                    //            {
                    //                //System.Console.WriteLine($"tipoRegistro: {tipoRegistro.ToString()} - segmentoRegistro: {segmentoRegistro.ToString()} - linha: {linha.ToString()}");

                    //                #region RegistroDetalheSegmentoA
                    //                linhaSegA = linha.ToString();
                    //                linhaSegB = "";
                    //                numCtrlIF = "";
                    //                ispbIFCred = "";
                    //                #endregion
                    //            }
                    //            else if (tipoRegistro.ToString().Equals("3") && segmentoRegistro.ToString().Equals("B")) // 3 - DetalheSegmentoA
                    //            {
                    //                //System.Console.WriteLine($"tipoRegistro: {tipoRegistro.ToString()} - segmentoRegistro: {segmentoRegistro.ToString()} - linha: {linha.ToString()}");

                    //                #region objRegDetalheSegB
                    //                seq++;
                    //                linhaSegB = linha.ToString();
                    //                var objRegistro = new JDRegistro();
                    //                objRegistro.Id = objArquivo.Id;
                    //                objRegistro.Seq = seq;
                    //                objRegistro.Tipo = "3";
                    //                objRegistro.NumCtrlIF = numCtrlIF;
                    //                objRegistro.LinhaSegA = linhaSegA;
                    //                objRegistro.LinhaSegB = linhaSegB;
                    //                objRegistro.ISPBIFCred = ispbIFCred;
                    //                objRegistro.Situacao = "01";
                    //                #endregion

                    //                #region SqlBulkCopyRowsAddInsRegistros
                    //                DataRow row = newProducts.NewRow();
                    //                row["IDARQV"] = objRegistro.Id;
                    //                row["SEQREG"] = objRegistro.Seq;
                    //                row["TPAREG"] = objRegistro.Tipo;
                    //                row["NUMCTRLIF"] = objRegistro.NumCtrlIF;
                    //                row["LINHA_SEGMENTO_A"] = objRegistro.LinhaSegA;
                    //                row["LINHA_SEGMENTO_B"] = objRegistro.LinhaSegB;
                    //                row["ISPBIFCRED"] = objRegistro.ISPBIFCred;
                    //                row["STAREG"] = objRegistro.Situacao;
                    //                newProducts.Rows.Add(row);
                    //                #endregion

                    //                linhaSegA = "";
                    //                linhaSegB = "";
                    //                numCtrlIF = "";
                    //                ispbIFCred = "";
                    //            }
                    //            else if (tipoRegistro.ToString().Equals("5")) // 5 - TrailerLote
                    //            {
                    //                //System.Console.WriteLine($"tipoRegistro: {tipoRegistro.ToString()} - segmentoRegistro: {segmentoRegistro.ToString()} - linha: {linha.ToString()}");

                    //                #region RegistroTrailerLote
                    //                seq++;
                    //                linhaSegA = linha.ToString();
                    //                linhaSegB = "";
                    //                numCtrlIF = "";
                    //                ispbIFCred = "";
                    //                var objRegistro = new JDRegistro();
                    //                objRegistro.Id = objArquivo.Id;
                    //                objRegistro.Seq = seq;
                    //                objRegistro.Tipo = "5";
                    //                objRegistro.NumCtrlIF = numCtrlIF;
                    //                objRegistro.LinhaSegA = linhaSegA;
                    //                objRegistro.LinhaSegB = linhaSegB;
                    //                objRegistro.ISPBIFCred = ispbIFCred;
                    //                objRegistro.Situacao = "01";
                    //                #endregion

                    //                #region SqlBulkCopyRowsAddInsRegistros
                    //                DataRow row = newProducts.NewRow();
                    //                row["IDARQV"] = objRegistro.Id;
                    //                row["SEQREG"] = objRegistro.Seq;
                    //                row["TPAREG"] = objRegistro.Tipo;
                    //                row["NUMCTRLIF"] = objRegistro.NumCtrlIF;
                    //                row["LINHA_SEGMENTO_A"] = objRegistro.LinhaSegA;
                    //                row["LINHA_SEGMENTO_B"] = objRegistro.LinhaSegB;
                    //                row["ISPBIFCRED"] = objRegistro.ISPBIFCred;
                    //                row["STAREG"] = objRegistro.Situacao;
                    //                newProducts.Rows.Add(row);
                    //                #endregion
                    //            }
                    //            else if (tipoRegistro.ToString().Equals("9")) // 9 - TrailerArquivo
                    //            {
                    //                //System.Console.WriteLine($"tipoRegistro: {tipoRegistro.ToString()} - segmentoRegistro: {segmentoRegistro.ToString()} - linha: {linha.ToString()}");

                    //                #region RegistroTrailerArquivo
                    //                seq++;
                    //                linhaSegA = linha.ToString();
                    //                linhaSegB = "";
                    //                numCtrlIF = "";
                    //                ispbIFCred = "";
                    //                var objRegistro = new JDRegistro();
                    //                objRegistro.Id = objArquivo.Id;
                    //                objRegistro.Seq = seq;
                    //                objRegistro.Tipo = "9";
                    //                objRegistro.NumCtrlIF = numCtrlIF;
                    //                objRegistro.LinhaSegA = linhaSegA;
                    //                objRegistro.LinhaSegB = linhaSegB;
                    //                objRegistro.ISPBIFCred = ispbIFCred;
                    //                objRegistro.Situacao = "01";
                    //                #endregion

                    //                #region SqlBulkCopyRowsAddInsRegistros
                    //                DataRow row = newProducts.NewRow();
                    //                row["IDARQV"] = objRegistro.Id;
                    //                row["SEQREG"] = objRegistro.Seq;
                    //                row["TPAREG"] = objRegistro.Tipo;
                    //                row["NUMCTRLIF"] = objRegistro.NumCtrlIF;
                    //                row["LINHA_SEGMENTO_A"] = objRegistro.LinhaSegA;
                    //                row["LINHA_SEGMENTO_B"] = objRegistro.LinhaSegB;
                    //                row["ISPBIFCRED"] = objRegistro.ISPBIFCred;
                    //                row["STAREG"] = objRegistro.Situacao;
                    //                newProducts.Rows.Add(row);
                    //                #endregion

                    //            }

                    //        }
                    //    }
                    //}
                    //stopwatch.Stop();
                    //System.Console.WriteLine($"{stopwatch.Elapsed} => NOVO ReadFile( qtdeLinha: " + qtdeLinha.ToString() + " )"); //ElapsedMilliseconds
                    //#endregion

                    //#region FileHeppersForeach

                    //stopwatch.Reset();
                    //stopwatch.Start();
                    //foreach (var recordt in result) // foreach (var recordt in engine)  //foreach (var recordt in result)
                    //{
                    //    qtdeLinha++;
                    //    var tipo = recordt.GetType();

                    //    if (tipo.Equals(typeof(RegistroHeaderArquivo))) // if (tipo == typeof(RegistroHeaderArquivo))
                    //    {

                    //        #region objRegHeaderArquivo
                    //        seq++;
                    //        var objRegHeaderArquivo = recordt as RegistroHeaderArquivo; // ((RegistroHeaderArquivo)recordt);
                    //        linhaSegA = objRegHeaderArquivo.Linha;
                    //        linhaSegB = "";
                    //        numCtrlIF = "";
                    //        ispbIFCred = "";
                    //        var objRegistro = new JDRegistro();
                    //        objRegistro.Id = objArquivo.Id;
                    //        objRegistro.Seq = seq;
                    //        objRegistro.Tipo = "0";
                    //        objRegistro.NumCtrlIF = numCtrlIF;
                    //        objRegistro.LinhaSegA = linhaSegA;
                    //        objRegistro.LinhaSegB = linhaSegB;
                    //        objRegistro.ISPBIFCred = ispbIFCred;
                    //        objRegistro.Situacao = "01";
                    //        #endregion

                    //        #region DbContextInsRegistros
                    //        //context.Registros.Add(objRegistro);
                    //        //context.SaveChanges();
                    //        //listRegistros.Add(objRegistro);
                    //        #endregion

                    //        #region SqlCommandInsRegistros
                    //        //SqlCommand commandInsReg = new SqlCommand("INSERT INTO TBJDSPBCAB_CNAB240_REGISTRO (IDARQV, SEQREG, TPAREG, NUMCTRLIF, LINHA_SEGMENTO_A, LINHA_SEGMENTO_B, ISPBIFCRED, STAREG) VALUES (@IDARQV, @SEQREG, @TPAREG, @NUMCTRLIF, @LINHA_SEGMENTO_A, @LINHA_SEGMENTO_B, @ISPBIFCRED, @STAREG)", connection); //transaction
                    //        //commandInsReg.Parameters.Add("@IDARQV", SqlDbType.Decimal);
                    //        //commandInsReg.Parameters.Add("@SEQREG", SqlDbType.Decimal);
                    //        //commandInsReg.Parameters.Add("@TPAREG", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@NUMCTRLIF", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@LINHA_SEGMENTO_A", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@LINHA_SEGMENTO_B", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@ISPBIFCRED", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@STAREG", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters["@IDARQV"].Value = objRegistro.Id; 
                    //        //commandInsReg.Parameters["@SEQREG"].Value = objRegistro.Seq; 
                    //        //commandInsReg.Parameters["@TPAREG"].Value = objRegistro.Tipo; 
                    //        //commandInsReg.Parameters["@NUMCTRLIF"].Value = objRegistro.NumCtrlIF; 
                    //        //commandInsReg.Parameters["@LINHA_SEGMENTO_A"].Value = objRegistro.LinhaSegA; 
                    //        //commandInsReg.Parameters["@LINHA_SEGMENTO_B"].Value = objRegistro.LinhaSegB; 
                    //        //commandInsReg.Parameters["@ISPBIFCRED"].Value = objRegistro.ISPBIFCred; 
                    //        //commandInsReg.Parameters["@STAREG"].Value = objRegistro.Situacao; 
                    //        //commandInsReg.ExecuteNonQuery();
                    //        #endregion

                    //        #region SqlBulkCopyRowsAddInsRegistros
                    //        DataRow row = newProducts.NewRow();
                    //        row["IDARQV"] = objRegistro.Id;
                    //        row["SEQREG"] = objRegistro.Seq;
                    //        row["TPAREG"] = objRegistro.Tipo;
                    //        row["NUMCTRLIF"] = objRegistro.NumCtrlIF;
                    //        row["LINHA_SEGMENTO_A"] = objRegistro.LinhaSegA;
                    //        row["LINHA_SEGMENTO_B"] = objRegistro.LinhaSegB;
                    //        row["ISPBIFCRED"] = objRegistro.ISPBIFCred;
                    //        row["STAREG"] = objRegistro.Situacao;
                    //        newProducts.Rows.Add(row);
                    //        #endregion

                    //    }
                    //    else if (tipo.Equals(typeof(RegistroHeaderLote))) // else if (tipo == typeof(RegistroHeaderLote))
                    //    {

                    //        #region RegistroHeaderLote
                    //        seq++;
                    //        var objRegHeaderLote = recordt as RegistroHeaderLote; // ((RegistroHeaderLote)recordt);
                    //        linhaSegA = objRegHeaderLote.Linha;
                    //        linhaSegB = "";
                    //        numCtrlIF = "";
                    //        ispbIFCred = "";
                    //        var objRegistro = new JDRegistro();
                    //        objRegistro.Id = objArquivo.Id;
                    //        objRegistro.Seq = seq;
                    //        objRegistro.Tipo = "1";
                    //        objRegistro.NumCtrlIF = numCtrlIF;
                    //        objRegistro.LinhaSegA = linhaSegA;
                    //        objRegistro.LinhaSegB = linhaSegB;
                    //        objRegistro.ISPBIFCred = ispbIFCred;
                    //        objRegistro.Situacao = "01";
                    //        #endregion

                    //        #region DbContextInsRegistros
                    //        //context.Registros.Add(objRegistro);
                    //        //context.SaveChanges();
                    //        //listRegistros.Add(objRegistro);
                    //        #endregion

                    //        #region SqlCommandInsRegistros
                    //        //SqlCommand commandInsReg = new SqlCommand("INSERT INTO TBJDSPBCAB_CNAB240_REGISTRO (IDARQV, SEQREG, TPAREG, NUMCTRLIF, LINHA_SEGMENTO_A, LINHA_SEGMENTO_B, ISPBIFCRED, STAREG) VALUES (@IDARQV, @SEQREG, @TPAREG, @NUMCTRLIF, @LINHA_SEGMENTO_A, @LINHA_SEGMENTO_B, @ISPBIFCRED, @STAREG)", connection); //transaction
                    //        //commandInsReg.Parameters.Add("@IDARQV", SqlDbType.Decimal);
                    //        //commandInsReg.Parameters.Add("@SEQREG", SqlDbType.Decimal);
                    //        //commandInsReg.Parameters.Add("@TPAREG", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@NUMCTRLIF", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@LINHA_SEGMENTO_A", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@LINHA_SEGMENTO_B", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@ISPBIFCRED", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@STAREG", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters["@IDARQV"].Value = objRegistro.Id;
                    //        //commandInsReg.Parameters["@SEQREG"].Value = objRegistro.Seq;
                    //        //commandInsReg.Parameters["@TPAREG"].Value = objRegistro.Tipo;
                    //        //commandInsReg.Parameters["@NUMCTRLIF"].Value = objRegistro.NumCtrlIF;
                    //        //commandInsReg.Parameters["@LINHA_SEGMENTO_A"].Value = objRegistro.LinhaSegA;
                    //        //commandInsReg.Parameters["@LINHA_SEGMENTO_B"].Value = objRegistro.LinhaSegB;
                    //        //commandInsReg.Parameters["@ISPBIFCRED"].Value = objRegistro.ISPBIFCred;
                    //        //commandInsReg.Parameters["@STAREG"].Value = objRegistro.Situacao;
                    //        //commandInsReg.ExecuteNonQuery();
                    //        #endregion

                    //        #region SqlBulkCopyRowsAddInsRegistros
                    //        DataRow row = newProducts.NewRow();
                    //        row["IDARQV"] = objRegistro.Id;
                    //        row["SEQREG"] = objRegistro.Seq;
                    //        row["TPAREG"] = objRegistro.Tipo;
                    //        row["NUMCTRLIF"] = objRegistro.NumCtrlIF;
                    //        row["LINHA_SEGMENTO_A"] = objRegistro.LinhaSegA;
                    //        row["LINHA_SEGMENTO_B"] = objRegistro.LinhaSegB;
                    //        row["ISPBIFCRED"] = objRegistro.ISPBIFCred;
                    //        row["STAREG"] = objRegistro.Situacao;
                    //        newProducts.Rows.Add(row);
                    //        #endregion

                    //    }
                    //    else if (tipo.Equals(typeof(RegistroDetalheSegmentoA))) // else if (tipo == typeof(RegistroDetalheSegmentoA))
                    //    {
                    //        #region RegistroDetalheSegmentoA
                    //        var objRegDetalheSegA = recordt as RegistroDetalheSegmentoA; // ((RegistroDetalheSegmentoA)recordt);
                    //        linhaSegA = objRegDetalheSegA.Linha;
                    //        linhaSegB = "";
                    //        numCtrlIF = "";
                    //        ispbIFCred = "";
                    //        #endregion
                    //    }
                    //    else if (tipo.Equals(typeof(RegistroDetalheSegmentoB))) // else if (tipo == typeof(RegistroDetalheSegmentoB))
                    //    {
                    //        #region objRegDetalheSegB
                    //        seq++;
                    //        var objRegDetalheSegB = recordt as RegistroDetalheSegmentoB; //((RegistroDetalheSegmentoB)recordt);
                    //        linhaSegB = objRegDetalheSegB.Linha;
                    //        var objRegistro = new JDRegistro();
                    //        objRegistro.Id = objArquivo.Id;
                    //        objRegistro.Seq = seq;
                    //        objRegistro.Tipo = "3";
                    //        objRegistro.NumCtrlIF = numCtrlIF;
                    //        objRegistro.LinhaSegA = linhaSegA;
                    //        objRegistro.LinhaSegB = linhaSegB;
                    //        objRegistro.ISPBIFCred = ispbIFCred;
                    //        objRegistro.Situacao = "01";
                    //        #endregion

                    //        #region DbContextInsRegistros
                    //        //context.Registros.Add(objRegistro);
                    //        //context.SaveChanges();
                    //        //listRegistros.Add(objRegistro);
                    //        #endregion

                    //        #region SqlCommandInsRegistros
                    //        //SqlCommand commandInsReg = new SqlCommand("INSERT INTO TBJDSPBCAB_CNAB240_REGISTRO (IDARQV, SEQREG, TPAREG, NUMCTRLIF, LINHA_SEGMENTO_A, LINHA_SEGMENTO_B, ISPBIFCRED, STAREG) VALUES (@IDARQV, @SEQREG, @TPAREG, @NUMCTRLIF, @LINHA_SEGMENTO_A, @LINHA_SEGMENTO_B, @ISPBIFCRED, @STAREG)", connection); //transaction
                    //        //commandInsReg.Parameters.Add("@IDARQV", SqlDbType.Decimal);
                    //        //commandInsReg.Parameters.Add("@SEQREG", SqlDbType.Decimal);
                    //        //commandInsReg.Parameters.Add("@TPAREG", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@NUMCTRLIF", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@LINHA_SEGMENTO_A", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@LINHA_SEGMENTO_B", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@ISPBIFCRED", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@STAREG", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters["@IDARQV"].Value = objRegistro.Id;
                    //        //commandInsReg.Parameters["@SEQREG"].Value = objRegistro.Seq;
                    //        //commandInsReg.Parameters["@TPAREG"].Value = objRegistro.Tipo;
                    //        //commandInsReg.Parameters["@NUMCTRLIF"].Value = objRegistro.NumCtrlIF;
                    //        //commandInsReg.Parameters["@LINHA_SEGMENTO_A"].Value = objRegistro.LinhaSegA;
                    //        //commandInsReg.Parameters["@LINHA_SEGMENTO_B"].Value = objRegistro.LinhaSegB;
                    //        //commandInsReg.Parameters["@ISPBIFCRED"].Value = objRegistro.ISPBIFCred;
                    //        //commandInsReg.Parameters["@STAREG"].Value = objRegistro.Situacao;
                    //        //commandInsReg.ExecuteNonQuery();
                    //        #endregion

                    //        #region SqlBulkCopyRowsAddInsRegistros
                    //        DataRow row = newProducts.NewRow();
                    //        row["IDARQV"] = objRegistro.Id;
                    //        row["SEQREG"] = objRegistro.Seq;
                    //        row["TPAREG"] = objRegistro.Tipo;
                    //        row["NUMCTRLIF"] = objRegistro.NumCtrlIF;
                    //        row["LINHA_SEGMENTO_A"] = objRegistro.LinhaSegA;
                    //        row["LINHA_SEGMENTO_B"] = objRegistro.LinhaSegB;
                    //        row["ISPBIFCRED"] = objRegistro.ISPBIFCred;
                    //        row["STAREG"] = objRegistro.Situacao;
                    //        newProducts.Rows.Add(row);
                    //        #endregion

                    //        linhaSegA = "";
                    //        linhaSegB = "";
                    //        numCtrlIF = "";
                    //        ispbIFCred = "";
                    //    }
                    //    else if (tipo.Equals(typeof(RegistroTrailerLote))) // else if (tipo == typeof(RegistroTrailerLote))
                    //    {

                    //        #region RegistroTrailerLote
                    //        seq++;
                    //        var objRegTrailerLote = recordt as RegistroTrailerLote; //((RegistroTrailerLote)recordt);
                    //        linhaSegA = objRegTrailerLote.Linha;
                    //        linhaSegB = "";
                    //        numCtrlIF = "";
                    //        ispbIFCred = "";
                    //        var objRegistro = new JDRegistro();
                    //        objRegistro.Id = objArquivo.Id;
                    //        objRegistro.Seq = seq;
                    //        objRegistro.Tipo = "5";
                    //        objRegistro.NumCtrlIF = numCtrlIF;
                    //        objRegistro.LinhaSegA = linhaSegA;
                    //        objRegistro.LinhaSegB = linhaSegB;
                    //        objRegistro.ISPBIFCred = ispbIFCred;
                    //        objRegistro.Situacao = "01";
                    //        #endregion

                    //        #region DbContextInsRegistros
                    //        //context.Registros.Add(objRegistro);
                    //        //context.SaveChanges();
                    //        //listRegistros.Add(objRegistro);
                    //        #endregion

                    //        #region SqlCommandInsRegistros
                    //        //SqlCommand commandInsReg = new SqlCommand("INSERT INTO TBJDSPBCAB_CNAB240_REGISTRO (IDARQV, SEQREG, TPAREG, NUMCTRLIF, LINHA_SEGMENTO_A, LINHA_SEGMENTO_B, ISPBIFCRED, STAREG) VALUES (@IDARQV, @SEQREG, @TPAREG, @NUMCTRLIF, @LINHA_SEGMENTO_A, @LINHA_SEGMENTO_B, @ISPBIFCRED, @STAREG)", connection); //transaction
                    //        //commandInsReg.Parameters.Add("@IDARQV", SqlDbType.Decimal);
                    //        //commandInsReg.Parameters.Add("@SEQREG", SqlDbType.Decimal);
                    //        //commandInsReg.Parameters.Add("@TPAREG", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@NUMCTRLIF", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@LINHA_SEGMENTO_A", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@LINHA_SEGMENTO_B", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@ISPBIFCRED", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@STAREG", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters["@IDARQV"].Value = objRegistro.Id;
                    //        //commandInsReg.Parameters["@SEQREG"].Value = objRegistro.Seq;
                    //        //commandInsReg.Parameters["@TPAREG"].Value = objRegistro.Tipo;
                    //        //commandInsReg.Parameters["@NUMCTRLIF"].Value = objRegistro.NumCtrlIF;
                    //        //commandInsReg.Parameters["@LINHA_SEGMENTO_A"].Value = objRegistro.LinhaSegA;
                    //        //commandInsReg.Parameters["@LINHA_SEGMENTO_B"].Value = objRegistro.LinhaSegB;
                    //        //commandInsReg.Parameters["@ISPBIFCRED"].Value = objRegistro.ISPBIFCred;
                    //        //commandInsReg.Parameters["@STAREG"].Value = objRegistro.Situacao;
                    //        //commandInsReg.ExecuteNonQuery();
                    //        #endregion

                    //        #region SqlBulkCopyRowsAddInsRegistros
                    //        DataRow row = newProducts.NewRow();
                    //        row["IDARQV"] = objRegistro.Id;
                    //        row["SEQREG"] = objRegistro.Seq;
                    //        row["TPAREG"] = objRegistro.Tipo;
                    //        row["NUMCTRLIF"] = objRegistro.NumCtrlIF;
                    //        row["LINHA_SEGMENTO_A"] = objRegistro.LinhaSegA;
                    //        row["LINHA_SEGMENTO_B"] = objRegistro.LinhaSegB;
                    //        row["ISPBIFCRED"] = objRegistro.ISPBIFCred;
                    //        row["STAREG"] = objRegistro.Situacao;
                    //        newProducts.Rows.Add(row);
                    //        #endregion

                    //    }
                    //    else if (tipo.Equals(typeof(RegistroTrailerArquivo))) // else if (tipo == typeof(RegistroTrailerArquivo))
                    //    {

                    //        #region RegistroTrailerArquivo
                    //        seq++;
                    //        var objRegTrailerArquivo = recordt as RegistroTrailerArquivo; // ((RegistroTrailerArquivo)recordt);
                    //        linhaSegA = objRegTrailerArquivo.Linha;
                    //        linhaSegB = "";
                    //        numCtrlIF = "";
                    //        ispbIFCred = "";
                    //        var objRegistro = new JDRegistro();
                    //        objRegistro.Id = objArquivo.Id;
                    //        objRegistro.Seq = seq;
                    //        objRegistro.Tipo = "9";
                    //        objRegistro.NumCtrlIF = numCtrlIF;
                    //        objRegistro.LinhaSegA = linhaSegA;
                    //        objRegistro.LinhaSegB = linhaSegB;
                    //        objRegistro.ISPBIFCred = ispbIFCred;
                    //        objRegistro.Situacao = "01";
                    //        #endregion

                    //        #region DbContextInsRegistros
                    //        //context.Registros.Add(objRegistro);
                    //        //context.SaveChanges();
                    //        //listRegistros.Add(objRegistro);
                    //        #endregion

                    //        #region SqlCommandInsRegistros
                    //        //SqlCommand commandInsReg = new SqlCommand("INSERT INTO TBJDSPBCAB_CNAB240_REGISTRO (IDARQV, SEQREG, TPAREG, NUMCTRLIF, LINHA_SEGMENTO_A, LINHA_SEGMENTO_B, ISPBIFCRED, STAREG) VALUES (@IDARQV, @SEQREG, @TPAREG, @NUMCTRLIF, @LINHA_SEGMENTO_A, @LINHA_SEGMENTO_B, @ISPBIFCRED, @STAREG)", connection); //transaction
                    //        //commandInsReg.Parameters.Add("@IDARQV", SqlDbType.Decimal);
                    //        //commandInsReg.Parameters.Add("@SEQREG", SqlDbType.Decimal);
                    //        //commandInsReg.Parameters.Add("@TPAREG", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@NUMCTRLIF", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@LINHA_SEGMENTO_A", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@LINHA_SEGMENTO_B", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@ISPBIFCRED", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters.Add("@STAREG", SqlDbType.VarChar);
                    //        //commandInsReg.Parameters["@IDARQV"].Value = objRegistro.Id;
                    //        //commandInsReg.Parameters["@SEQREG"].Value = objRegistro.Seq;
                    //        //commandInsReg.Parameters["@TPAREG"].Value = objRegistro.Tipo;
                    //        //commandInsReg.Parameters["@NUMCTRLIF"].Value = objRegistro.NumCtrlIF;
                    //        //commandInsReg.Parameters["@LINHA_SEGMENTO_A"].Value = objRegistro.LinhaSegA;
                    //        //commandInsReg.Parameters["@LINHA_SEGMENTO_B"].Value = objRegistro.LinhaSegB;
                    //        //commandInsReg.Parameters["@ISPBIFCRED"].Value = objRegistro.ISPBIFCred;
                    //        //commandInsReg.Parameters["@STAREG"].Value = objRegistro.Situacao;
                    //        //commandInsReg.ExecuteNonQuery();
                    //        #endregion

                    //        #region SqlBulkCopyRowsAddInsRegistros
                    //        DataRow row = newProducts.NewRow();
                    //        row["IDARQV"] = objRegistro.Id;
                    //        row["SEQREG"] = objRegistro.Seq;
                    //        row["TPAREG"] = objRegistro.Tipo;
                    //        row["NUMCTRLIF"] = objRegistro.NumCtrlIF;
                    //        row["LINHA_SEGMENTO_A"] = objRegistro.LinhaSegA;
                    //        row["LINHA_SEGMENTO_B"] = objRegistro.LinhaSegB;
                    //        row["ISPBIFCRED"] = objRegistro.ISPBIFCred;
                    //        row["STAREG"] = objRegistro.Situacao;
                    //        newProducts.Rows.Add(row);
                    //        #endregion

                    //    }

                    //    #region DbContextInsRegistrosAddRange

                    //    //if ((seq == 100000) || (seq == 200000) || (seq == 300000) || (seq == 400000) || (seq == 500000) || (seq == 600000) || (seq == 700000) || (seq == 800000) || (seq == 900000) || (seq == 1000000))
                    //    //{
                    //    //    System.Console.WriteLine($" SaveChanges( seq: " + seq.ToString() + " )");
                    //    //}

                    //    //if (listRegistros.Count >= 10000)
                    //    //{
                    //    //    System.Console.WriteLine($" SaveChanges( qtdeLinha: " + qtdeLinha.ToString() + " )");
                    //    //    context.Registros.AddRange(listRegistros);
                    //    //    context.SaveChanges();
                    //    //    listRegistros.Clear();
                    //    //}
                    //    #endregion

                    //}
                    //#endregion

                    #region SqlBulkCopyInsRegistrosWriteToServer
                    //using (var transaction = connection.BeginTransaction())
                    //{
                    //    try
                    //    {
                    //        transaction.Commit();
                    //    }
                    //    catch (Exception)
                    //    {
                    //        transaction.Rollback(); throw;
                    //    }
                    //}

                    //newProducts.AcceptChanges();
                    using (var sqlBulk = new SqlBulkCopy(conn)) //, SqlBulkCopyOptions.KeepIdentity, transaction
                    {
                        //sqlBulk.NotifyAfter = 1000;
                        //sqlBulk.SqlRowsCopied += (sender, eventArgs) => Console.WriteLine("Wrote " + eventArgs.RowsCopied + " records.");
                        sqlBulk.BulkCopyTimeout = 60;
                        sqlBulk.BatchSize = 1000;
                        sqlBulk.DestinationTableName = "dbo.TBJDSPBCAB_CNAB240_REGISTRO";
                        sqlBulk.WriteToServer(newProducts); // await sqlBulk.WriteToServerAsync(newProducts);
                        sqlBulk.Close();
                    }

                    //var dt = new DataTable();
                    //dt.Columns.Add("CustomerID", typeof(int));
                    //dt.Columns.Add("CustomerName");
                    //dt.Columns.Add("ContactName");
                    //dt.Columns.Add("Address");

                    //var bulk = new BulkOperation(conn);
                    //bulk.DestinationTableName = "dbo.TBJDSPBCAB_CNAB240_REGISTRO";
                    //bulk.ColumnMappings.Add("CustomerID", ColumnMappingDirectionType.Output);
                    //bulk.ColumnMappings.Add("CustomerName");
                    //bulk.ColumnMappings.Add("ContactName");
                    //bulk.ColumnMappings.Add("Address");
                    //bulk.BulkInsert(dt);

                    #endregion



                    #region DbContextInsRegistrosAddRange
                    //if (listRegistros.Count > 0)
                    //{
                    //    System.Console.WriteLine($" SaveChanges( qtdeLinha: " + qtdeLinha.ToString() + " )");
                    //    context.Registros.AddRange(listRegistros);
                    //    context.SaveChanges();
                    //    listRegistros.Clear();
                    //}
                    #endregion

                    //objArquivo.Situacao = "RR2"; //RR2	Arquivo REM Carregado com sucesso. Pendente integração.

                    #region DbContextAtuArquivos
                    //context.Entry(objArquivo).State = System.Data.Entity.EntityState.Modified;
                    //context.SaveChanges();
                    #endregion

                    //using (SqlCommand cmd = connection.CreateCommand())
                    //{
                    //    cmd.CommandText = @"UPDATE TBJDSPBCAB_CNAB240_ARQUIVO SET STARQV = @STARQV  WHERE IDARQV = @IDARQV";
                    //    cmd.Parameters.AddWithValue("STARQV", objArquivo.Situacao);
                    //    cmd.Parameters.AddWithValue("IDARQV", objArquivo.Id);
                    //    cmd.ExecuteNonQuery();
                    //}


                    conn.Close();
                    conn.Dispose();

                    stopwatch.Stop();
                    System.Console.WriteLine($"{stopwatch.Elapsed} => foreachFile( qtdeLinha: " + qtdeLinha.ToString() + " )");

                } //  using (var connectio

            }
            finally
            {
                System.Console.WriteLine("Fim");
                System.Console.ReadKey();
            }
        }
        static private Type CustomSelector(MultiRecordEngine engine, string recordLine)
        {
            var TipoRegistro = recordLine.Substring(07, 1).Trim();
            var SegmentoRegistro = recordLine.Substring(13, 1).Trim();

            if (TipoRegistro.Equals("0")) // 0-HeaderArquivo
                return typeof(RegistroHeaderArquivo);
            else if (TipoRegistro.Equals("1"))  // 1-HeaderLote
                return typeof(RegistroHeaderLote);
            else if ((TipoRegistro.Equals("3")) && (SegmentoRegistro.Equals("A"))) // 3-DetalheSegmentoA
                return typeof(RegistroDetalheSegmentoA);
            else if ((TipoRegistro.Equals("3")) && (SegmentoRegistro.Equals("B"))) // 3-DetalheSegmentoB
                return typeof(RegistroDetalheSegmentoB);
            else if (TipoRegistro.Equals("5")) // 5-TrailerLote
                return typeof(RegistroTrailerLote);
            else if (TipoRegistro.Equals("9")) // 9-TrailerArquivo
                return typeof(RegistroTrailerArquivo);
            else
                return null;

            //if (recordLine.StartsWith("29000000"))
            //    return typeof(RegistroHeaderArquivo);
            //if (recordLine.StartsWith("29000011"))
            //    return typeof(RegistroHeaderLote);
            //if (recordLine.StartsWith("2900001300001A"))
            //    return typeof(RegistroDetalheSegmentoA);
            //if (recordLine.StartsWith("900001300002B"))
            //    return typeof(RegistroDetalheSegmentoB);
            //if (recordLine.StartsWith("29000015"))
            //    return typeof(RegistroTrailerLote);
            //if (recordLine.StartsWith("29099999"))
            //    return typeof(RegistroTrailerArquivo);
            //else
            //    return null;

        }

        static private void TesteWriterCNAB240(string pathFile, int qtdeLinha)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            using (var writer = new StreamWriter(pathFile))
            {
                writer.WriteLine("29000000      080208561701000101                    00910 000000009396 2PAGSEGURO INTERNET LTDA       ITA                                     10511201910501900000000000000                                                                     ");
                writer.WriteLine("29000011C2041040 208561701000101                    00910 000000009396 2PAGSEGURO INTERNET LTDA                                                                             00000                                   00000000                    ");
                for (int i = 0; i < qtdeLinha; i++)
                {
                    writer.WriteLine("2900001300001A00100074803953 000000019468 9MARIA  DO SOCORRO SILVA       JDNUMCTRLIF00000000105112019REA        0000000000000000007700                            000000000000000                    00000000053779711591            0          ");
                    writer.WriteLine("2900001300002B   100053779711591                              00000                                                  00000000                                                                                                                   ");
                }
                writer.WriteLine("29000015         000024000000000008599213000000000000000000000000                                                                                                                                                                               ");
                writer.WriteLine("29099999         000001000024                                                                                                                                                                                                                   ");
            }
            stopwatch.Stop();
            System.Console.WriteLine($"{stopwatch.Elapsed} => StreamWriter( qtdeLinha: " + qtdeLinha.ToString() + " )");
        }

        static private void TestFileHelpersCNAB240(string pathDir)
        {
            //var path = pathDir + "FileHelpers.txt";
            //var engine = new FileHelperEngine<TesteCustomer>(Encoding.UTF8);
            //var result = engine.ReadFile(path);
            //engine.WriteFile("FileOut.txt", result);
            //foreach (TesteCustomer cust in result) Log.Information("*" + cust.CustId.ToString() + "* - *" + cust.Name + "* - *" + cust.Balance.ToString() + "*");

            //var path = pathDir + "FileHelpers.txt";
            //var engine = new FileHelperAsyncEngine<TesteCustomer>();
            //using (engine.BeginReadFile(path)) foreach (TesteCustomer cust in engine) Log.Information(cust.Name);

            //var path = pathDir + "FileHelpers.txt";
            //var engine = new FileHelperAsyncEngine<TesteCustomer>();
            //var arrayCustomers = new TesteCustomer[] { new TesteCustomer { CustId = 1732, Name = "Juan Perez", Balance = 435.00m, AddedDate = new DateTime(2020, 5, 11) }, new Customer { CustId = 554, Name = "Pedro Gomez", Balance = 12342.30m, AddedDate = new DateTime(2004, 2, 6) }, };
            //using (engine.BeginWriteFile(path))
            //    foreach (TesteCustomer cust in arrayCustomers)
            //        engine.WriteNext(cust);

        }

    }
}
