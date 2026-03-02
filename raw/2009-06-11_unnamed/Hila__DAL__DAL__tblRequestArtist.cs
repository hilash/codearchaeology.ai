using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class tblRequestArtist : SqlHelper
    {
        public static int InsertRequestArtist(
         int Artist_ID,
         string Artist_Type,
         string Artist_Name,
         string Artist_Bio,
         string Artist_Pic,
         string Artist_Rtype
         )
        {
            string s = "INSERT INTO tblRequestArtist(Artist_ID, Artist_Type, Artist_Name, Artist_Bio, Artist_Pic, Artist_Rtype) ";
            s += "VALUES ('" + Artist_ID + "','" + Artist_Type + "','" + Artist_Name + "','" + Artist_Bio + "','" + Artist_Pic + "','" + Artist_Rtype + "')";
            return FullConnected(s);
        }

        public static DataTable GetArtistsID_DT()
        {
            return GetDataTable("SELECT Artist_ID FROM tblRequestArtist", "tblRequestArtist");
        }

        public static DataTable GetArtists_DT()
        {
            return GetDataTable("SELECT * FROM tblRequestArtist", "tblRequestArtist");
        }

        public static int GetRequestArtistNextID()
        {
            DataTable dt = GetDataTable("SELECT MAX(Artist_ID) FROM tblRequestArtist", "tblmaxalbum");
            if (dt.Rows.Count == 0)
                return 1;
            else
                return int.Parse(dt.Rows[0][0].ToString()) + 1;
        }
    }
}
