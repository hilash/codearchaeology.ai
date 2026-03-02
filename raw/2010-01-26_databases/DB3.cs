using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc; 

namespace DataBases_HW3
{
    class DB3
    {
        static void Main(string[] args)
        {
            
            OdbcConnection DbConnection = new OdbcConnection("Driver={MySQL ODBC 5.1 Driver};Server=localhost;Database=bank; User=root;Password=;Option=3;");
            DbConnection.Open();
            OdbcCommand DbCommand = DbConnection.CreateCommand();
            DbCommand.CommandText = "SELECT * FROM Customer_Balance";
            OdbcDataReader DbReader = DbCommand.ExecuteReader();
            int fCount = DbReader.FieldCount;
            Console.Write(":");
            for (int i = 0; i < fCount; i++)
            {
                String fName = DbReader.GetName(i);
                Console.Write(fName + ":");
            }
            Console.WriteLine();
            while (DbReader.Read())
            {
                Console.Write(":");
                for (int i = 0; i < fCount; i++)
                {
                    String col = DbReader.GetString(i);
                    Console.Write(col + ":");
                }
                Console.WriteLine();
            }
            DbReader.Close();
            DbCommand.Dispose();
            DbConnection.Close();



        }
    }
}
