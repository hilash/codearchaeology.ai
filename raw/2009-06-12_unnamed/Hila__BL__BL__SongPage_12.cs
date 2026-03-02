using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    /// <summary>
    /// In songs page, all the details about the song
    /// </summary>
    public class SongPage
    {

        public int Song_ID;
        public int Song_Pop;
        public int Album_ID;
        public int Artist_ID;
        public string Song_Name;
        public string Album_Name;
        public string Artist_Name;
        public string Song_Lyrics;
        public string Song_Clip;
        public string Song_Genre;
        public string Song_Pic;
        public DateTime Song_Length;
        
        public void getSongData(int Song_ID1)
        {
            DataSet DS = new DataSet();
            DataTable DT = DAL.tblSong.GetSongDT(Song_ID1);
            DS.Tables.Add(DT);

            Song_ID = Song_ID1;
            Song_Pop  = int.Parse(DS.Tables[0].Rows[0]["Song_Pop"].ToString());
            Album_ID  = int.Parse(DS.Tables[0].Rows[0]["Album_ID"].ToString());
            Artist_ID = int.Parse(DS.Tables[0].Rows[0]["Artist_ID"].ToString());
            Song_Name = DS.Tables[0].Rows[0]["Song_Name"].ToString();
            Album_Name = DAL.tblAlbum.GetAlbumName(Album_ID);
            Artist_Name = DAL.tblArtist.GetArtistName(Artist_ID);
            Song_Lyrics = DS.Tables[0].Rows[0]["Song_Lyrics"].ToString();
            Song_Clip = DS.Tables[0].Rows[0]["Song_Clip"].ToString();
            Song_Genre = DS.Tables[0].Rows[0]["Song_Genre"].ToString();
            Song_Pic = DS.Tables[0].Rows[0]["Song_Pic"].ToString();
            Song_Length = (DateTime)DS.Tables[0].Rows[0]["Song_Length"];

        }
    }
}
