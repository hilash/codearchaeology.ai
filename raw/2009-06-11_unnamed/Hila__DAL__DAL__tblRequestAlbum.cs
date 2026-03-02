using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class tblRequestAlbum : SqlHelper
    {
        public static int InsertRequestAlbum(
         int Album_ID,
         string Album_Name,
         int Artist_ID,
         string Artist_Name,
         string Album_Pic,
         int Album_Year,
         string Album_Rtype
         )
        {
            string s = "INSERT INTO tblRequestAlbum(Album_ID, Album_Name, Artist_ID, Artist_Name, Album_Pic, Album_Year, Album_Rtype) ";
            s += "VALUES ('" + Album_ID + "','" + Album_Name + "','" + Artist_ID + "','" + Artist_Name + "','" + Album_Pic + "','" + Album_Year + "','" + Album_Rtype + "')";
            return FullConnected(s);
        }

        public static DataTable GetAlbumsID_DT()
        {
            return GetDataTable("SELECT Album_ID FROM tblRequestAlbum", "tblRequestAlbum");
        }

        public static DataTable GetAlbums_DT()
        {
            return GetDataTable("SELECT * FROM tblRequestAlbum", "tblRequestAlbum");
        }

        public static int GetRequestAlbumNextID()
        {
            DataTable dt = GetDataTable("SELECT MAX(Album_ID) FROM tblRequestAlbum", "tblmaxalbum");
            if (dt.Rows.Count == 0)
                return 1;
            else
                return int.Parse(dt.Rows[0][0].ToString()) + 1;
        }
    }
}
