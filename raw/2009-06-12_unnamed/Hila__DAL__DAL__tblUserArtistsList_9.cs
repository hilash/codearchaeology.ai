using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class tblUserArtistsList : SqlHelper
    {
        public static string tblName
        {
            get
            {
                return "tblUserArtistsList";
            }
        }

        public static int InsertUserArtistsList(int User_ID, int Artist_ID)
        {
            DAL.tblArtist.AddArtistPOP(Artist_ID, 1);
            return FullConnected("INSERT INTO tblUserArtistsList(User_ID, Artist_ID) VALUES('" + User_ID + "','" + Artist_ID + "')");
        }

        public static int DeleteUserArtistsList(int User_ID, int Artist_ID)
        {
            DAL.tblArtist.AddArtistPOP(Artist_ID, -1);
            return FullConnected("DELETE FROM tblUserArtistsList WHERE User_ID='" + User_ID + "' AND Artist_ID='" + Artist_ID + "'");
        }

        // return all the artists that the user likes
        public static DataTable GetArtists(int User_ID)
        {
            string SS = "SELECT (Artist_ID) FROM tblUserArtistsList WHERE User_ID='" + User_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);
            return dt;
        }

        // return all the users that the likes this artist
        public static DataTable GetUsers(int Artist_ID)
        {
            string SS = "SELECT (User_ID) FROM tblUserArtistsList WHERE Artist_ID='" + Artist_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);
            return dt;
        }

        // get user you want to delete and reduce 1 from each Artist he liked
        public static void DeleteArtists(int User_ID)
        {
            string SS = "SELECT (Artist_ID) FROM tblUserArtistsList WHERE User_ID='" + User_ID + "'";
            DataTable dt = GetDataTable(SS, tblName);

            foreach (DataRow dr in dt.Rows)
            {
                DeleteUserArtistsList(User_ID, int.Parse(dr["Artist_Id"].ToString()));
            }
        }     
    }
}
