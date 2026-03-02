using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class tblArtist : SqlHelper
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

        public static string GetInfo(string output_type, string where_type,string where_what)
        {
            string SS = "SELECT * FROM tblArtist WHERE ";
            SS += where_type;
            SS += "='" + where_what + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return dt.Rows[0][output_type].ToString();
        }

        public static void AddArtistPOP(int Artist_ID, int add)
        {
            int newPop;
            int Artist_Pop = int.Parse(GetInfo("Artist_Pop", "Artist_ID", Artist_ID.ToString()));
            if ((newPop = (Artist_Pop + add)) <= 0)
                newPop = 0;
            FullConnected("UPDATE tblArtist SET Artist_Pop='" + newPop + "'  WHERE Artist_ID='" + Artist_ID + "'");
        }

        public static int ArtistID(string Artist_Name)
        {
            string SS = "SELECT Artist_Id FROM tblArtist WHERE Artist_Name='" + Artist_Name + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return int.Parse(dt.Rows[0]["Artist_Id"].ToString());
        }

        public static int PopArtist(int number)
        {
            string SS = "SELECT (Artist_ID) FROM tblArtist ORDER BY Artist_Pop DESC ";
            DataTable dt = GetDataTable(SS, "tblArtist");
            //PrintDataTable(dt);
            if (number > dt.Rows.Count)
                return -1;
            else return int.Parse(dt.Rows[number]["Artist_Id"].ToString());
        } 
  
    }
}
