using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class tblAlbum : SqlHelper
    {
        public static string tblName
        {
            get
            {
                return "tblAlbum";
            }
        }

        public static int InsertAlbum(
            int Album_ID,
            int Artist_ID,
            string Album_Name,
            string Album_Pic
            )
        {
            return FullConnected("INSERT INTO tblAlbum(Album_ID, Artist_ID, Album_Name, Album_Pic) values('" + Album_ID + "','" + Artist_ID + "','" + Album_Name + "','" + Album_Pic + "')");
        }

        public static int UpdateAlbum(
            int Album_ID,
            int Artist_ID,
            string Album_Name,
            string Album_Pic
            )
        {
            bool flag = false;
            string SqlString = "UPDATE tblAlbum ";

            if (!(Artist_ID == 0))
            {
                SqlString += "SET ";
                SqlString += "Artist_ID='" + Artist_ID + "'";
                flag = true;
            }
            if (!((Album_Name == "") || (Album_Name == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Album_Name='" + Album_Name + "'";
                flag = true;
            }
            if (!((Album_Pic == "") || (Album_Pic == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Album_Pic='" + Album_Pic + "'";
                flag = true;
            }
            if (flag)
            {
                SqlString += " WHERE Album_ID='" + Album_ID + "'";
                return FullConnected(SqlString);
            }
            else return -1;
        }

        public static int DeleteAlbum(int Album_ID)
        {
            return FullConnected("DELETE FROM tblAlbum WHERE Album_ID='" + Album_ID + "'");
        }

        public static string GetInfo(string output_type, string where_type, string where_what)
        {
            string SS = "SELECT * FROM tblAlbum WHERE ";
            SS += where_type;
            SS += "='" + where_what + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return dt.Rows[0][output_type].ToString();
        }

        public static int AlbumID(string Album_Name)
        {
            string SS = "SELECT Album_Id FROM tblAlbum WHERE Album_Name='" + Album_Name + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return int.Parse(dt.Rows[0]["Album_Id"].ToString());
        }
    }
}