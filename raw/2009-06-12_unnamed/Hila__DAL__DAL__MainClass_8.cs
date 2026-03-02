using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MainClass
        {
        public static void Main()
    {

         //DAL.SqlHelper.PrintDataTable(Join.getArtistsByUser(1));
        //string i = DAL.tblArtist.GetArtistName(1);
        //Console.WriteLine(i);
        // DAL.SqlHelper.PrintDataTable(DAL.tblAlbum.GetAlbumsOfArtist(2));
         DAL.SqlHelper.PrintDataTable(DAL.tblSong.GetSongsOfArtist(1));
        }
}
}
