using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class tblArtist : SqlHelper
    {
        private static string tblName
        {
            get
            {
                return "tblArtist";
            }
        }

        private static int InsertArtist(
            int Artist_ID,
            string Artist_Type,
            string Artist_Name,
            string Artist_Bio,
            string Artist_Pic
            )
        {
            return FullConnected("INSERT INTO tblArtist(Artist_ID, Artist_Type, Artist_Name, Artist_Bio, Artist_Pic) values('" + Artist_ID + "','" + Artist_Type + "','" + Artist_Name + "','" + Artist_Bio + "','" + Artist_Pic + "')");
        }

        private static int UpdateArtist(
            int Artist_ID,
            string Artist_Type,
            string Artist_Name,
            string Artist_Bio,
            string Artist_Pic
            )
        {
            bool flag = false;
            string SqlString = "UPDATE tblArtist ";

            if (!((Artist_Type == "") || (Artist_Type == null)))
            {
                SqlString += "SET ";
                SqlString += "Artist_Type='" + Artist_Type + "'";
                flag = true;
            }
            if (!((Artist_Name == "") || (Artist_Name == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", "; 
                SqlString += "Artist_Name='" + Artist_Name + "'";
                flag = true;
            }
            if (!((Artist_Bio == "") || (Artist_Bio == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Artist_Bio='" + Artist_Bio + "'";
                flag = true;
            }
            if (!((Artist_Pic == "") || (Artist_Pic == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Artist_Pic='" + Artist_Pic + "'";
                flag = true;
            }
            if (flag)
            {
                SqlString += " WHERE Artist_ID='" + Artist_ID + "'";
                return FullConnected(SqlString);
            }
            else return -1;
        }

        private static int DeleteArtist(int Artist_ID)
        {
            return FullConnected("DELETE FROM tblArtist WHERE Artist_ID='" + Artist_ID + "'");
        }

        private static int ArtistID(string Artist_Name)
        {
            string SS = "SELECT Artist_Id FROM tblArtist WHERE Artist_Name='" + Artist_Name + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return int.Parse(dt.Rows[0]["Artist_Id"].ToString());
        }

        private static string GetInfo(string output_type, string where_type,string where_what)
        {
            string SS = "SELECT * FROM tblArtist WHERE ";
            SS += where_type;
            SS += "='" + where_what + "'";
            Console.WriteLine(SS);
            DataTable dt = GetDataTable(SS, tblName);

            return dt.Rows[0][output_type].ToString();
        }

        /*static void Main(string[] args)
        {
            //Console.WriteLine(GetInfo("Artist_Type", "Artist_Name", "KISS"));

            //InsertArtist(1234, "Band", "AC/DC", "http", "http");

            //UpdateArtist(1236, "", "","", "NNN");

            Console.ReadKey();
        }*/
    }
}
