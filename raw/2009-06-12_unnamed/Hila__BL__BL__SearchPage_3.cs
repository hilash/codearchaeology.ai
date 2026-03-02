using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class SearchPage
    {
        public static DataSet SearchSong(string Q)
        {
            DataSet DS = new DataSet();
            DataTable DT = DAL.tblSong.SearchSongName(Q);
            DS.Tables.Add(DT);
            return DS;
        }

        public static DataSet SearchArtist(string Q)
        {
            DataSet DS = new DataSet();
            DataTable DT = DAL.tblArtist.SearchArtistName(Q);
            DS.Tables.Add(DT);
            return DS;
        }

        public static DataSet SearchAlbum(string Q)
        {
            DataSet DS = new DataSet();
            DataTable DT = DAL.tblAlbum.SearchAlbumName(Q);
            DS.Tables.Add(DT);
            return DS;
        }
    }
}
