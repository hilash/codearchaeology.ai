using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class SongsPage
    {
        public DataTable pop;
        public DataTable news;
        public DataTable random;

        public void getData()
        {
            pop = DAL.tblSong.SongPopTable(20);
            news = DAL.tblSong.LastSongsAdded(20);
            random = DAL.tblSong.RandomSongs(20);
        }

        // return SongPopTable with a like columne that says if the user like the song or not.
        public static DataTable SongPopTableWithUserLikes(int User_ID)
        {
            DataTable dt1 = DAL.Join.getSongsByUser(User_ID);
            DataTable dt2 = DAL.tblSong.SongPopTable(20);

            dt2.Columns.Add("like");

            foreach (DataRow dr in dt2.Rows)
            {
                dr["like"] = "~/images/HeartSelected.png";
            }

            DAL.SqlHelper.PrintDataTable(dt2);
            return dt2;
        }

    }
}
