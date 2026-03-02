using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class AdminRequests
    {
        public DataTable artists;
        public DataTable albums;
        public DataTable songs;

        public void getRequestsTables()
        {
            artists = DAL.tblRequestArtist.GetArtists_DT();
            albums = DAL.tblRequestAlbum.GetAlbums_DT();
            songs = DAL.tblRequestSong.GetSongs_DT();
        }
    }
}
