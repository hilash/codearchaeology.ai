using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class tblAlbum : SqlHelper
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

        /// <summary>
        /// INPUT:  Album_ID
        /// OUTPUT: is the album is in the Data Base
        /// </summary>
        /// <param name="Album_ID"></param>
        /// <returns></returns>
        public static bool IsAlbumInDB(int Album_ID)
        {
            return (FullConnected("UPDATE tblAlbum SET Album_ID='" + Album_ID + "' WHERE Album_ID='" + Album_ID + "'") > 0) ? true : false;
        }
 
        /// <summary>
        /// INPUT:  Album_ID
        /// OUTPUT: data table with information about the album
        /// </summary>
        /// <param name="Album_ID"></param>
        /// <returns></returns>
        public static DataTable GetAlbumDT(int Album_ID)
        { 
            return GetDataTable("SELECT * FROM tblAlbum WHERE Album_ID='" + Album_ID + "'", "tblAlbum"); 
        }

        /// <summary>
        /// OUTPUT: data table with information about all the album in the data base
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAlbumsDT()
        { 
            return GetDataTable("SELECT * FROM tblAlbum", "tblAlbum"); 
        }

        /// <summary>
        /// INPUT:  Artist_ID
        /// OUTPUT: data table with Albums of that Artist
        /// </summary>
        /// <param name="Artist_ID"></param>
        /// <returns></returns>
        public static DataTable GetAlbumsOfArtist(int Artist_ID)
        {
            return GetDataTable("SELECT Album_ID FROM tblAlbum WHERE Artist_ID='" + Artist_ID + "'", "tblAlbum");
        }

        public static int Album_ID(string Album_Name)
        {
            DataTable dt = GetDataTable("SELECT Album_ID FROM tblAlbum WHERE Album_Name='" + Album_Name + "'", "tblAlbum");
            return int.Parse(dt.Rows[0]["Album_ID"].ToString());
        }

        /// <summary>
        /// given a string, return a data table with albums (ID) that like album name 
        /// </summary>
        /// <param name="Album_Name"></param>
        /// <returns></returns>
        public static DataTable SearchAlbumName(string Album_Name)
        {
            string SS ="SELECT * FROM tblAlbum WHERE Album_Name LIKE '%";
            SS += Album_Name;
            SS += "%'";

            return GetDataTable(SS, "tblAlbum");
        }
     
        public static string GetInfo(string output_type, string where_type, string where_what)
        {
            string SS = "SELECT '" + output_type + "' FROM tblAlbum WHERE ";
            SS += where_type;
            SS += "='" + where_what + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return dt.Rows[0][output_type].ToString();
        }

    }
}
