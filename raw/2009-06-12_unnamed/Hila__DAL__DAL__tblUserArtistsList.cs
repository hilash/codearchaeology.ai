using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class tblUserArtistsList : SqlHelper
    {
        private static string tblName
        {
            get
            {
                return "tblUserArtistsList";
            }
        }

        private static int InsertUserArtistsList(int User_ID, int Artist_ID)
        {
            return FullConnected("INSERT INTO tblUserArtistsList(User_ID, Artist_ID) VALUES('" + User_ID + "','" + Artist_ID + "')");
        }

        private static int DeleteUserArtistsList(int User_ID, int Artist_ID)
        {
            return FullConnected("DELETE FROM tblUserArtistsList WHERE User_ID='" + User_ID + "' AND Artist_ID='" + Artist_ID + "'");
        }

        // return all the artists that the user likes
        private static DataTable GetArtists(int User_ID)
        {
            string SS = "SELECT (Artist_ID) FROM tblUserArtistsList WHERE User_ID='" + User_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);
            return dt;
        }

        // return all the users that the likes this artist
        private static DataTable GetUsers(int Artist_ID)
        {
            string SS = "SELECT (User_ID) FROM tblUserArtistsList WHERE Artist_ID='" + Artist_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);
            return dt;
        }
        
        /*
        static void Main(string[] args)
        {
            DataTable dt = GetUsers(15);
            PrintDataTable(dt);
            
            Console.ReadKey();
        }
         */
    }
}
