using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class tblSong : SqlHelper
    {
        public static string tblName
        {
            get
            {
                return "tblSong";
            }
        }

        public static int InsertSong(
            int Song_ID,
            string Song_Name,
            int Album_ID,
            int Artist_ID,
            string Song_Lyrics,
            string Song_Clip,
            DateTime Song_Length,
            string Song_Genre,
            string Song_Pic
            )
        {
            return FullConnected("INSERT INTO tblSong(Song_ID, Song_Name, Album_ID, Artist_ID, Song_Lyrics, Song_Clip, Song_Length, Song_Genre,Song_Pic,Song_Pop) values('" + Song_ID + "','" + Song_Name + "','" + Album_ID + "','" + Artist_ID + "','" + Song_Lyrics + "','" + Song_Clip + "','" + Song_Length + "','" + Song_Genre + "','" + Song_Pic + "',0)");
        }

        public static int UpdateSong(
            int Song_ID,
            string Song_Name,
            int Album_ID,
            int Artist_ID,
            string Song_Lyrics,
            string Song_Clip,
            DateTime Song_Length, /*Date is wrong*/
            string Song_Genre,
            string Song_Pic
            )
        {
            DateTime Zero = new DateTime(1, 1,1);
            bool flag = false;
            string SqlString = "UPDATE tblSong ";

            if (!((Song_Name == "") || (Song_Name == null)))
            {
                SqlString += "SET ";
                SqlString += "Song_Name='" + Song_Name + "'";
                flag = true;
            }
            if (!(Album_ID == 0))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Album_ID='" + Album_ID + "'";
                flag = true;
            }
            
            if (!(Artist_ID == 0))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Artist_ID='" + Artist_ID + "'";
                flag = true;
            }
            
            if (!((Song_Lyrics == "") || (Song_Lyrics == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Song_Lyrics='" + Song_Lyrics + "'";
                flag = true;
            }
            
            if (!((Song_Clip == "") || (Song_Clip == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Song_Clip='" + Song_Clip + "'";
                flag = true;
            }
           
            /*
            //if (!(Song_Length.Equals(Zero)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Song_Length='" + Song_Length + "'";
                flag = true;
            }*/
            
            if (!((Song_Genre == "") || (Song_Genre == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Song_Genre='" + Song_Genre + "'";
                flag = true;
            }
            if (!((Song_Pic == "") || (Song_Pic == null)))
            {
                if (!flag)
                    SqlString += "SET ";
                else SqlString += ", ";
                SqlString += "Song_Pic='" + Song_Pic + "'";
                flag = true;
            }
            if (flag)
            {
                SqlString += " WHERE Song_ID='" + Song_ID + "'";
                return FullConnected(SqlString);
            }
            else return -1;
        }

        public static int DeleteSong(int Song_ID)
        {
            return FullConnected("DELETE FROM tblSong WHERE Song_ID='" + Song_ID + "'");
        }

        public static string GetInfo(string output_type, string where_type, string where_what)
        {
            string SS = "SELECT * FROM tblSong WHERE ";
            SS += where_type;
            SS += "='" + where_what + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return dt.Rows[0][output_type].ToString();
        }

        public static void AddSongPOP(int Song_ID, int add)
        {
            int newPop;
            int Song_Pop = int.Parse(GetInfo("Song_Pop", "Song_ID", Song_ID.ToString()));
            if  ((newPop = (Song_Pop + add)) <= 0)
                newPop = 0;
            FullConnected("UPDATE tblSong SET Song_Pop='" + newPop + "'  WHERE Song_ID='" + Song_ID + "'");
        }

        public static int SongID(string Song_Name)
        {
            string SS = "SELECT Song_Id FROM tblSong WHERE Song_Name='" + Song_Name + "'";
            DataTable dt = GetDataTable(SS, tblName);

            return int.Parse(dt.Rows[0]["Song_Id"].ToString());
        }

        public static int PopSong(int number)
        {
            string SS = "SELECT (Song_ID) FROM tblSong ORDER BY Song_Pop DESC ";
            DataTable dt = GetDataTable(SS, "tblSong");
            //PrintDataTable(dt);
            if (number > dt.Rows.Count)
                return -1;
            else return int.Parse(dt.Rows[number-1]["Song_ID"].ToString());
        }

        // return a data table with (Song_ID,Song_Pop) ordered by Song_Pop
        public static DataTable SongPopTable()
        {
            string SS = "SELECT Song_ID, Song_Pop FROM tblSong ORDER BY Song_Pop DESC";
            DataTable dt = GetDataTable(SS, "tblSong");
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                    Console.Write(dr[i].ToString() + "   ");
                Console.WriteLine();
            }
            return dt;
        }

        public static bool IsSongInDB(int Song_ID)
        {
            return (FullConnected("UPDATE tblSong SET Song_ID='" + Song_ID + "' WHERE Song_ID='" + Song_ID + "'") > 0) ? true : false;
        }

        // DOESNT WORK
        public static DataTable SearchSongName(string Song_Name)
        {
           // string sqlString = "SELECT tblSong.Song_ID, tblSong.Song_Name from tblSong ";
           
           //// sqlString += "inner join tblAlbum on tblAlbum.Album_ID = tblSong.Album_ID ";
           // sqlString += "WHERE tblSong.Song_Name LIKE '%";
           // sqlString += Song_Name;
           // sqlString += "%'";

            string sqlString = "SELECT tblSong.Song_ID, tblSong.Song_Name, tblArtist.Artist_Name, tblAlbum.Album_Name, ";
            sqlString += "tblSong.Artist_ID, tblSong.Album_ID, tblSong.Song_Genre, tblSong.Song_Length, tblSong.Song_Pop ";
            sqlString += "FROM tblSong inner join tblArtist on tblArtist.Artist_ID = tblSong.Artist_ID ";
            sqlString += "inner join tblAlbum on tblAlbum.Album_ID = tblSong.Album_ID ";
            sqlString += "WHERE tblSong.Song_Name LIKE '%";
            sqlString += Song_Name;
            sqlString += "%' ORDER BY Song_Pop DESC";

            return GetDataTable(sqlString, "tblSong");
        }

        /// <summary>
        /// INPUT:  Song_ID
        /// OUTPUT: data table with information about the Song
        /// </summary>
        /// <param name="Song_ID"></param>
        /// <returns></returns>
        public static DataTable GetSongDT(int Song_ID)
        {
            return GetDataTable("SELECT * FROM tblSong WHERE Song_ID='" + Song_ID + "'", "tblSong");
        }

        /// <summary>
        /// OUTPUT: data table with information about all the Song in the data base
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSongsDT()
        {
            return GetDataTable("SELECT * FROM tblSong", "tblSong");
        }

        /// <summary>
        /// INPUT:  Artist_ID
        /// OUTPUT: data table with Songs of that Artist
        /// </summary>
        /// <param name="Artist_ID"></param>
        /// <returns></returns>
        public static DataTable GetSongsOfArtist(int Artist_ID)
        {
            string sqlString = "select tblSong.Song_ID, tblSong.Song_Name,tblSong.Album_ID, tblAlbum.Album_Name, tblSong.Song_Pop ";
            sqlString += "from tblArtist inner join tblSong on tblArtist.Artist_ID = tblSong.Artist_ID ";
            sqlString += "inner join tblAlbum on tblAlbum.Album_ID = tblSong.Album_ID ";
           // sqlString += "inner join tblUserSongsList on tblUserSongsList.Song_ID = tblSong.Song_ID ";
            sqlString += "where tblSong.Artist_ID =" + Artist_ID;

            return SqlHelper.GetDataTable(sqlString, "songsByartist");
        }

        /// <summary>
        /// INPUT:  Artist_ID
        /// OUTPUT: data table with Songs of that Artist
        /// </summary>
        /// <param name="Artist_ID"></param>
        /// <returns></returns>
        public static DataTable GetSongsOfAlbum(int Album_ID)
        {
            string sqlString = "select tblSong.Song_ID, tblSong.Song_Name,tblSong.Artist_ID, tblArtist.Artist_Name, tblSong.Song_Pop ";
            sqlString += "from tblSong inner join tblAlbum on tblAlbum.Album_ID = tblSong.Album_ID ";
            sqlString += "inner join tblArtist on tblArtist.Artist_ID = tblSong.Album_ID ";
            sqlString += "where tblSong.Album_ID =" + Album_ID;

            return SqlHelper.GetDataTable(sqlString, "songsByartist");
        }

        public static DataTable GetSongsOfGenre(string Genre)
        {
            return GetDataTable("SELECT Song_ID FROM tblSong WHERE Genre='" + Genre + "'", "tblSong");
        }

    }
}
