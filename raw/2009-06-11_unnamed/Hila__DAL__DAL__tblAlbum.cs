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
            string Album_Pic,
            int Album_Year
            )
        {
            return FullConnected("INSERT INTO tblAlbum(Album_ID, Artist_ID, Album_Name, Album_Pic,Album_Year) values('" + Album_ID + "','" + Artist_ID + "','" + Album_Name + "','" + Album_Pic + "','" + Album_Year + "')");
        }

        public static int UpdateAlbum(
            int Album_ID,
            int Artist_ID,
            string Album_Name,
            string Album_Pic,
            int Album_Year
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

            if (!(Album_Year == 0))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Album_Year='" + Album_Year + "'";
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
            string SS = "SELECT tblAlbum.Album_ID,tblAlbum.Album_Name,tblAlbum.Album_Pic FROM tblAlbum ";
            SS += "inner join tblArtist on tblArtist.Artist_ID = tblAlbum.Artist_ID ";
            SS += "where tblArtist.Artist_ID =" + Artist_ID;
            return GetDataTable(SS,"AlbumsByArtist");
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
            string SS = "SELECT tblAlbum.Album_ID, tblAlbum.Album_Name,tblAlbum.Artist_ID, tblArtist.Artist_Name ,tblAlbum.Album_Year ";
            SS += "FROM tblAlbum inner join tblArtist on tblArtist.Artist_ID = tblAlbum.Artist_ID ";
            SS += "WHERE Album_Name LIKE '%";
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

        public static string GetAlbumName(int Album_ID)
        {
            string SS = "SELECT Album_Name FROM tblAlbum WHERE Album_ID = '" + Album_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return dt.Rows[0]["Album_Name"].ToString();
        }

        public static DataTable AlbumNameLike(string input)
        {
            string sqlString = "SELECT tblAlbum.Album_ID, tblAlbum.Album_Name ";
            sqlString += "FROM tblAlbum ";
            sqlString += "WHERE tblAlbum.Album_Name LIKE '%";
            sqlString += input;
            sqlString += "%'";
            return GetDataTable(sqlString, input);
        }

        public static int GetAlbumNextID()
        {
            DataTable dt = GetDataTable("SELECT MAX(Album_ID) FROM tblAlbum", "tblmaxalbum");
            if (dt.Rows.Count == 0)
                return 1;
            else
                return int.Parse(dt.Rows[0][0].ToString())+1;
        }

        public static DataTable GetNewestAlbums(int number)
        {
            string SS = "SELECT TOP " + number.ToString() + " tblAlbum.Album_ID,tblAlbum.Album_Name,tblArtist.Artist_ID,tblArtist.Artist_Name FROM tblAlbum ";
            SS += "inner join tblArtist on tblArtist.Artist_ID = tblAlbum.Artist_ID ORDER BY tblAlbum.Album_year DESC";
            return GetDataTable(SS, "NewAlbums");
        }

        public static DataTable GetTOPAlbums(int number)
        {
            string SS = "SELECT TOP " + number.ToString() + " tblAlbum.Album_ID,tblAlbum.Album_Name,tblArtist.Artist_ID,tblArtist.Artist_Name,tblAlbum.Album_Year FROM tblAlbum ";
            SS += "inner join tblArtist on tblArtist.Artist_ID = tblAlbum.Artist_ID ORDER BY tblAlbum.Album_Pop DESC";
            return GetDataTable(SS, "TOPAlbums");
        }

        public static DataTable LastAlbumAdded(int number)
        {
            string SS = "SELECT TOP " + number.ToString() + " tblAlbum.Album_ID,tblAlbum.Album_Name,tblArtist.Artist_ID,tblArtist.Artist_Name,tblAlbum.Album_Pic,tblAlbum.Album_Year FROM tblAlbum ";
            SS += "inner join tblArtist on tblArtist.Artist_ID = tblAlbum.Artist_ID ORDER BY tblAlbum.Album_ID DESC";
            return GetDataTable(SS, "NewAlbums");
        }

        public static DataTable RandomAlbums(int number)
        {
            string SS = "SELECT TOP " + number.ToString() + " tblAlbum.Album_ID,tblAlbum.Album_Name,tblArtist.Artist_ID,tblArtist.Artist_Name,tblAlbum.Album_Pic,tblAlbum.Album_Year FROM tblAlbum";
            SS += " inner join tblArtist on tblArtist.Artist_ID = tblAlbum.Artist_ID ORDER By NEWID()";
            return GetDataTable(SS, "NewAlbums");
        }

    }
}
