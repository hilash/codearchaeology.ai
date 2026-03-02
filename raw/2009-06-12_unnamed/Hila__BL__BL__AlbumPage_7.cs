using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    class AlbumPage
    {
        public int Album_ID;
        public int Artist_ID;
        public string Album_Name;
        public string Artist_Name;
        public string Album_Pic;

        public DataSet getAlbumData(int Album_ID1)
        {
            DataSet DS = new DataSet();
            DataTable DT = DAL.tblAlbum.GetAlbumDT(Album_ID1);
            DS.Tables.Add(DT);

            Album_ID = Album_ID1;
            Artist_ID = int.Parse(DS.Tables[0].Rows[0]["Artist_ID"].ToString());
            Album_Name = DS.Tables[0].Rows[0]["Album_Name"].ToString();
            Artist_Name = DAL.tblArtist.GetArtistName(Artist_ID);
            Album_Pic = DS.Tables[0].Rows[0]["Album_Pic"].ToString();

            // tbl 0 - artist Data
           // DAL.SqlHelper.PrintDataTable(DS.Tables[0]);

            DS.Tables.Add(DAL.tblSong.GetSongsOfAlbum(Album_ID1));
            // tbl 1 - artist albums
            //DAL.SqlHelper.PrintDataTable(DS.Tables[1]);
            // tbl 2 - artist Songs
            //DAL.SqlHelper.PrintDataTable(DS.Tables[2]);

            return DS;
        }
    }
}
