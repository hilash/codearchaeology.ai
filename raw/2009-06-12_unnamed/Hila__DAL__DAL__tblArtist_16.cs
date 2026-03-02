using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class tblArtist : SqlHelper
    {
        public static string tblName
        {
            get
            {
                return "tblArtist";
            }
        }

        public static int InsertArtist(
            int Artist_ID,
            string Artist_Type,
            string Artist_Name,
            string Artist_Bio,
            string Artist_Pic
            )
        {
            return FullConnected("INSERT INTO tblArtist(Artist_ID, Artist_Type, Artist_Name, Artist_Bio, Artist_Pic,Artist_Pop) values('" + Artist_ID + "','" + Artist_Type + "','" + Artist_Name + "','" + Artist_Bio + "','" + Artist_Pic + "',0)");
        }

        public static int UpdateArtist(
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

        public static int DeleteArtist(int Artist_ID)
        {
            return FullConnected("DELETE FROM tblArtist WHERE Artist_ID='" + Artist_ID + "'");
        }




        /// <summary>
        /// INPUT:  Artist_ID
        /// OUTPUT: is the artist is in the Data Base
        /// </summary>
        /// <param name="Artist_ID"></param>
        /// <returns></returns>
        public static bool IsArtistInDB(int Artist_ID)
        {
            return (FullConnected("UPDATE tblArtist SET Artist_ID='" + Artist_ID + "' WHERE Artist_ID='" + Artist_ID + "'") > 0) ? true : false;
        }

        /// <summary>
        /// INPUT:  Artist_ID
        /// OUTPUT: data table with information about the Artist
        /// </summary>
        /// <param name="Artist_ID"></param>
        /// <returns></returns>
        public static DataTable GetArtistDT(int Artist_ID)
        {
            return GetDataTable("SELECT * FROM tblArtist WHERE Artist_ID='" + Artist_ID + "'", "tblArtist");
        }

        /// <summary>
        /// OUTPUT: data table with information about all the Artist in the data base
        /// </summary>
        /// <returns></returns>
        public static DataTable GetArtistsDT()
        {
            return GetDataTable("SELECT * FROM tblArtist", "tblArtist");
        }






        public static void AddArtistPOP(int Artist_ID, int add)
        {
            int newPop;
            int Artist_Pop = int.Parse(GetInfo("Artist_Pop", "Artist_ID", Artist_ID.ToString()));
            if ((newPop = (Artist_Pop + add)) <= 0)
                newPop = 0;
            FullConnected("UPDATE tblArtist SET Artist_Pop='" + newPop + "'  WHERE Artist_ID='" + Artist_ID + "'");
        }

        public static int Artist_ID(string Artist_Name)
        {
            string SS = "SELECT Artist_Id FROM tblArtist WHERE Artist_Name='" + Artist_Name + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return int.Parse(dt.Rows[0]["Artist_Id"].ToString());
        }

        // given a popularity rank, returns the Artist in that popularity rank
        public static int ArtistByPop(int number)
        {
            string SS = "SELECT (Artist_ID) FROM tblArtist ORDER BY Artist_Pop DESC ";
            DataTable dt = GetDataTable(SS, "tblArtist");
            if (number > dt.Rows.Count)
                return -1;
            else return int.Parse(dt.Rows[number-1]["Artist_Id"].ToString());
        }

        // return a data table with (Artist_ID,Artist_Pop) ordered by Artist_Pop
        public static DataTable ArtistPopTable()
        {
            string SS = "SELECT Artist_ID, Artist_Pop FROM tblArtist ORDER BY Artist_Pop DESC";
            DataTable dt = GetDataTable(SS, "tblArtist");
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                    Console.Write(dr[i].ToString() + "   ");
                Console.WriteLine();
            }
            return dt;
        }


        public static DataTable SearchArtistName(string Artist_Name)
        {
            string SS = "SELECT Artist_ID, Artist_Name, Artist_Type, Artist_Pop FROM tblArtist WHERE Artist_Name LIKE '%";
            SS += Artist_Name;
            SS += "%'";

            return GetDataTable(SS, "tblArtist");
        }

        public static string GetInfo(string output_type, string where_type, string where_what)
        {
            string SS = "SELECT * FROM tblArtist WHERE ";
            SS += where_type;
            SS += "='" + where_what + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return dt.Rows[0][output_type].ToString();
        }

        public static string GetArtistName(int Artist_ID)
        {
            string SS = "SELECT Artist_Name FROM tblArtist WHERE Artist_ID = '" + Artist_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return dt.Rows[0]["Artist_Name"].ToString();
        }
  
    }
}
