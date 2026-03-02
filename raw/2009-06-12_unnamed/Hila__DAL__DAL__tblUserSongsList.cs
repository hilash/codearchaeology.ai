using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class tblUserSongsList : SqlHelper
    {
        private static string tblName
        {
            get
            {
                return "tblUserSongsList";
            }
        }

        private static int InsertUserSongsList(int User_ID, int Song_ID)
        {
            return FullConnected("INSERT INTO tblUserSongsList(User_ID, Song_ID) VALUES('" + User_ID + "','" + Song_ID + "')");
        }

        private static int DeleteUserSongsList(int User_ID, int Song_ID)
        {
            return FullConnected("DELETE FROM tblUserSongsList WHERE User_ID='" + User_ID + "' AND Song_ID='" + Song_ID + "'");
        }

        // return all the Songs that the user likes
        private static DataTable GetSongs(int User_ID)
        {
            string SS = "SELECT (Song_ID) FROM tblUserSongsList WHERE User_ID='" + User_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);
            return dt;
        }

        // return all the users that the likes this Song
        private static DataTable GetUsers(int Song_ID)
        {
            string SS = "SELECT (User_ID) FROM tblUserSongsList WHERE Song_ID='" + Song_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);
            return dt;
        }

        static void Main(string[] args)
        {


            Console.ReadKey();
        }
    }
}
