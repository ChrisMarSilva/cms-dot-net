using System;
using System.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=tamonabo_BDCMSTamoNaBolsa;Uid=root;Pwd=")) // "server=localhost;port=3305;database=tamonabo_BDCMSTamoNaBolsa;uid=root";  //'/root:@localhost:3306/tamonabo_BDCMSTamoNaBolsa'
            {
                conn.Open();

                var stopwatch = new Stopwatch();
                stopwatch.Reset();
                stopwatch.Start();

                //MySqlCommand Query = new MySqlCommand();
                //Query.Connection = conn;
                //Query.CommandText = @"SELECT * FROM TBUSUARIO";
                //MySqlDataReader dtreader = Query.ExecuteReader();
                //while (dtreader.Read())//Enquanto existir dados no select
                //    System.Console.WriteLine($"{dtreader["id"].ToString()} - {dtreader["nome"].ToString()}- {dtreader["senha"].ToString()}");

                MySqlCommand commandDel01 = new MySqlCommand("DELETE FROM ccccccccccc", conn);
                commandDel01.ExecuteNonQuery();

                MySqlBulkLoader bcp1 = new MySqlBulkLoader(conn);

                string Command = "INSERT INTO User (FirstName, LastName ) VALUES (@FirstName, @LastName);";
                for (int i = 0; i < 100000; i++) 
                {
                    using (MySqlCommand myCmd = new MySqlCommand(Command, conn))
                    {
                        myCmd.CommandType = CommandType.Text;
                        myCmd.Parameters.AddWithValue("@FirstName", "test");
                        myCmd.Parameters.AddWithValue("@LastName", "test");
                        myCmd.ExecuteNonQuery();
                    }
                }



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



                //SqlTransaction transaction = conn.BeginTransaction();
                //try
                //{
                //    foreach (string commandString in dbOperations)
                //    {
                //        SqlCommand cmd = new SqlCommand(commandString, conn, transaction);
                //        cmd.ExecuteNonQuery();
                //    }
                //    transaction.Commit();
                //} // Here the execution is committed to the DB
                //catch (Exception)
                //{
                //    transaction.Rollback();
                //    throw;
                //}

                stopwatch.Stop();
                System.Console.WriteLine($"{stopwatch.Elapsed}");



            }
        }
    }
}
