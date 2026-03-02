using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class ArtistPage
    {
        public int Artist_ID;
        public int Artist_Pop;
        public string Artist_Type;
        public string Artist_Name;
        public string Artist_Bio;
        public string Artist_Pic;

        public DataSet getArtistData(int Artist_ID1)
        {
            DataSet DS = new DataSet();
            DataTable DT = DAL.tblArtist.GetArtistDT(Artist_ID1);
            DS.Tables.Add(DT);

            Artist_ID = Artist_ID1;
            Artist_Pop = int.Parse(DS.Tables[0].Rows[0]["Artist_Pop"].ToString());
            Artist_Type = DS.Tables[0].Rows[0]["Artist_Type"].ToString();
            Artist_Name = DS.Tables[0].Rows[0]["Artist_Name"].ToString();
            Artist_Bio = DS.Tables[0].Rows[0]["Artist_Bio"].ToString();
            Artist_Pic = DS.Tables[0].Rows[0]["Artist_Pic"].ToString();

            // tbl 0 - artist Data
            //DAL.SqlHelper.PrintDataTable(DS.Tables[0]);

            DS.Tables.Add(DAL.tblAlbum.GetAlbumsOfArtist(Artist_ID1));
            DS.Tables.Add(DAL.tblSong.GetSongsOfArtist(Artist_ID1));
            // tbl 1 - artist albums
            //DAL.SqlHelper.PrintDataTable(DS.Tables[1]);
            // tbl 2 - artist Songs
            //DAL.SqlHelper.PrintDataTable(DS.Tables[2]);

            return DS;
        }
    }
}
