using System.Data.OracleClient;

Console.WriteLine("Inicio");
try
{

    var connectionString = "";
    connectionString = "User Id=ALDO_JDNPC;Password=ALDO_JDNPC;Data Source=JDDSVDB;Integrated Security=yes;";
    connectionString = "User Id=ALDO_JDNPC;Password=ALDO_JDNPC;Data Source=JDDSVDB.JDCONSULTORES.COM.BR;Integrated Security=yes;";
    connectionString = "Data Source=JDDSVDB;User Id=ALDO_JDNPC;Password=ALDO_JDNPC;";
    connectionString = "Data Source=JDDSVDB;Integrated Security=yes;";
    connectionString = "Data Source=JDDSVDB;User Id=ALDO_JDNPC;Password=ALDO_JDNPC;DBA Privilege=SYSDBA;";
    connectionString = "Provider=MSDAORA.1;Data Source=JDDSVDB;User Id=ALDO_JDNPC;Password=ALDO_JDNPC;Persist Security Info=False;";
    connectionString = "User Id=JDPIX;Password=JDPIX;Data Source=10.0.27.97:1521/XE;";
    connectionString = "Data Source=JDDSVDB.JDCONSULTORES.COM.BR,1522;User Id=ALDO_JDNPC;Password=ALDO_JDNPC;";
    connectionString = "user id=ALDO_JDNPC;password=ALDO_JDNPC;data source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = JDDSVDB.JDCONSULTORES.COM.BR)(PORT = 1522))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = JDDSVDB.JDCONSULTORES.COM.BR)))";
    connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=JDDSVDB.JDCONSULTORES.COM.BR)(PORT=1522))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=JDDSVDB.JDCONSULTORES.COM.BR)));User Id=ALDO_JDNPC;Password=ALDO_JDNPC;";
    connectionString = "Server=JDDSVDB;Uid=ALDO_JDNPC;Pwd=ALDO_JDNPC;";
    connectionString = "User Id=ALDO_JDNPC;Password=ALDO_JDNPC;Data Source=10.10.20.35:1521/XE;";

    // "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=myserver.sys.mycompany.com)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=mydb.mycompany.com)));User Id=MyLanId;Password=MyPasswors;Validate Connection=True;"

    // Foi feita uma tentativa de se carregar um programa com um formato incorreto. (0x8007000B)

    using (OracleConnection connection = new OracleConnection(connectionString))
    {
        connection.Open();
    }

}
catch (OracleException ex)
{
    string errorMessage = "Code: " + ex.Code + "\n" +"Message: " + ex.Message;
    //System.Diagnostics.EventLog log = new System.Diagnostics.EventLog();
    //log.Source = "My Application";
    //log.WriteEntry(errorMessage);
    // Console.WriteLine("An exception occurred. Please contact your system administrator.");
    Console.WriteLine($"ErroOracle: {errorMessage}");
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


