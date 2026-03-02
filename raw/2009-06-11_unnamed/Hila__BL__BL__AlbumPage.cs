using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class AlbumPage
    {
        public int Album_ID;
        public int Artist_ID;
        public string Album_Name;
        public string Artist_Name;
        public string Album_Pic;
        public int Album_Year;
        public DataTable songs;

        public void getAlbumData(int Album_ID1)
        {
            DataTable DT = DAL.tblAlbum.GetAlbumDT(Album_ID1);

            Album_ID = Album_ID1;
            Artist_ID = int.Parse(DT.Rows[0]["Artist_ID"].ToString());
            Album_Name = DT.Rows[0]["Album_Name"].ToString();
            Artist_Name = DAL.tblArtist.GetArtistName(Artist_ID);
            Album_Pic = DT.Rows[0]["Album_Pic"].ToString();
            Album_Year = 0;
            try
            {
                Album_Year = int.Parse(DT.Rows[0]["Album_Year"].ToString());
            }
            catch (Exception Ex)
            {
                Album_Year = 0;
            }

            songs = DAL.tblSong.GetSongsOfAlbum(Album_ID);


            // tbl 0 - artist Data
           // DAL.SqlHelper.PrintDataTable(DS.Tables[0]);

            // tbl 1 - artist albums
            //DAL.SqlHelper.PrintDataTable(DS.Tables[1]);
            // tbl 2 - artist Songs
            //DAL.SqlHelper.PrintDataTable(DS.Tables[2]);

        }
    }
}
