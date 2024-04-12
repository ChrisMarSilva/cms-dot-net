using System.Data;
using System.Data.SqlClient;

Console.WriteLine("INI"); 
try
{

    Console.WriteLine("Conectando ao BD de Origem...");
    var tabelaOrigem = "TBJDNPCBNF_CIP_CNVN"; //TbDadosOrigem // TBTESTE // TBJDNPCBNF_CIP_BNF // TBJDNPCBNF_CIP_CNVN // TBJDNPCBNF_CIP_RPRSNTNT
    var connectionStringOrigem = "";
    //connectionStringOrigem = "Data Source=127.0.0.1,1433;Initial Catalog=BDOrigem;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // CMS - NOTE
    connectionStringOrigem = "Data Source=10.0.27.143;Initial Catalog=Teste_JDNPC_CORA_PRD;User ID=Teste_JDNPC_CORA_PRD;Password=Teste_JDNPC_CORA_PRD;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // JD - JDNPC_CORA_PRD
    using var connOrigem = new SqlConnection(connectionStringOrigem);
    connOrigem.Open();

    Console.WriteLine("Conectando ao BD de Destino...");
    var tabelaDestino = "TBJDNPCBNF_CIP_CNVN"; // TbDadosDestino // TBTESTE // TBJDNPCBNF_CIP_BNF // TBJDNPCBNF_CIP_CNVN // TBJDNPCBNF_CIP_RPRSNTNT
    var connectionStringDestino = "";
    //connectionStringDestino = "Data Source=127.0.0.1,1433;Initial Catalog=BDDestino;User ID=sa;Password=Hello123#;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // CMS - NOTE
    connectionStringDestino = "Data Source=jdsp118;Initial Catalog=Teste_JDNPC_CORA_PRD;User ID=jddesenv;Password=jddesenv;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // JD - JDNPC_BNF_DOTNET
    using var connDestino = new SqlConnection(connectionStringDestino);
    connDestino.Open();

    var transactionDestino = connDestino.BeginTransaction();
    try
    {
        //Console.WriteLine("Limpando dados da BD de Destino...");
        //var selectDestino = $"TRUNCATE TABLE {tabelaDestino}";
        //var commandDelDestino = new SqlCommand(selectDestino, connDestino, transactionDestino);
        //commandDelDestino.ExecuteNonQuery();

        Console.WriteLine("Consultando dados da BD de Origem...");
        var selectOrigem = $"SELECT * FROM {tabelaOrigem} WITH(NOLOCK)";
        using var commandSelOrigem = new SqlCommand(selectOrigem, connOrigem);
        using var readerOrigem = commandSelOrigem.ExecuteReader();

        Console.WriteLine("Migrando dados da BD de Origem para o BD de Destino...");
        var optionsDestino = SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls; // | SqlBulkCopyOptions.UseInternalTransaction
        using var bulkCopy = new SqlBulkCopy(connDestino, optionsDestino, transactionDestino);
        bulkCopy.DestinationTableName = tabelaDestino;
        bulkCopy.BulkCopyTimeout = 1000 * 60 * 30;
        bulkCopy.BatchSize = 10_000;
        //bulkCopy.InsertIfNotExists = false;
        //bulkCopy.Transaction = transaction;
        //bulkCopy.ErrorMode = ErrorModeType.ThrowException;
        //bulkCopy.Log += Console.WriteLine;
        bulkCopy.WriteToServer(readerOrigem);

        transactionDestino.Commit();
        Console.WriteLine("Dados migrados com sucesso!");
    }
    catch (Exception ex)
    {
        transactionDestino.Rollback();
        Console.WriteLine($"ERRO-1: ${ex.Message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"ERRO-2: ${ex.Message}");
} 
finally
{
    Console.WriteLine("FIM");
}



/*
 
 
var table = new DataTable();
table.Columns.Add(new DataColumn("Id", typeof(Guid)));
table.Columns.Add(new DataColumn("Status", typeof(int)));

foreach (var contact in enumerable)
{
	var row = table.NewRow();
	row["Id"] = contact.Id;
	row["Status"] = (int) contact.EmailValidationStatus;
	table.Rows.Add(row);
}

using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionOptions))
{
	using (var connection = this.CreateSqlConnection())
	{
		connection.Open();
		var command = new SqlCommand("CREATE TABLE #Contacts (Id UNIQUEIDENTIFIER, Status INT)", connection);
		command.ExecuteNonQuery();
        table.AcceptChanges();
		using (var bulk = new SqlBulkCopy(connection))
		{
            bulk.BatchSize = 5000;
			bulk.BulkCopyTimeout = 0;
			bulk.DestinationTableName = "#Contacts";
			bulk.WriteToServer(table);
		}
		command.CommandText = "MERGE INTO dbo.[Contact] AS Target USING #Contacts AS Source ON Target.Id = Source.Id WHEN MATCHED THEN UPDATE SET Target.EmailValidationStatus = @status;";
		command.Parameters.Add(new SqlParameter("@status", (int) status));
		command.ExecuteNonQuery();
		command.CommandText = "DROP TABLE #Contacts";
		command.ExecuteNonQuery();
	}
	scope.Complete();
}


List<string> toinsert = new List<string>();
StringBuilder insertCmd = new StringBuilder("INSERT INTO tabblename (col1, col2, col3) VALUES ");

foreach (var row in rows)
{
      var first = row.First.Replace("'", "''")
      var second = row.Second.Replace("'", "''")
      var third = row.Third.Replace("'", "''")
      toinsert.Add(string.Format("( '{0}', '{1}', '{2}' )", first, second, third));
}
if (toinsert.Count != 0)
{
      insertCmd.Append(string.Join(",", toinsert));
      insertCmd.Append(";");
}
using (MySqlCommand myCmd = new MySqlCommand(insertCmd.ToString(), SQLconnectionObject))
{
      myCmd.CommandType = CommandType.Text;
      myCmd.ExecuteNonQuery();
}




dttableNew  = dttableOld.Clone();  

foreach (DataRow drtableOld in dttableOld.Rows)
{
   if (put some Condition)
   {
    dtTableNew.ImportRow(drtableOld);
}
}


String matchString = "ID0001"//assuming we have to find rows having key=ID0001
    DataTable dtTarget = new DataTable();
dtTarget = dtSource.Clone();
DataRow[] rowsToCopy;
rowsToCopy = dtSource.Select("key='" + matchString + "'");
foreach (DataRow temp in rowsToCopy)
{
    dtTarget.ImportRow(temp);
}


table1.AsEnumerable().Take(recodCount).CopyToDataTable(table2, LoadOption.OverwriteChanges);

table1.AsEnumerable().Where(yourcondition).CopyToDataTable(table2, LoadOption.OverwriteChanges)


    DataTable dataTable2 = dataTable1.Copy()
	
	
	DataTable destTable = sourceTable.Clone();
sourceTable.AsEnumerable().Where(row => condition ).ToList().ForEach(row => destTable.ImportRow(row));


DataTable newTable = oldtable.Clone();
for (int i = 0; i < oldtable.Rows.Count; i++)
{
    DataRow drNew = newTable.NewRow();
    drNew.ItemArray = oldtable.Rows[i].ItemArray;
    newTable.Rows.Add(drNew);
}


// Generate DataTable[] alltables from multiple datatables

DataTable newTable = alltables[0].Clone();
foreach (DataTable dt in alltables)
{
    for (int i = 0; i < dt.Rows.Count; i++)
        newTable.Rows.Add(dt.Rows[i].ItemArray);
}



DataRow dr = dataset1.Tables[0].NewRow();
for (int i = 0; i < dataset1.Tables[1].Columns.Count; i++)
{
    dr[dataset1.Tables[1].Columns[i].ColumnName] = dataset1.Tables[1].Rows[0][i];
}
datasetReport.Tables[0].Rows.Add(dr);


private void CopyDataTable(DataTable table)
{
    // Create an object variable for the copy.
    DataTable copyDataTable;
    copyDataTable = table.Copy();
    // Insert code to work with the copy.
}



oldDateTale.Merge(newDataTable);




DataGridView sourceGrid = this.dataGridView1;
DataGridView targetGrid = this.dataGridView2;
targetGrid.DataSource = sourceGrid.DataSource;



DataSet ds = new DataSet();
System.Data.DataTable dt = new System.Data.DataTable();
dt = _BOSearchView.DS.Tables[BusLib.TPV.TableName.SearchView].Copy();
ds.Tables.Add(dt);
UltGrdSaleExcel.SetDataBinding(ds, dt.TableName, true);


DataTable newTable = oldTable.DefaultView.ToTable(false, "ColumnName1", "ColumnName2", "ColumnName3", "ColumnName4", "ColumnName5");



DataTable oldTable = new DataTable();
DataTable newTable = oldTable.Copy();
for (int i = 5; i < 65; i++)
{
    newTable.Columns.RemoveAt(i);
}


DataTable dt = new DataTable();
string[] column = { "Column1", "Column2" };
dt = DTItem.DefaultView.ToTable("dd", false, column);
//DTItem is the Existing  Table and "dd" is the temporary tablename, u give whatever u want



private void button3_Click(object sender, EventArgs e)
{
    DataTable destiny = new DataTable();
    destiny.Columns.Add("c1");//set the columns you want to copy
    destiny.Columns.Add("c2");
    destiny.Columns.Add("c3");
    destiny.Columns.Add("c4");

    CopyColumns(dtST_Split, destiny, "c1", "c2", "c3", "c4");

}
// function to copy all rows but just specific columns from table 1 to table 2
private void CopyColumns(DataTable source, DataTable dest, params string[] columns)
{
    foreach (DataRow sourcerow in source.Rows)
    {
        DataRow destRow = dest.NewRow();
        foreach (string colname in columns)
        {
            destRow[colname] = sourcerow[colname];
        }
        dest.Rows.Add(destRow);
    }
}



public void CopyImport2CT(string myTable)
{
    //Copy data Importtable to Landentabel
    try
    {
        // Create the dataset and add the Categories table to it:
        DataSet myProviderDs = new DataSet();
        OleDbConnection mycopy2CTConn = null;
        try
        {
            mycopy2CTConn = new OleDbConnection(strProviderConn);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
            throw ex;
        }
        string copystring = "select * from " + myTable;
        OleDbCommand myProviderCommand = new OleDbCommand(copystring, mycopy2CTConn);
        OleDbDataAdapter ca = new OleDbDataAdapter(myProviderCommand);
        DataSet ds1 = new DataSet();
        DataTable dt1 = new DataTable(myTable);
        dt1.Columns.Add("Countrycode");
        dt1.Columns.Add("Country");
        dt1.Columns.Add("Highfee");
        dt1.Columns.Add("Lowfee");
        dt1.Columns.Add("Weekendfee");
        ca.Fill(ds1);
        Console.WriteLine(ds1.Tables[0].Rows.Count);
        mycopy2CTConn.Open();
        DataSet ds2 = new DataSet();
        DataTable dt2 = new DataTable("Landentabel");
        dt2.Columns.Add("Landnummer");
        dt2.Columns.Add("Landnaam");
        dt2.Columns.Add("Piektarief");
        dt2.Columns.Add("Daltarief");
        dt2.Columns.Add("Weekendtarief");
        OleDbConnection myCTConn = null;
        try
        {
            myCTConn = new OleDbConnection(strProviderConn);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
            throw ex;
        }


        for (int k = 0; k < ds1.Tables[0].Rows.Count; k++)
        {
            dt2.Rows.Add.[k]["Landnummer"] = ds2.Tables[0].Rows[k]["Countrycode"];
            dt2.Rows.Add.[k]["Landnaam"] = ds2.Tables[0].Rows[k]["Country"];
            dt2.Rows.Add.[k]["Piektarief"] = ds2.Tables[0].Rows[k]["Highfee"];
            dt2.Rows.Add.[k]["Daltarief"] = ds2.Tables[0].Rows[k]["Lowfee"];
            dt2.Rows.Add.[k]["Weekendtarief"] = ds2.Tables[0].Rows[k]["Weekendfee"];
        }

        mycopy2CTConn.Close();

    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
        throw ex;
    }
}

*/