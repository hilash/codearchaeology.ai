using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class tblUserSongsList : SqlHelper
    {
        public static string tblName
        {
            get
            {
                return "tblUserSongsList";
            }
        }

        public static int InsertUserSongsList(int User_ID, int Song_ID)
        {
            DAL.tblSong.AddSongPOP(Song_ID, 1);
            return FullConnected("INSERT INTO tblUserSongsList(User_ID, Song_ID) VALUES('" + User_ID + "','" + Song_ID + "')");
        }

        public static int DeleteUserSongsList(int User_ID, int Song_ID)
        {
            DAL.tblSong.AddSongPOP(Song_ID, -1);
            return FullConnected("DELETE FROM tblUserSongsList WHERE User_ID='" + User_ID + "' AND Song_ID='" + Song_ID + "'");
        }

        // return all the Songs that the user likes
        public static DataTable GetSongs(int User_ID)
        {
            string SS = "SELECT (Song_ID) FROM tblUserSongsList WHERE User_ID='" + User_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);
            return dt;
        }

        // return all the users that the likes this Song
        public static DataTable GetUsers(int Song_ID)
        {
            string SS = "SELECT (User_ID) FROM tblUserSongsList WHERE Song_ID='" + Song_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);
            return dt;
        }

        // get user you want to delete and reduce 1 from each Artist he liked
        public static void DeleteSongs(int User_ID)
        {
            string SS = "SELECT (Song_ID) FROM tblUserSongsList WHERE User_ID='" + User_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);

            foreach (DataRow dr in dt.Rows)
            {
                DeleteUserSongsList(User_ID, int.Parse(dr["Song_Id"].ToString()));
            }
        }
    }
}
