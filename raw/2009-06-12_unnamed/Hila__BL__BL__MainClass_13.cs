using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class MainClass
    {
        public static void Main()
        {
            //DataSet DS = new DataSet();
            //DS = UserPage.getSongsByUser(1);
            //DAL.SqlHelper.PrintDataTable(DS.Tables[0]);

            //int[] arr1 = { 1, 2, 3, 4 };
            //int[] arr2 = { 1, 2, 3, 4 };
            //Console.WriteLine( arr1 == arr2);
            //Console.ReadKey();

            //SongPage SP = new SongPage();
            //SP.getSongData(2);
            //Console.WriteLine(SP.Song_ID);
            //Console.WriteLine(SP.Song_Name);
            //Console.WriteLine(SP.Album_ID);
            //Console.WriteLine(SP.Artist_ID);
            //Console.WriteLine(SP.Song_Pop);
            //Console.WriteLine(SP.Album_Name);
            //Console.WriteLine(SP.Artist_Name);
            //Console.WriteLine(SP.Song_Lyrics);
            //Console.WriteLine(SP.Song_Clip);
            //Console.WriteLine(SP.Song_Genre);
            //Console.WriteLine(SP.Song_Pic);
            //Console.WriteLine(SP.Song_Length);    

            //ArtistPage AP = new ArtistPage();
            //AP.getArtistData(1);
            //Console.WriteLine(AP.Artist_ID);
            //Console.WriteLine(AP.Artist_Pop);
            //Console.WriteLine(AP.Artist_Type);
            //Console.WriteLine(AP.Artist_Name);
            //Console.WriteLine(AP.Artist_Bio);
            //Console.WriteLine(AP.Artist_Pic);

            //AlbumPage AP = new AlbumPage();
            //AP.getAlbumData(1);
            //Console.WriteLine(AP.Album_ID);
            //Console.WriteLine(AP.Album_Name);
            //Console.WriteLine(AP.Album_Pic);
            //Console.WriteLine(AP.Artist_ID);
            //Console.WriteLine(AP.Artist_Name);
            ArtistPage AP = new ArtistPage();
            Console.WriteLine("hereeeeeee");
            AP.getArtistData(1);
            Console.ReadKey();

        }
    }
}
