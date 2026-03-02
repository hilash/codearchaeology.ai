using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SqlHelper
    {
        protected static string ConnectionString
        {
            get
            {
                return @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Hila\Songs\App_Data\DB.mdf;Integrated Security=True;User Instance=True";
            }
           
        }

        public static int FullConnected(string SqlString)
        {
            int rowsAffected = -100;
            SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString);
            SqlCommand command = new SqlCommand(SqlString, connection);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
         
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected;
        }

        public static DataTable GetDataTable(string sqlS, string tableName)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlS, SqlHelper.ConnectionString);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            dt.TableName = tableName;
            return dt;
        }

        public static int UpdateDataTable(DataTable dt)
        {
            int rowsAffected;

            string SqlString = "SELECT * FROM " + dt.TableName + " WHERE 1=0";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(SqlString, SqlHelper.ConnectionString);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            rowsAffected = dataAdapter.Update(dt);

            return rowsAffected;
        }

        public static void PrintDataTable(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                    Console.Write(dr[i] + "   ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }
/*
        static void Main(string[] args)
        {
            string hila = "HILAB";
            string sqlString = "SELECT * FROM tblArtist";
            string table_name = "tblArtist";
           
            DataTable dt = SqlHelper.GetDataTable(sqlString, table_name);

            DataRow newDr = dt.NewRow();
            newDr["Artist_ID"] = 67;
            newDr[1] = "Band";
            newDr[2] = "Queen";

            dt.Rows.Add(newDr);


            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                    Console.Write(dr[i].ToString() + "   ");
                Console.WriteLine();
            }
            
           FullConnected("INSERT INTO tblArtist(Artist_ID,Artist_Type,Artist_Name) values(11,'" + hila + "','" + hila + "')");
           
            Console.WriteLine(SqlHelper.UpdateDataTable(dt));
            Console.ReadKey();
        }
 * 
 */
    }
}
