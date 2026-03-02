using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class tblRequestSong : SqlHelper
    {
        public static int InsertRequestSong(
          int Song_ID,
          string Song_Name,
          int Album_ID,
          string Album_Name,
          int Artist_ID,
          string Artist_Name,
          string Song_Lyrics,
          string Song_Clip,
          string Song_Length,
          string Song_Genre,
          string Song_Pic,
          string Song_Rtype
          )
        {
            string s = "INSERT INTO tblRequestSong(Song_ID, Song_Name, Album_ID, Album_Name, Artist_ID, Artist_Name, Song_Lyrics, Song_Clip,";
            s += "Song_Length, Song_Genre, Song_Pic, Song_Rtype) ";
            s += "values('" + Song_ID + "','" + Song_Name + "','" + Album_ID + "','" + Album_Name+ "','" + Artist_ID + "','" + Artist_Name + "','" + Song_Lyrics + "','" + Song_Clip + "','" + Song_Length + "','" + Song_Genre + "','" + Song_Pic + "','" + Song_Rtype + "')";
            return FullConnected(s);
        }

        public static DataTable GetSongsID_DT()
        {
            return GetDataTable("SELECT Song_ID FROM tblRequestSong", "tblRequestSong");
        }

        public static DataTable GetSongs_DT()
        {
            return GetDataTable("SELECT * FROM tblRequestSong", "tblRequestSong");
        }

        public static int GetRequestSongNextID()
        {
            DataTable dt = GetDataTable("SELECT MAX(Song_ID) FROM tblRequestSong", "tblmaxsong");
            if (dt.Rows.Count == 0)
                return 1;
            else
                return int.Parse(dt.Rows[0][0].ToString()) + 1;
        }
    }
}
