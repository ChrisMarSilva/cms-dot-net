

using Oracle.ManagedDataAccess.Client;

Console.WriteLine("Inicio");
try
{
    var dbDatabase = "PIXHML-ORACLE21C"; //  "JDDSVDB";
    var dbDatabaseIp = "10.1.4.11:1521/JDPI_SPI_PIX_AUT_AUTORIZACAO";  // "JDDSVDB.JDCONSULTORES.COM.BR,1522/JDDSVDB"; //  "10.204.1.3:1521/PROD"; 
    var dbDatabaseFull = "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.1.4.11)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = XE)))"; // "(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=JDDSVDB.JDCONSULTORES.COM.BR)(PORT=1522))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=JDDSVDB.JDCONSULTORES.COM.BR)))"; 
    var dbUser = "JDPI_SPI_PIX_AUT_AUTORIZACAO"; // "CRIS_JDPCS";
    var dbPass = "JDPI_SPI_PIX_AUT_AUTORIZACAO"; // "CRIS_JDPCS";

    //var databases = new List<string>
    //{
    //    $"JDDSVDB",
    //    $"10.204.1.3:1521/PROD",
    //    $"(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=JDDSVDB.JDCONSULTORES.COM.BR)(PORT=1522))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=JDDSVDB.JDCONSULTORES.COM.BR)))",
    //};

    var connectionStrings = new List<string>
    {
        $"Data Source={dbDatabase};User Id={dbUser};Password={dbPass};",
       // $"Data Source={dbDatabase};User Id={dbUser};Password={dbPass};DBA Privilege=SYSDBA;",
        //$"Data Source={dbDatabase};User Id={dbUser};Password={dbPass};DBA Privilege=Normal;",
        //$"Data Source={dbUser}/{dbPass}@10.1.4.11:1521/{dbDatabase};",
        //$"Data Source={dbUser}/{dbPass}@10.1.4.11/{dbDatabase};",
        //$"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.1.4.11)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));User Id=JDPI_SPI_PIX_AUT_AUTORIZACAO;Password=JDPI_SPI_PIX_AUT_AUTORIZACAO;",
        //$"Data Source={dbDatabaseIp};User Id={dbUser};Password={dbPass};",
        //$"Data Source={dbDatabaseFull};User Id={dbUser};Password={dbPass};",
        //"Data Source=10.1.4.11:1521/JDPI_SPI_PIX_AUT_AUTORIZACAO;User Id=JDPI_SPI_PIX_AUT_AUTORIZACAO;Password=JDPI_SPI_PIX_AUT_AUTORIZACAO;",

        //$"uid={dbUser};pwd={dbPass};Data Source={dbDatabase};",
        //$"uid={dbUser};pwd={dbPass};Data Source={dbDatabaseIp};",
        //$"uid={dbUser};pwd={dbPass};Data Source={dbDatabaseFull};",

        //$"USER ID={dbUser};Data Source={dbDatabase};",
        //$"USER ID={dbUser};Data Source={dbDatabaseIp};",
        //$"USER ID={dbUser};Data Source={dbDatabaseFull};",

        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabase};",
        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabase};Integrated Security=yes;",
        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabase};DBA Privilege=SYSDBA;",
        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabase};Persist Security Info=False;Provider=MSDAORA.1;",
        //$"USER ID={dbUser};PASSWORD={dbPass};DATA SOURCE={dbDatabase};PERSIST SECURITY INFO=True;",
        //$"USER ID={dbUser};PASSWORD={dbPass};DATA SOURCE={dbDatabase};PERSIST SECURITY INFO=True;Pooling=False;",
        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabase};Validate Connection=True;",

        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabaseIp};",
        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabaseIp};Integrated Security=yes;",
        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabaseIp};DBA Privilege=SYSDBA;",
        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabaseIp};Persist Security Info=False;Provider=MSDAORA.1;",
        //$"USER ID={dbUser};PASSWORD={dbPass};DATA SOURCE={dbDatabaseIp};PERSIST SECURITY INFO=True;",
        //$"USER ID={dbUser};PASSWORD={dbPass};DATA SOURCE={dbDatabaseIp};PERSIST SECURITY INFO=True;Pooling=False;",
        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabaseIp};Validate Connection=True;",

        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabaseFull};",
        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabaseFull};Integrated Security=yes;",
        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabaseFull};DBA Privilege=SYSDBA;",
        //$"User ID={dbUser};Password={dbPass};Data Source={dbDatabaseFull};Persist Security Info=False;Provider=MSDAORA.1;",
        //$"USER ID={dbUser};PASSWORD={dbPass};DATA SOURCE={dbDatabaseFull};PERSIST SECURITY INFO=True;",
        //$"USER ID={dbUser};PASSWORD={dbPass};DATA SOURCE={dbDatabaseFull};PERSIST SECURITY INFO=True;Pooling=False;",
        //$"USER ID={dbUser};Password={dbPass};Data Source={dbDatabaseFull};Validate Connection=True;",

        //$"USER ID={dbUser};Password={dbPass};Data Source=JDDSVDB.JDCONSULTORES.COM.BR,1522;",
        //$"USER ID={dbUser};Password={dbPass};Data Source=JDDSVDB.JDCONSULTORES.COM.BR;Integrated Security=yes;",

        //$"Uid={dbUser};Pwd={dbPass};Server={dbDatabase};",

        //$"User Id=jdpiapi;Password=jdpiapi;Data Source=localhost:1521/orclpdb1;",
        //$"User Id={dbUser};Password={dbUser};Data Source={dbDatabase};",
    };

    foreach (var connStr in connectionStrings)
    {
        try
        {
            using var conn = new OracleConnection(connStr);
            conn.Open();
            Console.WriteLine($"✅ Conexão OK: {connStr}");
        }
        //catch (OracleException ex)
        //{
        //    Console.WriteLine($"❌ Falha na conexão oracle: {ex.Code} - {ex.Message}");
        //}
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Falha na conexão: {ex.Message}");
        }
    }
}
finally
{
    Console.WriteLine("Fim");
    Console.ReadKey();
}


