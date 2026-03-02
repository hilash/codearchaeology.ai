using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class ArtistsPage
    {
        public DataTable pop;
        public DataTable all;
        public DataTable random;

        public void getData()
        {
            pop = DAL.tblArtist.ArtistPopTable(20);
            all = DAL.tblArtist.GetArtistsDT();
            random = DAL.tblArtist.RandomArtists(2);
       }
    }
}
