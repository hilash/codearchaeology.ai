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
        public DataTable albums;
        public DataTable songs;

        public void getArtistData(int Artist_ID1)
        {
            DataTable DT = DAL.tblArtist.GetArtistDT(Artist_ID1);

            Artist_ID = Artist_ID1;
            Artist_Pop = int.Parse(DT.Rows[0]["Artist_Pop"].ToString());
            Artist_Type = DT.Rows[0]["Artist_Type"].ToString();
            Artist_Name = DT.Rows[0]["Artist_Name"].ToString();
            Artist_Bio = DT.Rows[0]["Artist_Bio"].ToString();
            Artist_Pic = DT.Rows[0]["Artist_Pic"].ToString();



            songs  = DAL.tblSong.GetSongsOfArtist(Artist_ID);
            DT.TableName = "songs";
           // DAL.SqlHelper.PrintDataTable(songs);
            DT.Clear();

            albums = DAL.tblAlbum.GetAlbumsOfArtist(Artist_ID);
            DT.TableName = "albums";
           // DAL.SqlHelper.PrintDataTable(albums);
            DT.Clear();

            Console.WriteLine(Artist_Name);

        }
    }
}
