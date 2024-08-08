using CMS_DotNet_File_Css.Converters;
using CMS_DotNet_File_Css.Models;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Transactions;

Console.WriteLine("INI");
try
{
    var inputFilePath = "D:\\git\\CMS\\Migração Bnf\\exportaParaJd\\Beneficiarios.txt";
    //var tempFilePath = "D:\\git\\CMS\\Migração Bnf\\exportaParaJd\\BeneficiariosNew.txt";
    var beneficiarios = new List<BeneficiarioModel>();
    var stopWatch = new Stopwatch();
    var qtdLine = 0;
    //var qtdErros = 0;

    stopWatch.Start();
    try
    {
        using (var reader = new StreamReader(inputFilePath))
        {
            //using var writer = new StreamWriter(tempFilePath);

            var line = "";
            while ((line = reader.ReadLine()) != null)
            {
                qtdLine++;
                if (qtdLine == 1) continue;

                var values = line.Split(',');
                var beneficiario = new BeneficiarioModel();

                //if ((qtdLine == 1) || (qtdLine == 10534))
                //    Console.WriteLine($"Nro: {qtdLine.ToString("N0")} - Line: {line}");

                if (values.Length != 21)
                {
                    // var newLine = "";
                    if (values.Length == 22)
                    {
                        //newLine = $"" +
                        //    $"{values[0]}," + // CPFCNPJ_BENEFICIARIO
                        //    $"{values[1]}," + // TP_PESSOA_BENEFICIARIO
                        //    $"{values[2]}," + // ISPB_IF_ADMINISTRADA
                        //    $"{values[3]}," + // ISPB_IF_PRINCIPAL
                        //    $"{values[4] + values[5]}," + // NM_BENEFICIARIO
                        //    $"{values[6]}," + // NM_FANTASIA
                        //    $"{values[7]}," + // DT_INICIO_RELACIONAMENTO
                        //    $"{values[8]}," + // DT_FIM_RELACIONAMENTO
                        //    $"{values[9]}," + // SIT_RELACIONAMENTO
                        //    $"{values[10]}," + // SIT_BENEFICIARIO_IF
                        //    $"{values[11]}," + // DTHR_SIT_BENEFICIARIO_IF
                        //    $"{values[12]}," + // ID_BENEFICIARIO
                        //    $"{values[13]}," + // NUMREF_BENEFICIARIO
                        //    $"{values[14]}," + // NUMSEQ_BENEFICIARIO
                        //    $"{values[15]}," + // TP_MSG_ARQUIVO
                        //    $"{values[16]}," + // NUMMSG_REC
                        //    $"{values[17]}," + // ID_ARQV_REC
                        //    $"{values[18]}," + // DTHR_REGISTRO
                        //    $"{values[19]}," + // DTHR_ALTERACAO
                        //    $"{values[20]}," + // DT_INICIO_RELACIONAMENTO_ANTG
                        //    $"{values[21]}"; // DT_INICIO_RELACIONAMENTO_RECTE

                        beneficiario = BeneficiarioConverter.CreateBeneficiarioModel(
                            cpfCnpjBeneficiario: values[0],
                            tpPessoaBeneficiario: values[1],
                            ispbIfAdministrada: values[2],
                            ispbIfPrincipal: values[3],
                            nmBeneficiario: values[4] + values[5],
                            nmFantasia: values[6],
                            dtInicioRelacionamento: values[7],
                            dtFimRelacionamento: values[8],
                            sitRelacionamento: values[9],
                            sitBeneficiarioIf: values[10],
                            dthrSitBeneficiarioIf: values[11],
                            idBeneficiario: values[12],
                            numRefBeneficiario: values[13],
                            numSeqBeneficiario: values[14],
                            tpMsgArquivo: values[15],
                            numMsgRec: values[16],
                            idArqvRec: values[17],
                            dthrRegistro: values[18],
                            dthrAlteracao: values[19],
                            dtInicioRelacionamentoAntg: values[20],
                            dtInicioRelacionamentoRecte: values[21]
                        );
                    }
                    else if (values.Length == 23)
                    {
                        //newLine = $"" +
                        //    $"{values[0]}," + // CPFCNPJ_BENEFICIARIO
                        //    $"{values[1]}," + // TP_PESSOA_BENEFICIARIO
                        //    $"{values[2]}," + // ISPB_IF_ADMINISTRADA
                        //    $"{values[3]}," + // ISPB_IF_PRINCIPAL
                        //    $"{values[4] + values[5]}," + // NM_BENEFICIARIO
                        //    $"{values[6] + values[7]}," + // NM_FANTASIA
                        //    $"{values[8]}," + // DT_INICIO_RELACIONAMENTO
                        //    $"{values[9]}," + // DT_FIM_RELACIONAMENTO
                        //    $"{values[10]}," + // SIT_RELACIONAMENTO
                        //    $"{values[11]}," + // SIT_BENEFICIARIO_IF
                        //    $"{values[12]}," + // DTHR_SIT_BENEFICIARIO_IF
                        //    $"{values[13]}," + // ID_BENEFICIARIO
                        //    $"{values[14]}," + // NUMREF_BENEFICIARIO
                        //    $"{values[15]}," + // NUMSEQ_BENEFICIARIO
                        //    $"{values[16]}," + // TP_MSG_ARQUIVO
                        //    $"{values[17]}," + // NUMMSG_REC
                        //    $"{values[18]}," + // ID_ARQV_REC
                        //    $"{values[19]}," + // DTHR_REGISTRO
                        //    $"{values[20]}," + // DTHR_ALTERACAO
                        //    $"{values[21]}," + // DT_INICIO_RELACIONAMENTO_ANTG
                        //    $"{values[22]}"; // DT_INICIO_RELACIONAMENTO_RECTE

                        beneficiario = BeneficiarioConverter.CreateBeneficiarioModel(
                            cpfCnpjBeneficiario: values[0],
                            tpPessoaBeneficiario: values[1],
                            ispbIfAdministrada: values[2],
                            ispbIfPrincipal: values[3],
                            nmBeneficiario: values[4] + values[5],
                            nmFantasia: values[6] + values[7],
                            dtInicioRelacionamento: values[8],
                            dtFimRelacionamento: values[9],
                            sitRelacionamento: values[10],
                            sitBeneficiarioIf: values[11],
                            dthrSitBeneficiarioIf: values[12],
                            idBeneficiario: values[13],
                            numRefBeneficiario: values[14],
                            numSeqBeneficiario: values[15],
                            tpMsgArquivo: values[16],
                            numMsgRec: values[17],
                            idArqvRec: values[18],
                            dthrRegistro: values[19],
                            dthrAlteracao: values[20],
                            dtInicioRelacionamentoAntg: values[21],
                            dtInicioRelacionamentoRecte: values[22]
                        );
                    }
                    else if (values.Length == 24)
                    {
                        //newLine = $"" +
                        //    $"{values[0]}," + // CPFCNPJ_BENEFICIARIO
                        //    $"{values[1]}," + // TP_PESSOA_BENEFICIARIO
                        //    $"{values[2]}," + // ISPB_IF_ADMINISTRADA
                        //    $"{values[3]}," + // ISPB_IF_PRINCIPAL
                        //    $"{values[4] + values[5] + values[6]}," + // NM_BENEFICIARIO
                        //    $"{values[7] + values[8]}," + // NM_FANTASIA
                        //    $"{values[9]}," + // DT_INICIO_RELACIONAMENTO
                        //    $"{values[10]}," + // DT_FIM_RELACIONAMENTO
                        //    $"{values[11]}," + // SIT_RELACIONAMENTO
                        //    $"{values[12]}," + // SIT_BENEFICIARIO_IF
                        //    $"{values[13]}," + // DTHR_SIT_BENEFICIARIO_IF
                        //    $"{values[14]}," + // ID_BENEFICIARIO
                        //    $"{values[15]}," + // NUMREF_BENEFICIARIO
                        //    $"{values[16]}," + // NUMSEQ_BENEFICIARIO
                        //    $"{values[17]}," + // TP_MSG_ARQUIVO
                        //    $"{values[18]}," + // NUMMSG_REC
                        //    $"{values[19]}," + // ID_ARQV_REC
                        //    $"{values[20]}," + // DTHR_REGISTRO
                        //    $"{values[21]}," + // DTHR_ALTERACAO
                        //    $"{values[22]}," + // DT_INICIO_RELACIONAMENTO_ANTG
                        //    $"{values[23]}"; // DT_INICIO_RELACIONAMENTO_RECTE

                        beneficiario = BeneficiarioConverter.CreateBeneficiarioModel(
                            cpfCnpjBeneficiario: values[0],
                            tpPessoaBeneficiario: values[1],
                            ispbIfAdministrada: values[2],
                            ispbIfPrincipal: values[3],
                            nmBeneficiario: values[4] + values[5] + values[6],
                            nmFantasia: values[7] + values[8],
                            dtInicioRelacionamento: values[9],
                            dtFimRelacionamento: values[10],
                            sitRelacionamento: values[11],
                            sitBeneficiarioIf: values[12],
                            dthrSitBeneficiarioIf: values[13],
                            idBeneficiario: values[14],
                            numRefBeneficiario: values[15],
                            numSeqBeneficiario: values[16],
                            tpMsgArquivo: values[17],
                            numMsgRec: values[18],
                            idArqvRec: values[19],
                            dthrRegistro: values[20],
                            dthrAlteracao: values[21],
                            dtInicioRelacionamentoAntg: values[22],
                            dtInicioRelacionamentoRecte: values[23]
                        );
                    }
                    else if (values.Length == 25)
                    {
                        //    newLine = $"" +
                        //        $"{values[0]}," + // CPFCNPJ_BENEFICIARIO
                        //        $"{values[1]}," + // TP_PESSOA_BENEFICIARIO
                        //        $"{values[2]}," + // ISPB_IF_ADMINISTRADA
                        //        $"{values[3]}," + // ISPB_IF_PRINCIPAL
                        //        $"{values[4] + values[5] + values[6]}," + // NM_BENEFICIARIO
                        //        $"{values[7] + values[8] + values[9]}," + // NM_FANTASIA
                        //        $"{values[10]}," + // DT_INICIO_RELACIONAMENTO
                        //        $"{values[11]}," + // DT_FIM_RELACIONAMENTO
                        //        $"{values[12]}," + // SIT_RELACIONAMENTO
                        //        $"{values[13]}," + // SIT_BENEFICIARIO_IF
                        //        $"{values[14]}," + // DTHR_SIT_BENEFICIARIO_IF
                        //        $"{values[15]}," + // ID_BENEFICIARIO
                        //        $"{values[16]}," + // NUMREF_BENEFICIARIO
                        //        $"{values[17]}," + // NUMSEQ_BENEFICIARIO
                        //        $"{values[18]}," + // TP_MSG_ARQUIVO
                        //        $"{values[19]}," + // NUMMSG_REC
                        //        $"{values[20]}," + // ID_ARQV_REC
                        //        $"{values[21]}," + // DTHR_REGISTRO
                        //        $"{values[22]}," + // DTHR_ALTERACAO
                        //        $"{values[23]}," + // DT_INICIO_RELACIONAMENTO_ANTG
                        //        $"{values[24]}"; // DT_INICIO_RELACIONAMENTO_RECTE

                        beneficiario = BeneficiarioConverter.CreateBeneficiarioModel(
                            cpfCnpjBeneficiario: values[0],
                            tpPessoaBeneficiario: values[1],
                            ispbIfAdministrada: values[2],
                            ispbIfPrincipal: values[3],
                            nmBeneficiario: values[4] + values[5] + values[6],
                            nmFantasia: values[7] + values[8] + values[9],
                            dtInicioRelacionamento: values[10],
                            dtFimRelacionamento: values[11],
                            sitRelacionamento: values[12],
                            sitBeneficiarioIf: values[13],
                            dthrSitBeneficiarioIf: values[14],
                            idBeneficiario: values[15],
                            numRefBeneficiario: values[16],
                            numSeqBeneficiario: values[17],
                            tpMsgArquivo: values[18],
                            numMsgRec: values[19],
                            idArqvRec: values[20],
                            dthrRegistro: values[21],
                            dthrAlteracao: values[22],
                            dtInicioRelacionamentoAntg: values[23],
                            dtInicioRelacionamentoRecte: values[24]
                        );
                    }
                    else
                    {
                        Console.WriteLine($"Nro: {qtdLine.ToString("N0")} - Qtd. Fields: {values.Length}: Line: {line}");
                    }

                    //writer.WriteLine(newLine); // salvar a linha alterada
                    //qtdErros++;
                    //if (qtdErros > 1) break;
                }
                else
                {
                    beneficiario = BeneficiarioConverter.CreateBeneficiarioModel(
                        cpfCnpjBeneficiario: values[0],
                        tpPessoaBeneficiario: values[1],
                        ispbIfAdministrada: values[2],
                        ispbIfPrincipal: values[3],
                        nmBeneficiario: values[4],
                        nmFantasia: values[5],
                        dtInicioRelacionamento: values[6],
                        dtFimRelacionamento: values[7],
                        sitRelacionamento: values[8],
                        sitBeneficiarioIf: values[9],
                        dthrSitBeneficiarioIf: values[10],
                        idBeneficiario: values[11],
                        numRefBeneficiario: values[12],
                        numSeqBeneficiario: values[13],
                        tpMsgArquivo: values[14],
                        numMsgRec: values[15],
                        idArqvRec: values[16],
                        dthrRegistro: values[17],
                        dthrAlteracao: values[18],
                        dtInicioRelacionamentoAntg: values[19],
                        dtInicioRelacionamentoRecte: values[20]
                    );

                    // writer.WriteLine(line); // salvar a linha original, sem alteração
                }

                //if (qtdLine == 101880) // if ((qtdLine == 101877) || (qtdLine == 101878) || (qtdLine == 101879) || (qtdLine == 101880) || (qtdLine == 101881))
                beneficiarios.Add(beneficiario);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERRO ARQV({qtdLine.ToString("N0")}): {ex.Message}");
        return;
    }
    finally
    {
        stopWatch.Stop();
        var ts = stopWatch.Elapsed;
        var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        Console.WriteLine($"Tempo Arquivo: {elapsedTime} ({stopWatch.ElapsedMilliseconds} ms)");
        Console.WriteLine($"qtdLine: {qtdLine.ToString("N0")}");
        //Console.WriteLine($"qtdErros: {qtdErros.ToString("N0")}");
    }

    stopWatch.Restart();
    try
    {
        var dataTable = new DataTable("TBJDNPCBNF_CIP_RPRSNTNT");
        // dataTable.Columns.Add(new DataColumn() { ColumnName = "xxxx", DataType = Nullable.GetUnderlyingType(typeof(string)) ?? typeof(string), AllowDBNull = true });
        //dataTable.Columns.Add("CPFCNPJ_BENEFICIARIO", typeof(string));
        //dataTable.Columns.Add("TP_PESSOA_BENEFICIARIO", typeof(string));
        //dataTable.Columns.Add("ISPB_IF_ADMINISTRADA", typeof(string));
        //dataTable.Columns.Add("ISPB_IF_PRINCIPAL", typeof(decimal?));
        //dataTable.Columns.Add("NM_BENEFICIARIO", typeof(string));
        //dataTable.Columns.Add("NM_FANTASIA", typeof(string));
        //dataTable.Columns.Add("DT_INICIO_RELACIONAMENTO", typeof(string));
        //dataTable.Columns.Add("DT_FIM_RELACIONAMENTO", typeof(string));
        //dataTable.Columns.Add("SIT_RELACIONAMENTO", typeof(string));
        //dataTable.Columns.Add("SIT_BENEFICIARIO_IF", typeof(string));
        //dataTable.Columns.Add("DTHR_SIT_BENEFICIARIO_IF", typeof(decimal?));
        //dataTable.Columns.Add("ID_BENEFICIARIO", typeof(string));
        //dataTable.Columns.Add("NUMREF_BENEFICIARIO", typeof(decimal?));
        //dataTable.Columns.Add("NUMSEQ_BENEFICIARIO", typeof(decimal?));
        //dataTable.Columns.Add("TP_MSG_ARQUIVO", typeof(string));
        //dataTable.Columns.Add("NUMMSG_REC", typeof(decimal?));
        //dataTable.Columns.Add("ID_ARQV_REC", typeof(decimal?));
        //dataTable.Columns.Add("DTHR_REGISTRO", typeof(decimal?));
        //dataTable.Columns.Add("DTHR_ALTERACAO", typeof(decimal?));
        //dataTable.Columns.Add("DT_INICIO_RELACIONAMENTO_ANTG", typeof(string));
        //dataTable.Columns.Add("DT_INICIO_RELACIONAMENTO_RECTE", typeof(string));

        var connectionString = "Data Source=jdsp118;Initial Catalog=TESTE_JDNPC_CELCOIN_HML;User ID=jddesenv;Password=jddesenv;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        using var conn = new SqlConnection(connectionString);
        conn.Open();

        using (var cmd = new SqlCommand("TRUNCATE TABLE TBJDNPCBNF_CIP_RPRSNTNT", conn))
        {
            cmd.ExecuteNonQuery();
        }
        using (var cmd = new SqlCommand("TRUNCATE TABLE TBJDNPCBNF_CIP_CNVN", conn))
        {
            cmd.ExecuteNonQuery();
        }
        using (var cmd = new SqlCommand("DELETE FROM TBJDNPCBNF_CIP_BNF", conn))
        {
            cmd.ExecuteNonQuery();
        }

        qtdLine = 0;
        var query = @"INSERT INTO TBJDNPCBNF_CIP_BNF (CPFCNPJ_BENEFICIARIO, TP_PESSOA_BENEFICIARIO, ISPB_IF_ADMINISTRADA, ISPB_IF_PRINCIPAL, NM_BENEFICIARIO, NM_FANTASIA, DT_INICIO_RELACIONAMENTO, DT_FIM_RELACIONAMENTO, SIT_RELACIONAMENTO, SIT_BENEFICIARIO_IF, DTHR_SIT_BENEFICIARIO_IF, ID_BENEFICIARIO, NUMREF_BENEFICIARIO, NUMSEQ_BENEFICIARIO, TP_MSG_ARQUIVO, NUMMSG_REC, ID_ARQV_REC, DTHR_REGISTRO, DTHR_ALTERACAO, DT_INICIO_RELACIONAMENTO_ANTG, DT_INICIO_RELACIONAMENTO_RECTE ) VALUES (@CPFCNPJ_BENEFICIARIO, @TP_PESSOA_BENEFICIARIO, @ISPB_IF_ADMINISTRADA, @ISPB_IF_PRINCIPAL, @NM_BENEFICIARIO, @NM_FANTASIA, @DT_INICIO_RELACIONAMENTO, @DT_FIM_RELACIONAMENTO, @SIT_RELACIONAMENTO, @SIT_BENEFICIARIO_IF, @DTHR_SIT_BENEFICIARIO_IF, @ID_BENEFICIARIO, @NUMREF_BENEFICIARIO, @NUMSEQ_BENEFICIARIO, @TP_MSG_ARQUIVO, @NUMMSG_REC, @ID_ARQV_REC, @DTHR_REGISTRO, @DTHR_ALTERACAO, @DT_INICIO_RELACIONAMENTO_ANTG, @DT_INICIO_RELACIONAMENTO_RECTE)";

        foreach (var beneficiario in beneficiarios)
        {
            qtdLine++;
            //Console.WriteLine($"Beneficiario: {beneficiario.CPFCNPJ_BENEFICIARIO}, Nome: {beneficiario.NM_BENEFICIARIO}");

            try
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CPFCNPJ_BENEFICIARIO", beneficiario.CPFCNPJ_BENEFICIARIO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TP_PESSOA_BENEFICIARIO", beneficiario.TP_PESSOA_BENEFICIARIO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ISPB_IF_ADMINISTRADA", beneficiario.ISPB_IF_ADMINISTRADA ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ISPB_IF_PRINCIPAL", beneficiario.ISPB_IF_PRINCIPAL ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NM_BENEFICIARIO", GetFirst50Characters(beneficiario.NM_BENEFICIARIO) ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NM_FANTASIA", GetFirst50Characters(beneficiario.NM_FANTASIA) ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DT_INICIO_RELACIONAMENTO", beneficiario.DT_INICIO_RELACIONAMENTO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DT_FIM_RELACIONAMENTO", beneficiario.DT_FIM_RELACIONAMENTO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SIT_RELACIONAMENTO", beneficiario.SIT_RELACIONAMENTO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SIT_BENEFICIARIO_IF", beneficiario.SIT_BENEFICIARIO_IF ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DTHR_SIT_BENEFICIARIO_IF", beneficiario.DTHR_SIT_BENEFICIARIO_IF ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ID_BENEFICIARIO", beneficiario.ID_BENEFICIARIO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NUMREF_BENEFICIARIO", beneficiario.NUMREF_BENEFICIARIO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NUMSEQ_BENEFICIARIO", beneficiario.NUMSEQ_BENEFICIARIO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TP_MSG_ARQUIVO", beneficiario.TP_MSG_ARQUIVO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NUMMSG_REC", beneficiario.NUMMSG_REC ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ID_ARQV_REC", beneficiario.ID_ARQV_REC ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DTHR_REGISTRO", beneficiario.DTHR_REGISTRO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DTHR_ALTERACAO", beneficiario.DTHR_ALTERACAO ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DT_INICIO_RELACIONAMENTO_ANTG", beneficiario.DT_INICIO_RELACIONAMENTO_ANTG ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DT_INICIO_RELACIONAMENTO_RECTE", beneficiario.DT_INICIO_RELACIONAMENTO_RECTE ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }

                //var row = dataTable.NewRow();
                //row["CPFCNPJ_BENEFICIARIO"] = beneficiario.CPFCNPJ_BENEFICIARIO ?? (object)DBNull.Value;
                //row["TP_PESSOA_BENEFICIARIO"] = beneficiario.TP_PESSOA_BENEFICIARIO ?? (object)DBNull.Value;
                //row["ISPB_IF_ADMINISTRADA"] = beneficiario.ISPB_IF_ADMINISTRADA ?? (object)DBNull.Value;
                //row["ISPB_IF_PRINCIPAL"] = beneficiario.ISPB_IF_PRINCIPAL ?? (object)DBNull.Value;
                //row["NM_BENEFICIARIO"] = GetFirst50Characters(beneficiario.NM_BENEFICIARIO) ?? (object)DBNull.Value;
                //row["NM_FANTASIA"] = GetFirst50Characters(beneficiario.NM_FANTASIA) ?? (object)DBNull.Value;
                //row["DT_INICIO_RELACIONAMENTO"] = beneficiario.DT_INICIO_RELACIONAMENTO ?? (object)DBNull.Value;
                //row["DT_FIM_RELACIONAMENTO"] = beneficiario.DT_FIM_RELACIONAMENTO ?? (object)DBNull.Value;
                //row["SIT_RELACIONAMENTO"] = beneficiario.SIT_RELACIONAMENTO ?? (object)DBNull.Value;
                //row["SIT_BENEFICIARIO_IF"] = beneficiario.SIT_BENEFICIARIO_IF ?? (object)DBNull.Value;
                //row["DTHR_SIT_BENEFICIARIO_IF"] = beneficiario.DTHR_SIT_BENEFICIARIO_IF ?? (object)DBNull.Value;
                //row["ID_BENEFICIARIO"] = beneficiario.ID_BENEFICIARIO ?? (object)DBNull.Value;
                //row["NUMREF_BENEFICIARIO"] = beneficiario.NUMREF_BENEFICIARIO ?? (object)DBNull.Value;
                //row["NUMSEQ_BENEFICIARIO"] = beneficiario.NUMSEQ_BENEFICIARIO ?? (object)DBNull.Value;
                //row["TP_MSG_ARQUIVO"] = beneficiario.TP_MSG_ARQUIVO ?? (object)DBNull.Value;
                //row["NUMMSG_REC"] = beneficiario.NUMMSG_REC ?? (object)DBNull.Value;
                //row["ID_ARQV_REC"] = beneficiario.ID_ARQV_REC ?? (object)DBNull.Value;
                //row["DTHR_REGISTRO"] = beneficiario.DTHR_REGISTRO ?? (object)DBNull.Value;
                //row["DTHR_ALTERACAO"] = beneficiario.DTHR_ALTERACAO ?? (object)DBNull.Value;
                //row["DT_INICIO_RELACIONAMENTO_ANTG"] = beneficiario.DT_INICIO_RELACIONAMENTO_ANTG ?? (object)DBNull.Value;
                //row["DT_INICIO_RELACIONAMENTO_RECTE"] = beneficiario.DT_INICIO_RELACIONAMENTO_RECTE ?? (object)DBNull.Value;
                //dataTable.Rows.Add(row);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO BANCO({qtdLine.ToString("N0")}): {ex.Message}");
            }
        }

        // var transaction = conn.BeginTransaction();
        try
        {
            // dataTable.AcceptChanges();
            // var optionsDestino = SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls; // | SqlBulkCopyOptions.UseInternalTransaction
            // using (var bulkCopy = new SqlBulkCopy(conn, optionsDestino)))
            // {
            //    bulkCopy.DestinationTableName = "TBJDNPCBNF_CIP_BNF";
            //    bulkCopy.BulkCopyTimeout = 60;
            //    bulkCopy.BatchSize = 10_000;

            //    bulkcopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("CPFCNPJ_BENEFICIARIO", "CPFCNPJ_BENEFICIARIO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("TP_PESSOA_BENEFICIARIO", "TP_PESSOA_BENEFICIARIO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ISPB_IF_ADMINISTRADA", "ISPB_IF_ADMINISTRADA"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ISPB_IF_PRINCIPAL", "ISPB_IF_PRINCIPAL"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("NM_BENEFICIARIO", "NM_BENEFICIARIO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("NM_FANTASIA", "NM_FANTASIA"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DT_INICIO_RELACIONAMENTO", "DT_INICIO_RELACIONAMENTO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DT_FIM_RELACIONAMENTO", "DT_FIM_RELACIONAMENTO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("SIT_RELACIONAMENTO", "SIT_RELACIONAMENTO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("SIT_BENEFICIARIO_IF", "SIT_BENEFICIARIO_IF"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DTHR_SIT_BENEFICIARIO_IF", "DTHR_SIT_BENEFICIARIO_IF"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ID_BENEFICIARIO", "ID_BENEFICIARIO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("NUMREF_BENEFICIARIO", "NUMREF_BENEFICIARIO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("NUMSEQ_BENEFICIARIO", "NUMSEQ_BENEFICIARIO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("TP_MSG_ARQUIVO", "TP_MSG_ARQUIVO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("NUMMSG_REC", "NUMMSG_REC"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ID_ARQV_REC", "ID_ARQV_REC"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DTHR_REGISTRO", "DTHR_REGISTRO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DTHR_ALTERACAO", "DTHR_ALTERACAO"));
            //    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("DT_INICIO_RELACIONAMENTO_ANTG", "DT_INICIO_RELACIONAMENTO_ANTG"));
            //    bulkCopy.ColumnMappings.Addnew SqlBulkCopyColumnMapping(("DT_INICIO_RELACIONAMENTO_RECTE", "DT_INICIO_RELACIONAMENTO_RECTE"));

            //    bulkCopy.ColumnMappings.Add("CPFCNPJ_BENEFICIARIO", "CPFCNPJ_BENEFICIARIO");
            //    bulkCopy.ColumnMappings.Add("TP_PESSOA_BENEFICIARIO", "TP_PESSOA_BENEFICIARIO");
            //    bulkCopy.ColumnMappings.Add("ISPB_IF_ADMINISTRADA", "ISPB_IF_ADMINISTRADA");
            //    bulkCopy.ColumnMappings.Add("ISPB_IF_PRINCIPAL", "ISPB_IF_PRINCIPAL");
            //    bulkCopy.ColumnMappings.Add("NM_BENEFICIARIO", "NM_BENEFICIARIO");
            //    bulkCopy.ColumnMappings.Add("NM_FANTASIA", "NM_FANTASIA");
            //    bulkCopy.ColumnMappings.Add("DT_INICIO_RELACIONAMENTO", "DT_INICIO_RELACIONAMENTO");
            //    bulkCopy.ColumnMappings.Add("DT_FIM_RELACIONAMENTO", "DT_FIM_RELACIONAMENTO");
            //    bulkCopy.ColumnMappings.Add("SIT_RELACIONAMENTO", "SIT_RELACIONAMENTO");
            //    bulkCopy.ColumnMappings.Add("SIT_BENEFICIARIO_IF", "SIT_BENEFICIARIO_IF");
            //    bulkCopy.ColumnMappings.Add("DTHR_SIT_BENEFICIARIO_IF", "DTHR_SIT_BENEFICIARIO_IF");
            //    bulkCopy.ColumnMappings.Add("ID_BENEFICIARIO", "ID_BENEFICIARIO");
            //    bulkCopy.ColumnMappings.Add("NUMREF_BENEFICIARIO", "NUMREF_BENEFICIARIO");
            //    bulkCopy.ColumnMappings.Add("NUMSEQ_BENEFICIARIO", "NUMSEQ_BENEFICIARIO");
            //    bulkCopy.ColumnMappings.Add("TP_MSG_ARQUIVO", "TP_MSG_ARQUIVO");
            //    bulkCopy.ColumnMappings.Add("NUMMSG_REC", "NUMMSG_REC");
            //    bulkCopy.ColumnMappings.Add("ID_ARQV_REC", "ID_ARQV_REC");
            //    bulkCopy.ColumnMappings.Add("DTHR_REGISTRO", "DTHR_REGISTRO");
            //    bulkCopy.ColumnMappings.Add("DTHR_ALTERACAO", "DTHR_ALTERACAO");
            //    bulkCopy.ColumnMappings.Add("DT_INICIO_RELACIONAMENTO_ANTG", "DT_INICIO_RELACIONAMENTO_ANTG");
            //    bulkCopy.ColumnMappings.Add("DT_INICIO_RELACIONAMENTO_RECTE", "DT_INICIO_RELACIONAMENTO_RECTE");

            //    bulkCopy.WriteToServer(dataTable);
            //}

            //transaction.Commit();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO SqlBulkCopy: {ex.Message}");
            //transaction.Rollback();
        }
    }
    finally
    {
        stopWatch.Stop();
        var ts = stopWatch.Elapsed;
        var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
        Console.WriteLine($"Tempo Banco: {elapsedTime} ({stopWatch.ElapsedMilliseconds} ms)");
        Console.WriteLine($"qtdLine: {qtdLine.ToString("N0")}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"ERRO: {ex.Message}");
}
finally
{
    Console.WriteLine("FIM");
    Console.ReadKey();
}

string GetFirst50Characters(string? input)
{
    if (input != null && input.Length > 0)
        return input.Length <= 50 ? input : input.Substring(0, 50);
    return string.Empty;
}